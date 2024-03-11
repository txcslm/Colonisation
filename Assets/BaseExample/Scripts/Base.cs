using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ResourceExample.Scripts;
using UnitExample.Scripts.Units;
using UnityEngine;

namespace BaseExample.Scripts
{
	[RequireComponent(typeof(UnitFabric))]
	[RequireComponent(typeof(ResourceScanner))]
	public class Base : MonoBehaviour
	{
		private const float MaxUnitsCount = 3;

		private readonly List<Unit> _units = new List<Unit>();

		private UnitFabric _fabric;
		private ResourceScanner _scanner;

		private void Awake()
		{
			_fabric = GetComponent<UnitFabric>();
			_scanner = GetComponent<ResourceScanner>();
		}

		private void Start()
		{
			CreateUnit();

			StartCoroutine(SearchResourceCoroutine());
		}

		private void Update()
		{
			if (_units.Count < MaxUnitsCount)
			{
				CreateUnit();
			}
		}

		private void CreateUnit()
		{
			Unit unit = _fabric.SpawnUnit();
			_units.Add(unit);
			
			unit.Initialize(this);
		}

		private IEnumerator SearchResourceCoroutine()
		{
			WaitForSeconds waitScanDelay = new WaitForSeconds(_scanner.ScanDelay);

			while (enabled)
			{
				Queue<Resource> freeResources = _scanner.Scan();
				Queue<Unit> freeUnits = FindFreeUnits();

				SendBots(freeUnits, freeResources);
				yield return waitScanDelay;
			}
		}

		private void SendBots(Queue<Unit> freeUnits, Queue<Resource> freeResources)
		{
			while (freeResources.TryPeek(out Resource resource) && freeUnits.TryPeek(out Unit unit))
			{
				if (resource.IsOrdered)
				{
					freeResources.Dequeue();
					continue;
				}

				freeUnits.Dequeue();

				resource.SetOrderedStatus();
				unit.SetTarget(resource);
				unit.Run();
			}
		}

		private Queue<Unit> FindFreeUnits()
		{
			IEnumerable<Unit> enumerable = _units.Where(unit => unit.IsFree == true);

			return new Queue<Unit>(enumerable);
		}
	}
}
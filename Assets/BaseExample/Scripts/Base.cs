using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ResourceExample.Scripts;
using UnitExample.Scripts.Units;
using UnityEngine;

namespace BaseExample.Scripts
{
    [RequireComponent(typeof(UnitFabric), typeof(ResourceScanner), typeof(ResourceStorage))] 
    public class Base : MonoBehaviour 
    {
        private const float MaxUnitsCount = 3;
        private const int UnitPrice = 3;
        private const int BasePrice = 10;

        private readonly List<Unit> _units = new List<Unit>();
        private readonly List<Resource> _orderedResources = new List<Resource>();

        private UnitFabric _fabric;
        private ResourceScanner _scanner;
        private ResourceStorage _resourceStorage;
        private Status _currentStatus;

        private enum Status
        {
            Harvest,
            Build
        }
        
        private void Awake()
        {
            _fabric = GetComponent<UnitFabric>();
            _scanner = GetComponent<ResourceScanner>();
            _resourceStorage = GetComponent<ResourceStorage>();
        }

        private void Start()
        {
            CreateUnit();

            StartCoroutine(ManageUnitsCoroutine());
        }

        private void Update() =>
            CheckResourceChange();

        private void CheckResourceChange()
        {
            if (_resourceStorage.ResourcesCount >= UnitPrice && _units.Count < MaxUnitsCount)
            {
                BuyUnit();
            }
        }

        private void BuyUnit()
        {
            CreateUnit();
            _resourceStorage.DecreaseResourceCount(UnitPrice);
        }

        private void CreateUnit()
        {
            Unit unit = _fabric.SpawnUnit();
            _units.Add(unit);

            unit.Initialize(this, _resourceStorage);
        }

        private IEnumerator ManageUnitsCoroutine()
        {
            WaitForSeconds waitScanDelay = new WaitForSeconds(_scanner.ScanDelay);
            Debug.Log("manage");

            while (enabled)
            {
                Debug.Log("While");
                Queue<Resource> freeResources = _scanner.Scan();
                Queue<Unit> freeUnits = FindFreeUnits();

                SendUnits(freeUnits, freeResources);
                yield return waitScanDelay;
            }
        }

        private void SendUnits(Queue<Unit> freeUnits, Queue<Resource> freeResources)
        {
            Debug.Log("SendUnits");
            while (freeResources.TryPeek(out Resource resource) && freeUnits.TryPeek(out Unit unit))
            {
                Debug.Log("Send While");
                if (IsResourceOrdered(resource))
                {
                    freeResources.Dequeue();
                    continue;
                }

                freeUnits.Dequeue();

                OrderResource(resource);
                unit.SetTarget(resource);
                unit.Run();
            }
        }

        private void OrderResource(Resource resource) =>
            _orderedResources.Add(resource);

        private bool IsResourceOrdered(Resource resource) =>
            _orderedResources.Contains(resource);

        private Queue<Unit> FindFreeUnits()
        {
            List<Unit> busyUnits = _units.Where(unit => unit.IsFree == false).ToList();
            List<Unit> freeUnits = _units.Where(unit => unit.IsFree).ToList();

            foreach (Unit busyUnit in busyUnits)
            {
                _units.Remove(busyUnit);
                _units.Add(busyUnit);
            }

            return freeUnits.Count > 0 ? new Queue<Unit>(freeUnits) : null;
        } 
    }
}
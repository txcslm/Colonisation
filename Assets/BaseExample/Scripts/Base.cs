using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FlagExample.Scripts;
using ResourceExample.Scripts;
using UnitExample.Scripts.Units;
using UnityEngine;

namespace BaseExample.Scripts
{ 
    [RequireComponent(typeof(UnitFabric), typeof(ResourceScanner), typeof(ResourceStorage))]
    [RequireComponent(typeof(FlagHolder))]
    public class Base : MonoBehaviour 
    {
        private const float MaxUnitsCount = 3;
        private const int UnitPrice = 3;
        private const int BasePrice = 5;

        private readonly List<Unit> _units = new List<Unit>();
        private readonly List<Resource> _orderedResources = new List<Resource>();

        private FlagHolder _flagHolder;
        private UnitFabric _fabric;
        private ResourceScanner _scanner;
        private ResourceStorage _resourceStorage;

        private Flag Flag => _flagHolder.Flag;

        private enum Status
        {
            Harvest,
            Build
        }

        private Status _currentStatus = Status.Harvest;

        private void Awake()
        {
            _flagHolder = GetComponent<FlagHolder>();
            _fabric = GetComponent<UnitFabric>();
            _scanner = GetComponent<ResourceScanner>();
            _resourceStorage = GetComponent<ResourceStorage>();
        }

        private void Start()
        {
            const int initialResourceCount = 9;
            
            _resourceStorage.IncreaseResourcesCount(initialResourceCount);
            Flag.Placed += OnFlagPlaced;
            StartCoroutine(ManageUnitsCoroutine());
        }

        private void OnDisable() =>
            Flag.Placed -= OnFlagPlaced;

        private void Update() => CheckResourceChange();

        private void OnFlagPlaced() => _currentStatus = Status.Build;

        private void CheckResourceChange()
        {
            if (_currentStatus == Status.Harvest)
            {
                if (_resourceStorage.CanPay(UnitPrice) && _units.Count < MaxUnitsCount)
                    BuyUnit();
            }
            else if (_currentStatus == Status.Build)
            {
                if (Flag != null && _resourceStorage.ResourcesCount >= BasePrice)
                    BuildNewBase();
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

            while (enabled)
            {
                Queue<Resource> freeResources = _scanner.Scan();
                Queue<Unit> freeUnits = FindFreeUnits();

                SendUnits(freeUnits, freeResources);
                yield return waitScanDelay;
            }
        }

        private void SendUnits(Queue<Unit> freeUnits, Queue<Resource> freeResources)
        {
            if (freeResources == null || freeUnits == null)
                return;
            
            while (freeResources.TryPeek(out Resource resource) && freeUnits.TryPeek(out Unit unit))
            {
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

        private void OrderResource(Resource resource) => _orderedResources.Add(resource);

        private bool IsResourceOrdered(Resource resource) => _orderedResources.Contains(resource);

        private Queue<Unit> FindFreeUnits()
        {
            List<Unit> busyUnits = _units.Where(unit => !unit.IsFree).ToList();
            List<Unit> freeUnits = _units.Where(unit => unit.IsFree).ToList();

            foreach (Unit busyUnit in busyUnits)
            {
                _units.Remove(busyUnit);
                _units.Add(busyUnit);
            }

            return freeUnits.Count > 0 ? new Queue<Unit>(freeUnits) : null;
        } 

        private void BuildNewBase()
        {
            if (_flagHolder.Flag.IsPlaced)
            {
                Vector3 flagPosition = _flagHolder.Flag.transform.position;
                Unit unit = _units.FirstOrDefault(u => u.IsFree);
                
                if (unit != null)
                {
                    unit.Build(flagPosition, Flag);
                    _resourceStorage.DecreaseResourceCount(BasePrice);
                }
            }
        }
    }
}
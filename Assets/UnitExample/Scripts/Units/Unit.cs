using System.Collections;
using BaseExample.Scripts;
using FlagExample.Scripts;
using ResourceExample.Scripts;
using UnityEngine;

namespace UnitExample.Scripts.Units
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private Transform _bag;
        [SerializeField] private float _speed;
        [SerializeField] private BaseFabric _baseFabric;
        
        private Resource _targetResource;
        private ResourceStorage _storage;
        private Base _base;

        public bool IsFree { get; private set; } = true;

        public void SetHome(Base @base) => _base = @base;

        public void SetTarget(Resource target) => _targetResource = target;

        public void Initialize(Base unitBase, ResourceStorage storage)
        {
            _storage = storage;
            _base = unitBase;
        }

        public void Run() => StartCoroutine(MoveToTargetCoroutine());

        public void Build(Vector3 position, Flag flag)
        {
            StartCoroutine(BuildCoroutine(position, flag));
        }

        private IEnumerator BuildCoroutine(Vector3 position, Flag flag)
        {
            yield return Move(position);
            flag.Hide();
            _baseFabric.Create(position);
            IsFree = true;
        }

        private void Take()
        {
            _targetResource.transform.SetParent(transform);
            _targetResource.transform.position = _bag.position;
        }

        private void Drop()
        {
            _storage.IncreaseResourcesCount(1);
            Destroy(_targetResource.gameObject);
        }

        private IEnumerator MoveToTargetCoroutine()
        {
            if (_targetResource == null)
                yield break;

            IsFree = false;
            yield return Move(_targetResource.transform.position);
            Take();
            IsFree = true;
            yield return Move(_base.transform.position);
            Drop();
        }

        private IEnumerator Move(Vector3 target)
        {
            while (Vector3.Distance(transform.position, target) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);
                yield return null;
            }
        }
    }
}
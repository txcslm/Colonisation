using System.Collections;
using BaseExample.Scripts;
using ResourceExample.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace UnitExample.Scripts.Units
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private Transform _bag;
        [SerializeField] private float _speed;
        
        private Resource _targetResource;
        private ResourceStorage _storage;
        private Base _base;

        public bool IsFree { get; private set; } = true;

        public void SetTarget(Resource target)
        {
            _targetResource = target;
        }

        public void Initialize(Base unitBase, ResourceStorage storage)
        {
            _storage = storage;
            _base = unitBase;
        }

        public void Run()
        {
            StartCoroutine(MoveToTargetCoroutine());
        }
        
        private void Take()
        {
            _targetResource.transform.SetParent(transform);
            _targetResource.transform.position = _bag.position;
        }

        private IEnumerator MoveToTargetCoroutine()
        {
            if (_targetResource == null)
                yield break;

            IsFree = false;
            yield return Move(_targetResource.transform);
            Take();
            IsFree = true;
            yield return Move(_base.transform);
            Drop();
        }

        private void Drop()
        {
            _storage.UpdateResourcesCount(1);
            
            Destroy(_targetResource.gameObject);
        }

        private IEnumerator Move(Transform target)
        {
            while (Vector3.Distance(transform.position, target.position) > Mathf.Epsilon)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
                yield return null;
            }
        }
    }
}

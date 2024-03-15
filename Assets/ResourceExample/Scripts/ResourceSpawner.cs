using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace ResourceExample.Scripts
{
	public class ResourceSpawner : MonoBehaviour
	{
		private const float Delay = 2f;

		[SerializeField] private Resource _resource;
		[SerializeField] private List<Transform> _points;

		private WaitForSeconds _waiting;

		private bool _isWork;

		private void Start()
		{
			_waiting = new WaitForSeconds(Delay);

			StartCoroutine(SpawnResourceCoroutine());
		}

		private void OnEnable() =>
			_isWork = true;

		private void OnDisable() =>
			_isWork = false;

		private IEnumerator SpawnResourceCoroutine()
		{
			while (_isWork)
			{
				Vector3 point = GetRandomPoint();
				Spawn(point);

				yield return _waiting;
			}
		}

		private Vector3 GetRandomPoint()
		{
			int index = Random.Range(0, _points.Count);

			return _points[index].position;
		}

		private void Spawn(Vector3 position) =>
			Object.Instantiate(_resource, position, Quaternion.identity);
	}
}
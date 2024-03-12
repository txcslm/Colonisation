using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace ResourceExample.Scripts
{
	public class ResourceSpawner : MonoBehaviour
	{
		private const float Delay = 2f;
		private const float TwoPI = 2f * Mathf.PI;

		[SerializeField] private Resource _resource;
		[SerializeField] private float _spawnRadius = 10f; 

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
			Vector3 randomPoint;

			do
			{
				float randomAngle = Random.Range(0f, TwoPI);

				float x = Mathf.Cos(randomAngle) * _spawnRadius;
				float z = Mathf.Sin(randomAngle) * _spawnRadius;

				Vector3 offset = new Vector3(x, 0f, z);

				randomPoint = transform.position + offset;
			}
			while (IsTooCloseToOtherResources(randomPoint));

			return randomPoint;
		}

		private bool IsTooCloseToOtherResources(Vector3 point)
		{
			float minDistance = 2.0f;
			
			return FindObjectsOfType<Resource>()
				.Select(existingResource => Vector3.Distance(existingResource.transform.position, point))
				.Any(distance => distance < minDistance);
		}

		private void Spawn(Vector3 position) =>
			Instantiate(_resource, position, Quaternion.identity);
	}
}
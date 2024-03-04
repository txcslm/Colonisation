	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using Unit = UnitExample.Scripts.Units.Unit;

	namespace CastleExample.Scripts
	{
		public class Base : MonoBehaviour
		{
			[SerializeField] [Range(0, 100)] private float _sphereRadius = 50f;
			[SerializeField] private List<Unit> _units;
			[SerializeField] private float _delay;
			[SerializeField] private LayerMask _resourceLayer;

			private WaitForSeconds _waiting;

			private bool _isWork;

			private void Start()
			{
				_isWork = true;
				_waiting = new WaitForSeconds(_delay);
				StartCoroutine(SearchResourceCoroutine());
			}

			private void OnDrawGizmos()
			{
				Gizmos.color = Color.red;

				Gizmos.DrawWireSphere(transform.position, _sphereRadius);
			}

			private IEnumerator SearchResourceCoroutine()
			{
				_units.ForEach(unit => unit.IsFree = true);

				while (_isWork)
				{
					Collider[] colliders = Physics.OverlapSphere(transform.position, _sphereRadius, _resourceLayer.value);

					if (colliders.Length > 0)
					{
						Debug.Log($"Found {colliders.Length} colliders.");
						
						Debug.Log("LetsGOOOOO!");

						for (int i = 0; i < _units.Count; i++)
						{
							if (i < colliders.Length && _units[i].IsFree)
							{
								_units[i].SetTarget(colliders[i].transform.position);
								_units[i].IsFree = false;
							}
							else
							{
								Debug.LogWarning("Not enough colliders for all free units!");
							}
						}
					}
					else
					{
						_units.ForEach(unit => unit.IsFree = true);
					}

					yield return _waiting;
				}
			}
		}
	}
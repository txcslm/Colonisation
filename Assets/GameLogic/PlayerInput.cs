using Constants;
using FlagExample.Scripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameLogic
{
	public class PlayerInput : MonoBehaviour
	{
		private const int LeftMouse = 0;

		[SerializeField] private Camera _camera;

		private FlagHolder _currentFlagHolder;

		private void Update()
		{
			if (Input.GetMouseButtonDown(LeftMouse))
			{
				Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

				if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
				{
					ProcessDepartmentCollision(hit);
					ProcessMapCollision(hit);
				}
			}
		}

		private void ProcessMapCollision(RaycastHit hit)
		{
			int objectLayer = 1 << hit.collider.gameObject.layer;

			if (objectLayer == Layers.PlaneMask && _currentFlagHolder != null)
			{
				_currentFlagHolder.Place(hit.point);
				_currentFlagHolder = null;
			}
		}

		private void ProcessDepartmentCollision(RaycastHit hit)
		{
			int objectLayer = 1 << hit.collider.gameObject.layer;

			if (objectLayer == Layers.BaseMask)
			{
				FlagHolder flagHolder = hit.collider.GetComponent<FlagHolder>();
				_currentFlagHolder = flagHolder;
			}
		}
	}
}
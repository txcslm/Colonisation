using Constants;
using FlagExample.Scripts;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
	private const int LeftMouse = 0;

	private Camera _mainCamera;
	private FlagHolder _currentFlagHolder;

	private void Awake()
	{
		_mainCamera = Camera.main;
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(LeftMouse))
		{
			Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D raycastHit2D = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
			ProcessDepartmentCollision(raycastHit2D);
			ProcessMapCollision(raycastHit2D);
		}
	}

	private void ProcessMapCollision(RaycastHit2D raycastHit2D)
	{
		int objectLayer = 1 << raycastHit2D.collider.gameObject.layer;

		if (objectLayer == Layers.PlaneMask && _currentFlagHolder != null)
		{
			_currentFlagHolder.Place(raycastHit2D.point);
			_currentFlagHolder = null;
		}
	}

	private void ProcessDepartmentCollision(RaycastHit2D raycastHit2D)
	{
		int objectLayer = 1 << raycastHit2D.collider.gameObject.layer;

		if (objectLayer == Layers.BaseMask)
		{
			FlagHolder flagHolder = raycastHit2D.collider.GetComponent<FlagHolder>();
			_currentFlagHolder = flagHolder;
		}
	}
}
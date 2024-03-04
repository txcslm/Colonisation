using System;
using ResourceExample.Scripts;
using UnitExample.Scripts.Units.StateMachine.Payloads;
using UnityEngine;

namespace UnitExample.Scripts.Units.StateMachine.States
{
	public class CollectResourceState : IParameterState
	{
		private readonly IStateSwitcher _stateSwitcher;
		private readonly Unit _unit;
		private Resource _currentResource;

		public CollectResourceState(IStateSwitcher stateSwitcher, Unit unit)
		{
			_stateSwitcher = stateSwitcher ?? throw new ArgumentNullException(nameof(stateSwitcher));
			_unit = unit ?? throw new ArgumentNullException(nameof(unit));
		}

		public void Enter<TPayload>(TPayload payload) where TPayload : IPayload
		{
			if (payload is Vector3Payload vector3Payload)
			{
				Debug.Log("Enter: CollectResourceState");
				_unit.SetTarget(vector3Payload.Position);
				Resource currentResource = _unit.GetCurrentResource();
				AttachResourceToUnit(currentResource);
				_stateSwitcher.SwitchState<MovementState>(new Vector3Payload(_unit.Target, true));
			}
			else
			{
				Debug.LogError("CollectResourceState Enter: Unexpected payload type or payload is null!");
			}
		}

		private void AttachResourceToUnit(Resource currentResource)
		{
			if (currentResource != null)
			{
				Debug.Log("Attach");
				currentResource.transform.parent = _unit.transform;
				currentResource.transform.localPosition = new Vector3(0, 3, 0);
			}
		}

		public void Exit() { }

		public void Update(float deltaTime) { }
	}
}
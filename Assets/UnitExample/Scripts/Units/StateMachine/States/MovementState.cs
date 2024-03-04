using UnitExample.Scripts.Units.StateMachine.Payloads;
using UnityEngine;

namespace UnitExample.Scripts.Units.StateMachine.States
{
	public class MovementState : IParameterState
	{
		private readonly IStateSwitcher _stateSwitcher;

		private Unit _unit;
		private float _speed = 10f;
		private Vector3Payload _targetInfo;

		public MovementState(IStateSwitcher stateSwitcher, Unit unit)
		{
			_stateSwitcher = stateSwitcher;
			_unit = unit;
		}

		public void Exit() { }

		public void Update(float deltaTime)
		{
			Debug.Log($"MovementState: Update! Target position: {_targetInfo.Position}");
			_unit.transform.position = Vector3.MoveTowards(_unit.transform.position, _targetInfo.Position, deltaTime * _speed);

			if (Vector3.Distance(_unit.transform.position, _targetInfo.Position) <= 1f)
				_stateSwitcher.SwitchState<AchieveTargetState>(_targetInfo);
		}

		public void Enter<TPayload>(TPayload payload) where TPayload : IPayload
		{
			Debug.Log($"MovementState: Enter {_targetInfo.Position}");
			
			if (payload is Vector3Payload targetInfo)
			{
				_targetInfo = targetInfo;
			}
		}
	}
}
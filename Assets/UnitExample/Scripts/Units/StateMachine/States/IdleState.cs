using System;
using UnitExample.Scripts.Units.StateMachine.Payloads;
using UnityEngine;

namespace UnitExample.Scripts.Units.StateMachine.States
{
		public class IdleState : IState
		{
			private readonly IStateSwitcher _stateSwitcher;
			private readonly Unit _unit;

			public IdleState(IStateSwitcher stateSwitcher, Unit unit)
			{
				_stateSwitcher = stateSwitcher ?? throw new ArgumentNullException(nameof(stateSwitcher));
				_unit = unit ?? throw new ArgumentNullException(nameof(unit));
			}

			public void Update(float deltaTime)
			{
				if (_unit.IsFree && _unit.Target != Vector3.zero)
				{
					_unit.IsFree = false;
					Debug.Log($" Target pos: {_unit.Target}");
					_stateSwitcher.SwitchState<MovementState>(new Vector3Payload(_unit.Target, false));
				}
			}

			public void Enter()
			{
				Debug.Log("IdleState : Enter");
			}

			public void Exit() =>
				Debug.Log("IdleState: Exit");
		}
}
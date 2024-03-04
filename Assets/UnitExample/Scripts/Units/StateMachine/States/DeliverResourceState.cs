using System;
using UnitExample.Scripts.Units.StateMachine.Payloads;

namespace UnitExample.Scripts.Units.StateMachine.States
{
	public class DeliverResourceState : IState
	{
		private readonly IStateSwitcher _stateSwitcher;
		private readonly Unit _unit;

		public DeliverResourceState(IStateSwitcher stateSwitcher, Unit unit)
		{
			_stateSwitcher = stateSwitcher ?? throw new ArgumentNullException(nameof(stateSwitcher));
			_unit = unit ?? throw new ArgumentNullException(nameof(unit));
		}

		public void Exit()
		{
			
		}

		public void Update(float deltaTime)
		{
			
		}

		public void Enter()
		{
			
			_stateSwitcher.SwitchState<MovementState>(new Vector3Payload(_unit.Target, false));
		}
	}
}
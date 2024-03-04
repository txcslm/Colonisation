using UnitExample.Scripts.Units.StateMachine.Payloads;

namespace UnitExample.Scripts.Units.StateMachine.States
{
	public class AchieveTargetState : IParameterState
	{
		private readonly IStateSwitcher _stateSwitcher;
		private IPayload _payload;

		public AchieveTargetState(IStateSwitcher stateSwitcher)
		{
			_stateSwitcher = stateSwitcher;
		}

		public void Exit() { }

		public void Update(float deltaTime) { }

		public void Enter<TPayload>(TPayload payload) where TPayload : IPayload
		{
			_payload = payload;

			if (_payload is Vector3Payload { IsBase: false })
				_stateSwitcher.SwitchState<CollectResourceState>(_payload);
			else
				_stateSwitcher.SwitchState<DeliverResourceState>();
		}
	}
}
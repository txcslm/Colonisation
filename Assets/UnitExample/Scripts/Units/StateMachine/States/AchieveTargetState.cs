using UnitExample.Scripts.Units.StateMachine.Payloads;

namespace UnitExample.Scripts.Units.StateMachine.States
{
	public class AchieveTargetState : IParametrState
	{
		private readonly IStateSwitcher _stateSwitcher;

		public AchieveTargetState ( IStateSwitcher stateSwitcher)
		{
			_stateSwitcher = stateSwitcher;
		}

		public void Exit() { }

		public void Update(float deltaTime) { }

		public void Enter<TPayload>(TPayload payload) where TPayload : IPayload
		{
			if (payload is Vector3Payload target == false)
				return;

			if (target.IsBase)
				_stateSwitcher.SwitchState<DeliverResourceState>();
			else
				_stateSwitcher.SwitchState<CollectResourceState>();
		}
	}
}
using UnitExample.Scripts.Units.StateMachine.Payloads;
using UnitExample.Scripts.Units.StateMachine.States;

namespace UnitExample.Scripts.Units.StateMachine
{
	public interface IStateSwitcher
	{
		void SwitchState<TState>() where TState : IUpdatable;

		void SwitchState<TParametrState>(IPayload payload) where TParametrState : IUpdatable;
	}
}
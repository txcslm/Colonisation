using UnityEngine;

namespace UnitExample.Scripts.Units.StateMachine.States
{
	public interface IParametrState : IUpdatable

	{
	void Enter<TPayload>(TPayload payload) where TPayload : IPayload;
	}
}
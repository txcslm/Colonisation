using UnitExample.Scripts.Units.StateMachine.Payloads;
using UnityEngine;

namespace UnitExample.Scripts.Units.StateMachine.States
{
	public interface IParameterState : IUpdatable

	{
	void Enter<TPayload>(TPayload payload) where TPayload : IPayload;
	}
}
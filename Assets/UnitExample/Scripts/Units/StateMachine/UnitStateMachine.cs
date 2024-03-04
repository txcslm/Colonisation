using System;
using System.Collections.Generic;
using UnitExample.Scripts.Units.StateMachine.States;
using UnityEngine;

namespace UnitExample.Scripts.Units.StateMachine
{
	public class UnitStateMachine : IStateSwitcher
	{
		private Dictionary<Type, IUpdatable> _states;
		private IUpdatable _currentState;

		public UnitStateMachine(Unit unit)
		{
			_states = new Dictionary<Type, IUpdatable>()
			{
				[typeof(IdleState)] = new IdleState(this, unit),
				[typeof(MovementState)] = new MovementState(this, unit),
				[typeof(CollectResourceState)] = new CollectResourceState(this, unit),
				[typeof(DeliverResourceState)] = new DeliverResourceState(this, unit),
				[typeof(AchieveTargetState)] = new AchieveTargetState(this)
			};
		}
		
		public void Update(float deltaTime) => 
			_currentState.Update(deltaTime);

		public void SwitchState<TState>() where TState : IUpdatable
		{
			if (GetNewState<TState>())
				return;

			if (_currentState is IState state)
				state.Enter();
		}

		public void SwitchState<TParametrState>(IPayload payload) where TParametrState : IUpdatable
		{
			if (GetNewState<TParametrState>())
				return;

			if (_currentState is IParametrState state)
				state.Enter(payload);
		}

		private bool GetNewState<TParametrState>() where TParametrState : IUpdatable
		{
			if (_states.TryGetValue(typeof(TParametrState), out IUpdatable newState) == false)
				return true;

			_currentState?.Exit();
			_currentState = newState;
			return false;
		}

	}
}
using System;
using UnityEngine;

namespace UnitExample.Scripts.Units.StateMachine.States
{
	public class CollectResourceState : IState
	{
		public CollectResourceState(IStateSwitcher stateSwitcher, Unit unit)
		{
		}

		public void Enter()
		{
			Debug.Log(GetType());
		}

		public void Exit()
		{
		}

		public void Update(float deltaTime) { }
	}
}
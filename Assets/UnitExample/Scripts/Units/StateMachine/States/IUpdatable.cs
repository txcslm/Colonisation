namespace UnitExample.Scripts.Units.StateMachine.States
{
	public interface IUpdatable : IExitable
	{
		void Update(float deltaTime);
	}
}
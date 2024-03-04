using ResourceExample.Scripts;
using UnitExample.Scripts.Units.StateMachine.States;

namespace UnitExample.Scripts.Units.StateMachine.Payloads
{
	public struct ResourcePayload : IPayload
	{
		public Resource Resource { get; }

		public ResourcePayload(Resource resource)
		{
			Resource = resource;
		}
	}
}
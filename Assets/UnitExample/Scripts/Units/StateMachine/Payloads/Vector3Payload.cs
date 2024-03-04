
using ResourceExample.Scripts;
using UnitExample.Scripts.Units.StateMachine.States;
using UnityEngine;

namespace UnitExample.Scripts.Units.StateMachine.Payloads
{
	public struct Vector3Payload : IPayload
	{
		public Vector3 Position { get; }
		public bool IsBase { get; }
		public Resource Resource { get; }
		
		public Vector3Payload(Vector3 position, bool isBase)
		{
			Position = position;
			IsBase = isBase;
			Resource = null;
		}
		
		public Vector3Payload(Vector3 position, bool isBase, Resource resource)
		{
			Position = position;
			IsBase = isBase;
			Resource = resource;
		}
	}
}
using BaseExample.Scripts;
using UnityEngine;

namespace Constants
{
	public static class Layers
	{
		
		private readonly static int Resource = LayerMask.NameToLayer("Resource");
		private readonly static int Plane = LayerMask.NameToLayer("Plane");
		private readonly static int Base = LayerMask.NameToLayer("Base");
		
		public static int ResourceMask { get; } = 1 << Resource;
		public static int PlaneMask { get; } = 1 << Plane;
		public static int BaseMask { get; } = 1 << Base;
	}
}
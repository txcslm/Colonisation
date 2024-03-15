using BaseExample.Scripts;
using UnityEngine;

namespace Constants
{
	public static class Layers
	{
		public readonly static int ResourceMask = 1 << Resource;
		public readonly static int PlaneMask = 1 << Plane;
		public readonly static int BaseMask = 1 << Base;
		
		private readonly static int Resource = LayerMask.NameToLayer("Resource");
		private readonly static int Plane = LayerMask.NameToLayer("Plane");
		private readonly static int Base = LayerMask.NameToLayer("Base");
	}
}
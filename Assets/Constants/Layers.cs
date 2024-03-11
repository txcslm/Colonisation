using UnityEngine;

namespace Constants
{
	public static class Layers
	{
		public readonly static int Resource = LayerMask.NameToLayer("Resource");
		public readonly static int ResourceMask = 1 << Resource;
	}
}
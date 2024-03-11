using UnityEngine;

namespace ResourceExample.Scripts
{
	public class Resource : MonoBehaviour
	{
		public bool IsOrdered { get; private set; }

		public void SetOrderedStatus()
		{
			IsOrdered = true;
		}
	}
}
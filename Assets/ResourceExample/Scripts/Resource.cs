using UnityEngine;

namespace ResourceExample.Scripts
{
	public class Resource : MonoBehaviour
	{
		[field: SerializeField] public int Value { get; } = 1;
	}
}
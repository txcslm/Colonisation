using UnityEngine;

namespace BaseExample.Scripts
{
	public class BaseFabric : MonoBehaviour
	{
		[SerializeField] private Base _basePrefab;

		public Base Create(Vector3 position)
		{
			return Instantiate(_basePrefab, position, Quaternion.identity);
		}
	}
}
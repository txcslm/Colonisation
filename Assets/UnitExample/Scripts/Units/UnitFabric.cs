using UnityEngine;

namespace UnitExample.Scripts.Units
{
	public class UnitFabric : MonoBehaviour
	{
		[SerializeField] private Unit _unitPrefab;

		public Unit SpawnUnit()
		{
			return Instantiate(_unitPrefab, transform.position, Quaternion.identity);
		}
	}
}
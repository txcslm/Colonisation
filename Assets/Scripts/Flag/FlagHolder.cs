using UnityEngine;

namespace FlagExample.Scripts
{
	public class FlagHolder : MonoBehaviour
	{
		[SerializeField] private Flag flagPrefab;

		public Flag Flag { get; private set; }

		private void Awake()
		{
			Flag = Instantiate(flagPrefab, transform);
			Flag.gameObject.SetActive(false);
		}

		public void Place(Vector3 position)
		{
			Flag.transform.position = position;

			if (Flag.IsPlaced == false)
			{
				Flag.SetPlaced();
				Flag.gameObject.SetActive(true);
			}
		}
	}
}
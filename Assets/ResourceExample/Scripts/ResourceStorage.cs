using TMPro;
using UnityEngine;

namespace ResourceExample.Scripts
{
	public class ResourceStorage : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _resourcesCountTMP;
		
		public int ResourcesCount { get; private set; }

		public void UpdateResourcesCount(int amount)
		{
			ResourcesCount += amount;
			
			_resourcesCountTMP.text = ResourcesCount.ToString();
		}
	}
}
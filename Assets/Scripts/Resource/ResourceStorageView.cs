using System;
using TMPro;
using UnityEngine;

namespace ResourceExample.Scripts
{
	public class ResourceStorageView : MonoBehaviour
	{
		private TextMeshProUGUI _resourcesCountTMP;
		private ResourceStorage _storage;

		private void Awake()
		{
			_resourcesCountTMP = GetComponent<TextMeshProUGUI>();
			_storage = GetComponentInParent<ResourceStorage>();
			
			_storage.ResourcesCountChanged += ChangeValue;
		}

		private void OnDestroy() =>
			_storage.ResourcesCountChanged -= ChangeValue;

		private void ChangeValue() =>
			_resourcesCountTMP.text = _storage.ResourcesCount.ToString();
	}
}
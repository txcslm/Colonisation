using System;
using UnityEngine;

namespace FlagExample.Scripts
{
	public class Flag : MonoBehaviour
	{
		public event Action Placed;

		public bool IsPlaced { get; private set; }

		public void SetPlaced()
		{
			IsPlaced = true;
			Placed?.Invoke();
		}

		public void Hide()
		{
			gameObject.SetActive(false);
			IsPlaced = false;
		}
	}
}
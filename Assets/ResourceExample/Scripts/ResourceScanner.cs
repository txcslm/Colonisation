using System.Collections.Generic;
using Constants;
using UnityEngine;

namespace ResourceExample.Scripts
{
	public class ResourceScanner : MonoBehaviour
	{
		[SerializeField] [Range(0, 100)] private float _sphereRadius = 50f; 
		[SerializeField] private float _scanDelay;

		public float ScanDelay => _scanDelay;
		
		private void OnDrawGizmos()                                           
		{                                                                     
			Gizmos.color = Color.red;                                         
                                                                              
			Gizmos.DrawWireSphere(transform.position, _sphereRadius);         
		}                                                                     
		
		public Queue<Resource> Scan()
		{
			Debug.Log("Scan resources");
			Collider[] colliders = Physics.OverlapSphere(transform.position, _sphereRadius, Layers.ResourceMask);
			
			Queue<Resource> resources = new Queue<Resource>();

			foreach (Collider collider in colliders)
			{
				Debug.Log("Resource scan foreach: " + resources.Count);
				
				if (collider.TryGetComponent(out Resource resource))
				{
					Debug.Log("Resource scan: " + resources.Count);
					resources.Enqueue(resource);
				}
			}

			return resources;
		}
	}			
}
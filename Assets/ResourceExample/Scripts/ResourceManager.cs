using ResourceExample.Scripts;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
	private Resource _currentResource;

	public void SetCurrentResource(Resource resource)
	{
		_currentResource = resource;
	}

	public Resource GetCurrentResource()
	{
		return _currentResource;
	}

	public void ClearCurrentResource()
	{
		_currentResource = null;
	}
}
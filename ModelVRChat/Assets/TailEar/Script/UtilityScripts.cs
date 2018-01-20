using UnityEngine;
using System.Collections;

public static class UtilityScripts {

	public static Component FindComponentInChildWithName<T>( Transform parentTransform, string refName ) where T : Component
	{
		Transform[] allTransforms = parentTransform.GetComponentsInChildren<Transform>();
		
		foreach( Transform tempTransform in allTransforms )
		{
			if( tempTransform.name.Contains(refName) )
			{
				Component tempComponent = tempTransform.GetComponent<T>();
				if(tempComponent) return tempComponent;
			}
		}
		
		return null;
	}

	public static Transform FindChildWithName( Transform parentTransform, string refName )
	{
		Transform[] allTransforms = parentTransform.GetComponentsInChildren<Transform>();
		
		foreach( Transform tempTransform in allTransforms )
		{
			if( tempTransform.name.Contains(refName) )
			{
				return tempTransform;
			}
		}
		
		return null;
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyCanvas : MonoBehaviour 
{
	public static DontDestroyCanvas instance;

	void Awake()
	{
		if (instance != null)
		{
			Destroy (gameObject);
		} 
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

}

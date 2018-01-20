using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour 
{

	private static bool cameraExists;

	void Start () 
	{
		if (!cameraExists) 
		{
			cameraExists = true;
			DontDestroyOnLoad (transform.gameObject);
		} 
		else
			Destroy (gameObject);
	}
}

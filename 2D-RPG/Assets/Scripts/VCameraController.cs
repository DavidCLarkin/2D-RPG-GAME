using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VCameraController : MonoBehaviour 
{
	public static bool vCameraExists;

	void Start () 
	{
		if (!vCameraExists) 
		{
			vCameraExists = true;
			DontDestroyOnLoad (transform.gameObject);
		} 
		else
			Destroy (gameObject);
	}
}

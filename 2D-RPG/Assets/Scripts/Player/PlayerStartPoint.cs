using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartPoint : MonoBehaviour 
{
	private PlayerController player;
	private CameraController camera;
	private VCameraController vCamera;

	public string pointName;

	// Use this for initialization
	void Start () 
	{
		player = FindObjectOfType<PlayerController>();

		if (player.startPoint == pointName) 
		{
			player.transform.position = transform.position;

			camera = FindObjectOfType<CameraController>();
			camera.transform.position = new Vector2 (transform.position.x, transform.position.y);

			vCamera = FindObjectOfType<VCameraController>();
			vCamera.transform.position = new Vector2 (transform.position.x, transform.position.y);
		}
	}
}

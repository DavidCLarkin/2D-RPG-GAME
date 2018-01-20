using UnityEngine;
using System.Collections;

public class AutoRotateCharacter: MonoBehaviour {

	[System.NonSerialized]
	public Transform thisTransform;

	[System.NonSerialized]
	public float currentRotVal = 45.0f;

	[System.NonSerialized]
	public float rotVal = 0.0f;

	Vector3 rotation = new Vector3();

	// Use this for initialization
	void Start () {
	
		thisTransform = this.transform;

	}
	
	// Update is called once per frame
	void LateUpdate () {
	
		thisTransform.eulerAngles = new Vector3(0,currentRotVal,0);

		rotation = new Vector3(0,rotVal,0);

		thisTransform.Rotate(rotation);
	}

}

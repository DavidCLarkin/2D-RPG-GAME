using UnityEngine;
using System.Collections;

public class SampleMotionControl : MonoBehaviour {

	Animator anim;

	[Range(0,10)]
	public int motionIndex = 10;

	Transform thisTransform;

	// Use this for initialization
	void Start ()
	{
		anim = GetComponent<Animator> ();
		thisTransform = transform;
	}
	
	// Update is called once per frame
	void LateUpdate ()
	{
		if( !anim ) return;

		ClampMotionIndex();
		SetMotionIndex();
	}


	void ClampMotionIndex()
	{
		if (motionIndex < 1)						
		{
			motionIndex = 1;
			return;
		}

		if (motionIndex > 10) motionIndex = 10; 
	}

	public void SetMotionIndex()
	{
		anim.SetInteger ( "MotionIndex", motionIndex  );
		thisTransform.position = Vector3.zero;
	}

	//////////////////////////////////////////////////

}



















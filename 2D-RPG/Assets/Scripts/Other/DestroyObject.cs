using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public float timeToDestroy;

	void Start ()
    {
        Destroy(gameObject, timeToDestroy);
	}
	
}

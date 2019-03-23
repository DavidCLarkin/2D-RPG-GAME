using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateAfterTime : MonoBehaviour
{
    public float timeToShowFor;
    private float timer;
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;
        if (timer >= timeToShowFor)
        {
            timer = 0;
            gameObject.SetActive(false);
        }
	}
}

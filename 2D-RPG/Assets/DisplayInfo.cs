using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayInfo : MonoBehaviour 
{
    public Transform obj;
    private Vector2 actualPosition;
    public Camera cam; // set in inspector

    void Update () 
	{
        if (GameManagerSingleton.instance.tooltip.IsActive())
        {
            transform.position = actualPosition;
        }
	}

    //Convert object position to screen space
    public void SetPosition(Transform trans)
    {
        actualPosition = cam.WorldToScreenPoint(trans.position);
    }

}

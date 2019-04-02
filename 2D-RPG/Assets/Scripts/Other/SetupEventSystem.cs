using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SetupEventSystem : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        GameManagerSingleton.instance.GetComponent<HandleButtonEvents>().eventSystem = 
            GameObject.FindGameObjectWithTag("EventSystem").GetComponent<EventSystem>();

    }

}

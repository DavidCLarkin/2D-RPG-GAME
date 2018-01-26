using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManagerSingleton : MonoBehaviour 
{
	public static GameManagerSingleton instance;

	//Objects
	public Text tooltip;
	public GameObject player;

	private void Awake()
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadLevel : MonoBehaviour 
{
	public string levelToLoad;
	public string exitPoint;

	private PlayerController player;
	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindObjectOfType<PlayerController>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.name == "Player") 
		{
			SceneManager.LoadScene (levelToLoad);
			player.startPoint = exitPoint;
		}
	}
}

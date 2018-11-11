using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadLevel : MonoBehaviour 
{
	public string levelToLoad;
	public string exitPoint;

	private Player player;

	// Use this for initialization
	void Start () 
	{
		player = GameManagerSingleton.instance.player.GetComponent<Player>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player") 
		{
			SceneManager.LoadScene (levelToLoad);
			player.startPoint = exitPoint;
		}
	}
}

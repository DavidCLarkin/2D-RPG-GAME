using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour {

    public Text healthText;
	// Use this for initialization
	void Start () {
        healthText = GameObject.Find("HealthText").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        healthText.text = "Health: " + GameManagerSingleton.instance.player.GetComponent<PlayerController>().health;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplay : MonoBehaviour
{

    public Text healthText;
    public Text staminaText;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update ()
    {
        healthText.text = "Health: " + GameManagerSingleton.instance.player.GetComponent<HealthComponent>().health;
        staminaText.text = "Stamina: " + GameManagerSingleton.instance.player.GetComponent<StaminaComponent>().stamina;
	}
}

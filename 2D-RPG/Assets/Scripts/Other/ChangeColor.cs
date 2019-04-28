using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public Color colorToChangeTo;
    private HealthComponent health;
	// Use this for initialization
	void Start ()
    {
        health = GetComponent<HealthComponent>();	
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Change color if half health
        if (health.health <= health.maxHealth / 2)
            GetComponent<SpriteRenderer>().color = colorToChangeTo;
	}
}

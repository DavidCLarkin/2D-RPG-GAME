using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StaminaComponent : MonoBehaviour 
{
	public float maxStamina;
	public float stamina;

	private InputComponent input;
	public event Action OnUse = delegate { };

	void Awake()
	{
		stamina = maxStamina;
		input = GetComponent<InputComponent> ();
	}

	void Update()
	{
		for(int i = 0; i < input.keys.Length; i++)
		{
			if (input.Dodge && Input.GetKey(input.keys[i])) // need to not allow dodging if stamina too low
				UseStamina ();
		}
	}
		
	public void UseStamina()
	{
		if (stamina >= 30f) 
		{
			stamina -= 30f;
		}
	}
}

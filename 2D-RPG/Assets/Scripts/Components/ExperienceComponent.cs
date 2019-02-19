using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ExperienceComponent : MonoBehaviour 
{
	public int level = 1;
	public int currentExp = 0;
	public int baseExp = 100;
	public int expToLevel = 100;
	public float expIncrease = 1.7f;

	public Slider expBar;

	void Start()
	{
		expBar.value = CalculateExpPercentage ();
	}

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.N)) 
		{
			IncreaseExp (50);
		}
	}

	void IncreaseExp(int exp)
	{
		currentExp += exp;
		if (currentExp >= expToLevel) 
		{
			LevelUp();
		}

		expBar.value = CalculateExpPercentage ();
	}

	void LevelUp()
	{
		currentExp -= expToLevel;
		level++;
		expToLevel = (int)(baseExp * (Mathf.Pow (expIncrease, level)));
	}

	float CalculateExpPercentage()
	{
		Debug.Log (currentExp / baseExp);
		return (float)currentExp / expToLevel;
	}
}

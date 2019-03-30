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
    public int totalExp;

	public Slider expBar;
    public Text currentExpDisplay;
    private Text expText;

    private string expString;

	void Start()
	{
        expText = expBar.GetComponentInChildren<Text>();
        UpdateExpUI();
        StartCoroutine(FrequentUpdateExp());
    }

    // Update is called once per frame
    void Update () 
	{
		if (Input.GetKeyDown (KeyCode.N)) 
		{
			IncreaseExp (500);
		}

    }

	public void IncreaseExp(int exp)
	{
		currentExp += exp;
        totalExp += exp;
		if (currentExp >= expToLevel) 
		{
			LevelUp();
		}

        UpdateExpUI();
    }

	void LevelUp()
	{
		currentExp -= expToLevel;
		level++;
		expToLevel = (int)(baseExp * (Mathf.Pow (expIncrease, level)));
        UpdateExpUI();
    }

	float CalculateExpPercentage()
	{
		return (float)currentExp / expToLevel;
	}

    IEnumerator FrequentUpdateExp()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.25f);
            UpdateExpUI();
        }
    }

    void UpdateExpUI()
    {
        expText.text = totalExp.ToString();
    }

    // Whenever enabled, restart
    private void OnEnable()
    {
        StartCoroutine(FrequentUpdateExp());
    }
}

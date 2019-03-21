using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FontAnimation : MonoBehaviour
{
    private Text text;
    public Color baseColor;
    public Color newColor;
    public float timer;

    void Start ()
    {
        text = GetComponent<Text>();
        StartCoroutine(FadeOut());
	}
	
	// Update is called once per frame
	void Update ()
    {
        /*
        if (text.color.a > 0.4)
        {
            text.color = Color.Lerp(newColor, newColor, 0.001f);
        }
        else if (text.color.a <= 0.4)
        {
            text.color = Color.Lerp(baseColor, baseColor, 0.001f);
        }
        */
    }

    //note the change from 'void' to 'IEnumerator'
    IEnumerator FadeOut()
    {
        //ugly while, Update would be ideal
        while(true)
            if (text.color.a > 0.4)
            {
                text.color = Color.Lerp(baseColor, newColor, 2);
                yield return new WaitForSeconds(1.5f);
            }
            else if (text.color.a <= 0.4)
            {
                text.color = Color.Lerp(newColor, baseColor, 2);
                yield return new WaitForSeconds(1.5f);
            }
        //code after fading is finished
    }
}

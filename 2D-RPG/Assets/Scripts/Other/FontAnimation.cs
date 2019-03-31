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
	

    IEnumerator FadeOut()
    {
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
    }
}

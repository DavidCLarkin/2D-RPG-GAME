using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LerpColors : MonoBehaviour
{

    public Color lerpedColor;
    float timer = 0;

    void Update()
    {
        timer += Time.deltaTime / 2.0f;
        GetComponent<TextMeshProUGUI>().color = Color.Lerp(Color.red, Color.black, timer);
    }

    private void OnEnable()
    {
        timer = 0; // reset timer so it can lerp again
    }
}

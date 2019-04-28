using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class HandleButtonEvents : MonoBehaviour
{
    public EventSystem eventSystem;
    public GameObject[] upgradeButtonList;
    public GameObject[] pauseButtonList;

    private int upgradeBtnIndex = 0;
    private int pauseBtnIndex = 0;

    private float selectButtonDelay = 0.25f;
    private float btnDelayTimer;
	
	// Update is called once per frame
	void Update ()
    {
        if (btnDelayTimer > 0)
            btnDelayTimer -= Time.unscaledDeltaTime;

        if (btnDelayTimer <= 0)
        {
            if (Input.GetAxisRaw("Vertical") < 0)
            {
                if (upgradeBtnIndex != upgradeButtonList.Length - 1)
                    upgradeBtnIndex++;
                else
                    upgradeBtnIndex = 0;
                btnDelayTimer = selectButtonDelay;
            }
            else if (Input.GetAxisRaw("Vertical") > 0)
            {
                if (upgradeBtnIndex != 0)
                    upgradeBtnIndex--;
                else
                    upgradeBtnIndex = upgradeButtonList.Length - 1;
                btnDelayTimer = selectButtonDelay;
            }

            if (Input.GetAxisRaw("Vertical") < 0)
            {
                if (pauseBtnIndex != pauseButtonList.Length - 1)
                    pauseBtnIndex++;
                else
                    pauseBtnIndex = 0;
                btnDelayTimer = selectButtonDelay;
            }
            else if (Input.GetAxisRaw("Vertical") > 0)
            {
                if (pauseBtnIndex != 0)
                    pauseBtnIndex--;
                else
                    pauseBtnIndex = pauseButtonList.Length - 1;
                btnDelayTimer = selectButtonDelay;
            }


            if (pauseButtonList[pauseBtnIndex].gameObject.name == "VolumeSlider")
            {
                if (Input.GetAxis("DPadHorizontal") == 1)
                    pauseButtonList[pauseButtonList.Length - 1].GetComponent<Slider>().value += 0.5f;
                if (Input.GetAxis("DPadHorizontal") == -1)
                    pauseButtonList[pauseButtonList.Length - 1].GetComponent<Slider>().value -= 0.5f;
            }

        }

        if (SceneManager.GetActiveScene().name == "Hub" || SceneManager.GetActiveScene().name == "Tutorial")
        {
            if (GameManagerSingleton.instance.statVendor.panelOpen)
            {
                eventSystem.SetSelectedGameObject(upgradeButtonList[upgradeBtnIndex]);
            }
        }

        if(GameManagerSingleton.instance.pausePanel.activeSelf)
        {
            eventSystem.SetSelectedGameObject(pauseButtonList[pauseBtnIndex]);
        }

	}

    public void OnButtonSelected(Button button)
    {
        button.GetComponentInChildren<Text>().color = Color.white;
    }

    public void OnButtonDeselect(Button button)
    {
        button.GetComponentInChildren<Text>().color = Color.black;
    }
}

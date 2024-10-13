using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject objUISetting;

    public Animator animUIInventory;
    public Animator animUImap;
    public bool isInventoryOpened;
    public bool isMapOpened;
    public GameObject objGameOverUI;


    public TextMeshProUGUI objPlayerNameText;

    public void Update()
    {
        objPlayerNameText.text = PlayerNameManager.playerName;
    }

    public void OpenSettingUI()
    {
        objUISetting.SetActive(true);
    }

    public void CloseSettingUI()
    {
        objUISetting.SetActive(false);
    }

    public void ToggleInventoryUI()
    {
        isInventoryOpened = !isInventoryOpened;

        if(isInventoryOpened == true)
        {
            animUIInventory.SetBool("isInventoryOpened", true);
        }
        else if(isInventoryOpened == false)
        {
            animUIInventory.SetBool("isInventoryOpened", false);
        }
    }

    public void ToggMapUI()
    {
        isMapOpened = !isMapOpened;

        if (isMapOpened == true)
        {
            animUImap.SetBool("isMapOpened", true);
        }
        else if (isMapOpened == false)
        {
            animUImap.SetBool("isMapOpened", false);
        }
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("Scene_Main");
        //objGameOverUI.SetActive(false);
    }

    public void DeadButton()
    {
        SceneManager.LoadScene("Scene_Start");
    }

}

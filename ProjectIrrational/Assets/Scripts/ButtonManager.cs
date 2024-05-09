using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public GameObject objUISetting;

    public Animator animUIInventory;
    public bool isInventoryOpened;

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

}

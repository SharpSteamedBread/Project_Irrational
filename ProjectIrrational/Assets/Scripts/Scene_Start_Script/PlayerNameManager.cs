using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerNameManager : MonoBehaviour
{
    public TMP_InputField playerNameField;

    public static string playerName = "defaultName";

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SavePlayerName()
    {
        if(playerNameField.text.Length <= 15)
        {
            playerName = playerNameField.text;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MapSwitcher : MonoBehaviour
{
    [Header("이야기 진행도 초기화")]
    [SerializeField] private ShowTextJson showTextJson;

    private bool click = false;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GotoDefault()
    {
        //InitValue();
        SceneManager.LoadScene("Scene_Main");
    }

    public void GotoZehupe()
    {
        if(click == true)
        {
            return;
        
        }

        //InitValue();
        click = true;
        SceneManager.LoadScene("Scene_Zehupe");
    }

    public void GotoReasercharea1()
    {
        if (click == true)
        {
            return;

        }

        //InitValue();
        click = true;
        SceneManager.LoadScene("Scene Reaserch area1");
    }

    public void InitValue()
    {
        showTextJson.currentDialogIndex = 0;
        showTextJson.currentEventPath = 0;
        showTextJson.hasSelectedText = 0;
    }
}

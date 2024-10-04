using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MapSwitcher : MonoBehaviour
{
    [Header("이야기 진행도 초기화")]
    [SerializeField] private ShowTextJson showTextJson;

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
        //InitValue();
        SceneManager.LoadScene("Scene_Zehupe");
    }

    public void InitValue()
    {
        showTextJson.currentDialogIndex = 0;
        showTextJson.currentEventPath = 0;
        showTextJson.hasSelectedText = 0;
    }
}

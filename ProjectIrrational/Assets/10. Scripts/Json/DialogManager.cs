using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DialogManager : MonoBehaviour
{
    [Header("기본 지역")]
    public TextAsset dialog01File;
    public TextAsset selectText01File;
    public TextAsset randomEvent01File;
    public TextAsset itemSheetFile;

    [Space(40)]

    public DialogList dialogList01;
    public Selection01List selectText01;
    public RandomEvent01List randomEvent01;
    public ItemSheetList itemSheet;



    [Space(100)]
    [Header("Zehupe")]
    public TextAsset dialog02File;
    public TextAsset selectText02File;
    public TextAsset randomEvent02File;

    [Space(40)]

    public DialogListZehupe dialogList02;
    public Selection02List selectText02;
    public RandomEvent02List randomEvent02;


    private void Awake()
    {
        LoadDialog01();
        LoadSelectText01();
        LoadRandomEvent01();
        LoadItemSheet01();

        LoadDialog02();
        LoadSelectText02();
        LoadRandomEvent02();
    }

    private void LoadDialog01()
    {
        string json = "{\"dialogSection01\":" + dialog01File.text + "}";
        dialogList01 = JsonUtility.FromJson<DialogList>(json);
    }

    private void LoadSelectText01()
    {
        string json = "{\"selection01\":" + selectText01File.text + "}";
        selectText01 = JsonUtility.FromJson<Selection01List>(json);
    }
    private void LoadRandomEvent01()
    {
        string json = "{\"randomEvent01\":" + randomEvent01File.text + "}";
        randomEvent01 = JsonUtility.FromJson<RandomEvent01List>(json);
    }
    private void LoadItemSheet01()
    {
        string json = "{\"itemSheet\":" + itemSheetFile.text + "}";
        itemSheet = JsonUtility.FromJson<ItemSheetList>(json);
    }

    private void LoadDialog02()
    {
        string json = "{\"dialogSection02\":" + dialog02File.text + "}";
        dialogList02 = JsonUtility.FromJson<DialogListZehupe>(json);
    }

    private void LoadSelectText02()
    {
        string json = "{\"selection02\":" + selectText02File.text + "}";
        selectText02 = JsonUtility.FromJson<Selection02List>(json);
    }
    private void LoadRandomEvent02()
    {
        string json = "{\"randomEvent02\":" + randomEvent02File.text + "}";
        randomEvent02 = JsonUtility.FromJson<RandomEvent02List>(json);
    }



    void DisplayDialog(int dialogNumber)
    {
        if(dialogNumber < dialogList01.dialogSection01.Count)
        {
            DialogSection01 dialog01 = dialogList01.dialogSection01[dialogNumber];
            Debug.Log(dialog01.textContents);
        }
    }
}

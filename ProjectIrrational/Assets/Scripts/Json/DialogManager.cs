using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DialogManager : MonoBehaviour
{
    public TextAsset dialog01File;
    public TextAsset selectText01File;
    public TextAsset randomEvent01File;
    public TextAsset itemSheetFile;

    public DialogList dialogList01;
    public Selection01List selectText01;
    public RandomEvent01List randomEvent01;
    public ItemSheetList itemSheet;



    private void Awake()
    {
        LoadDialog01();
        LoadSelectText01();
        LoadRandomEvent01();
        LoadItemSheet01();
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


    void DisplayDialog(int dialogNumber)
    {
        if(dialogNumber < dialogList01.dialogSection01.Count)
        {
            DialogSection01 dialog01 = dialogList01.dialogSection01[dialogNumber];
            Debug.Log(dialog01.textContents);
        }
    }
}

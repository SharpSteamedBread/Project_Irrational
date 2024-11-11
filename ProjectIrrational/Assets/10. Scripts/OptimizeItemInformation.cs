using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemInformation
{
    public int itemCode = 0;

    public string itemSort = "null";
    public string itemName = "null";
    public string itemTip = "null";
}

public class OptimizeItemInformation : MonoBehaviour
{
    public MainText mainText;
    public ShowTextJson showTextJson;
    public ItemInformation itemInfo;

    public DialogManager dialogManager;
    public ItemSheetList itemSheetList;

    public void Awake()
    {
        showTextJson = GameObject.FindObjectOfType<ShowTextJson>();
        GetItemInfo();
    }

    public void GetItemInfo()
    {
        //아이템 코드가 1부터 시작해서 1씩 빼줌
        itemInfo.itemCode = itemSheetList.itemSheet[(showTextJson.currentDialogIndex)].itemCode;
        itemInfo.itemSort = itemSheetList.itemSheet[showTextJson.currentDialogIndex].itemSort;


        itemInfo.itemCode = mainText.ItemSheet
                            [mainText.DialogText[showTextJson.currentDialogIndex].getItemCode - 1].itemCode;
        itemInfo.itemSort = mainText.ItemSheet[itemInfo.itemCode - 1].itemSort;
        itemInfo.itemName = mainText.ItemSheet[itemInfo.itemCode - 1].itemName;
        itemInfo.itemTip = mainText.ItemSheet[itemInfo.itemCode - 1].itemInfo;

    }
}

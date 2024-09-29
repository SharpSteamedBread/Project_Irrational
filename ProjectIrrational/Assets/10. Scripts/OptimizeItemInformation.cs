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
    public ShowTextJson showText;
    public ItemInformation itemInfo;

    public void Awake()
    {
        showText = GameObject.FindObjectOfType<ShowTextJson>();
        GetItemInfo();
    }

    public void GetItemInfo()
    {
        //아이템 코드가 1부터 시작해서 1씩 빼줌
        itemInfo.itemCode = mainText.ItemSheet
                            [mainText.DialogText[showText.currentDialogIndex].getItemCode - 1].itemCode;
        itemInfo.itemSort = mainText.ItemSheet[itemInfo.itemCode - 1].itemSort;
        itemInfo.itemName = mainText.ItemSheet[itemInfo.itemCode - 1].itemName;
        itemInfo.itemTip = mainText.ItemSheet[itemInfo.itemCode - 1].itemInfo;

    }
}

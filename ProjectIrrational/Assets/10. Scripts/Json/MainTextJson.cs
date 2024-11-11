using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTextJson : MonoBehaviour
{
    
}

[System.Serializable]
public class DialogSection01
{
    public int number;
    public string textContents;
    public int hasSelectEvent;
    public string mainImage;
    public string statType;
    public int statValue;
    public int getItemCode;
    public int fontValue;
    public int selectEventJumpTo;
}

[System.Serializable]
public class Selection01
{
    public int selectEventNumber;
    public string eventImage;

    public string selectText1;
    public string triggerEvent1;
    public string eventCondition_type1;
    public int eventCondition_value1;

    public string selectText2;
    public string triggerEvent2;
    public string eventCondition_type2;
    public int eventCondition_value2;

    public string selectText3;
    public string triggerEvent3;
    public string eventCondition_type3;
    public int eventCondition_value3;
}

[System.Serializable]
public class RandomEvent01
{
    public int eventID;
    public string printEventImageResult;
    public string printResult;
    public int mainDialogJumpTo;
    public int eventEndChecker;
}

[System.Serializable]
public class ItemSheet
{
    public int itemCode;
    public string itemSort;
    public string itemName;
    public string itemInfo;
    public string itemFunc;
    public string itemEvent;
}

[System.Serializable]
public class DialogList
{
    public List<DialogSection01> dialogSection01;
}

[System.Serializable]
public class Selection01List
{
    public List<Selection01> selection01;
}

[System.Serializable]
public class RandomEvent01List
{
    public List<RandomEvent01> randomEvent01;
}

[System.Serializable]
public class ItemSheetList
{
    public List<ItemSheet> itemSheet;
}





//////
/// <summary>
/// Zehupe Áö¿ª
/// </summary>


[System.Serializable]
public class DialogSection02
{
    public int number;
    public string textContents;
    public int hasSelectEvent;
    public string mainImage;
    public string statType;
    public int statValue;
    public int getItemCode;
    public int fontValue;
    public int selectEventJumpTo;
}

[System.Serializable]
public class Selection02
{
    public int selectEventNumber;
    public string eventImage;

    public string selectText1;
    public string triggerEvent1;
    public string eventCondition_type1;
    public int eventCondition_value1;

    public string selectText2;
    public string triggerEvent2;
    public string eventCondition_type2;
    public int eventCondition_value2;

    public string selectText3;
    public string triggerEvent3;
    public string eventCondition_type3;
    public int eventCondition_value3;
}

[System.Serializable]
public class RandomEvent02
{
    public int eventID;
    public string printEventImageResult;
    public string printResult;
    public int mainDialogJumpTo;
}

[System.Serializable]
public class DialogListZehupe
{
    public List<DialogSection02> dialogSection02;
}

[System.Serializable]
public class Selection02List
{
    public List<Selection02> selection02;
}

[System.Serializable]
public class RandomEvent02List
{
    public List<RandomEvent02> randomEvent02;
}



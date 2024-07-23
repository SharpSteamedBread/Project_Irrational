[System.Serializable]

public class DialogDBEntity
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

    public int selectEventNumber;
    public string triggerRandomEvent;

    public string selectText1;
    public string selectText2;
    public string selectText3;
    public string selectText4;

    public string triggerEvent1;
    public string triggerEvent2;
    public string triggerEvent3;
    public string triggerEvent4;

    public string printResult1;
    public string printResult2;
    public string printResult3;
    public string printResult4;

    public string eventImage;
    public string printEventImageResult1;
    public string printEventImageResult2;
    public string printEventImageResult3;
    public string printEventImageResult4;

    public int eventID;
    public string printEventImageResult;
    public string printResult;
    public int mainDialogJumpTo;

    public int itemCode;
    public string itemSort;
    public string itemName;
    public string itemInfo;
    public string itemFunc;     //아이템 효과인데 툴팁인지 효과수치인지는 모릅
    public string itemEvent;

}

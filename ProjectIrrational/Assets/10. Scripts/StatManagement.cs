using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManagement : MonoBehaviour
{
    public DialogManager dialogManager;

    public DialogList dialogList01;             // JSON에서 불러온 Dialog 데이터 리스트
    public Selection01List selectText01;        // JSON에서 불러온 Selection 데이터 리스트
    public RandomEvent01List randomEvent01;     // JSON에서 불러온 RandomEvent 데이터 리스트

    public GameObject objTextController;

    public int getCurrDialogIndex;
    
    [Header("스텟 오브젝트")]
    public int valueHeart;

    public int valueCoin;

    public int valueMental;
    
    public void Awake()
    {
        getCurrDialogIndex = objTextController.GetComponent<ShowTextJson>().currentDialogIndex;
    }

    private void Start()
    {
        dialogList01 = dialogManager.dialogList01;
        selectText01 = dialogManager.selectText01;
        randomEvent01 = dialogManager.randomEvent01;
    }

    public void CalculateHeart()
    {
        getCurrDialogIndex = objTextController.GetComponent<ShowTextJson>().currentDialogIndex;
        if (dialogList01.dialogSection01[getCurrDialogIndex].statValue > 0)
        {
            for (int i = 0; i < dialogList01.dialogSection01[getCurrDialogIndex].statValue; ++i)
            {
                valueHeart++;
            }
        }
        else if (dialogList01.dialogSection01[getCurrDialogIndex].statValue < 0)
        {
            for (int i = 0; i < Mathf.Abs(dialogList01.dialogSection01[getCurrDialogIndex].statValue); ++i)
            {
                valueHeart--;
            }
        }
    }

    public void CalculateCoin()
    {
        getCurrDialogIndex = objTextController.GetComponent<ShowTextJson>().currentDialogIndex;

        if (dialogList01.dialogSection01[getCurrDialogIndex].statValue > 0)
        {
            for (int i = 0; i < dialogList01.dialogSection01[getCurrDialogIndex].statValue; ++i)
            {
                valueCoin++;
            }
        }
        else if (dialogList01.dialogSection01[getCurrDialogIndex].statValue < 0)
        {
            for (int i = 0; i < Mathf.Abs(dialogList01.dialogSection01[getCurrDialogIndex].statValue); ++i)
            {
                valueCoin--;
            }
        }
    }

    public void CalculateMental()
    {
        getCurrDialogIndex = objTextController.GetComponent<ShowTextJson>().currentDialogIndex;

        if (dialogList01.dialogSection01[getCurrDialogIndex].statValue > 0)
        {
            for (int i = 0; i < dialogList01.dialogSection01[getCurrDialogIndex].statValue; ++i)
            {
                valueMental++;
            }
        }
        else if (dialogList01.dialogSection01[getCurrDialogIndex].statValue < 0)
        {
            for (int i = 0; i < Mathf.Abs(dialogList01.dialogSection01[getCurrDialogIndex].statValue); ++i)
            {
                valueMental--;
            }
        }
    }
}

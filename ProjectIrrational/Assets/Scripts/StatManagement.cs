using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManagement : MonoBehaviour
{
    public MainText mainText;
    public GameObject objTextController;

    public int getCurrDialogIndex;

    [Header("스텟 오브젝트")]
    [SerializeField] private GameObject parentHeart;
    [SerializeField] private GameObject prefabHeart;
    public int valueHeart;

    [SerializeField] private GameObject parentCoin;
    [SerializeField] private GameObject prefabCoin;
    public int valueCoin;

    [SerializeField] private GameObject parentMental;
    [SerializeField] private GameObject prefabMental;
    public int valueMental;

    public void Awake()
    {
        getCurrDialogIndex = objTextController.GetComponent<ShowTextJson>().currentDialogIndex;
    }

    public void CalculateHeart()
    {
        getCurrDialogIndex = objTextController.GetComponent<ShowTextJson>().currentDialogIndex;

        if(mainText.DialogText[getCurrDialogIndex].statValue > 0)
        {
            for (int i = 0; i < mainText.DialogText[getCurrDialogIndex].statValue; ++i)
            {
                valueHeart++;
                Instantiate(prefabHeart, parentHeart.transform);
            }
        }
        else if (mainText.DialogText[getCurrDialogIndex].statValue < 0)
        {
            for (int i = 0; i < Mathf.Abs(mainText.DialogText[getCurrDialogIndex].statValue); ++i)
            {
                valueHeart--;

                if (parentHeart.transform.childCount > 0)
                {
                    Transform toDestroyLastPrefab = parentHeart.transform.GetChild(parentHeart.transform.childCount - 1);
                    Destroy(toDestroyLastPrefab.gameObject);
                }
            }
        }
    }

    public void CalculateCoin()
    {
        getCurrDialogIndex = objTextController.GetComponent<ShowTextJson>().currentDialogIndex;

        if (mainText.DialogText[getCurrDialogIndex].statValue > 0)
        {
            for (int i = 0; i < mainText.DialogText[getCurrDialogIndex].statValue; ++i)
            {
                valueCoin++;
                Instantiate(prefabCoin, parentCoin.transform);
            }
        }
        else if (mainText.DialogText[getCurrDialogIndex].statValue < 0)
        {
            for (int i = 0; i < Mathf.Abs(mainText.DialogText[getCurrDialogIndex].statValue); ++i)
            {
                valueCoin--;

                if (parentCoin.transform.childCount > 0)
                {
                    Transform toDestroyLastPrefab = parentCoin.transform.GetChild(parentCoin.transform.childCount - 1);
                    Destroy(toDestroyLastPrefab.gameObject);
                }
            }
        }
    }

    public void CalculateMental()
    {
        getCurrDialogIndex = objTextController.GetComponent<ShowTextJson>().currentDialogIndex;

        if (mainText.DialogText[getCurrDialogIndex].statValue > 0)
        {
            for (int i = 0; i < mainText.DialogText[getCurrDialogIndex].statValue; ++i)
            {
                valueMental++;
                Instantiate(prefabMental, parentMental.transform);
            }
        }
        else if (mainText.DialogText[getCurrDialogIndex].statValue < 0)
        {
            for (int i = 0; i < Mathf.Abs(mainText.DialogText[getCurrDialogIndex].statValue); ++i)
            {
                valueMental--;

                if(parentMental.transform.childCount > 0)
                {
                    Transform toDestroyLastPrefab = parentMental.transform.GetChild(parentMental.transform.childCount - 1);
                    Destroy(toDestroyLastPrefab.gameObject);

                }
            }
        }
    }
}

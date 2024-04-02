using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowText : MonoBehaviour
{
    public MainText mainText;
    public int lineCount;

    [Header("텍스트 타이핑")]

    [SerializeField] private TextMeshProUGUI objText;

    private int currentDialogIndex = 0;

    private string prevText;
    private string currText;

    [Header("선택지 출력")]
    private int selectEvent;
    private int eventCode = 0;

    public TextMeshProUGUI selectText1;
    public TextMeshProUGUI selectText2;
    public TextMeshProUGUI selectText3;
    public TextMeshProUGUI selectText4;


    private void Awake()
    {
        currentDialogIndex = mainText.DialogText[0].number;
        Debug.Log(currentDialogIndex);


        objText.text = prevText + " ";

        StartCoroutine(OnTypingText());
    }

    private void Update()
    {
        UpdateText();
    }

    public void UpdateText()
    {

        if (Input.GetMouseButtonDown(0))
        {
            currentDialogIndex++;
            Debug.Log(currentDialogIndex);

            StartCoroutine(OnTypingText());
            StartCoroutine(CheckSelectEvent());
        }
    }


    private IEnumerator OnTypingText()
    {
        prevText += currText + "\n\n";

        for (int count = 0; count < mainText.DialogText[currentDialogIndex - 1].textContents.Length; count++)
        {
            currText = mainText.DialogText[currentDialogIndex - 1].textContents.Substring(0, count+1);
            objText.text = prevText + currText;

            yield return new WaitForSeconds(0.03f);
        }
    }

    private IEnumerator CheckSelectEvent()
    {
        selectEvent = mainText.DialogText[currentDialogIndex - 1].hasSelectEvent;
        

        if (selectEvent == 1)
        {
            selectText1.text = mainText.SelectText[0].selectText1;
            selectText2.text = mainText.SelectText[0].selectText2;
            selectText3.text = mainText.SelectText[0].selectText3;
            selectText4.text = mainText.SelectText[0].selectText4;
        }

        yield return new WaitForSeconds(0f);

        eventCode++;
    }
}

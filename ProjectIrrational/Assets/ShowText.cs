using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowText : MonoBehaviour
{
    public MainText mainText;

    [SerializeField] private TextMeshProUGUI objText;

    private int currentDialogIndex = 0;

    private string prevText;
    private string currText;

    private float typingSpeed = 0.1f;
    private bool isTypingEffect = false;

    private void Awake()
    {
        currentDialogIndex = mainText.Sheet1[0].number;
        Debug.Log(currentDialogIndex);

        //objText.text = mainText.Sheet1[currentDialogIndex -1].textContents;
        //Debug.Log(objText.text);

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

        }
    }


    private IEnumerator OnTypingText()
    {
        prevText += currText + "\n\n";

        for (int count = 0; count < mainText.Sheet1[currentDialogIndex - 1].textContents.Length; count++)
        {
            currText = mainText.Sheet1[currentDialogIndex - 1].textContents.Substring(0, count+1);
            objText.text = prevText + currText;

            yield return new WaitForSeconds(0.03f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowText : MonoBehaviour
{
    public MainText mainText;
    public GameObject randomEncounterManager;

    [Header("텍스트 타이핑")]

    [SerializeField] private TextMeshProUGUI objText;

    public int currentDialogIndex = 0;

    private string prevText;
    private string currText;

    public bool isTyping = true;

    [SerializeField] float typingSpeed = 0.1f;


    [Header("선택지 출력")]
    public int eventCode;
    private int selectEvent;

    public TextMeshProUGUI selectText1;
    public TextMeshProUGUI selectText2;
    public TextMeshProUGUI selectText3;

    public GameObject objSelectText1;
    public GameObject objSelectText2;
    public GameObject objSelectText3;

    public int hasSelectedText = 0;

    [Header("이미지 페이드 연출")]

    public Image objEventImage;
    public Animator animEventImage;

    [Header("설정")]
    public GameObject objSettingUI;

    [Header("랜덤 이벤트")]
    public int randomNumber = 0;
    public int eventID;
    public string eventPath;


    private void Awake()
    {
        currentDialogIndex = mainText.DialogText[0].number;
        Debug.Log(currentDialogIndex);

        objEventImage.GetComponent<Image>();
        animEventImage.GetComponent<Animator>();

        objText.text = prevText + " ";

        StartCoroutine(OnTypingText());
    }
  
    private void Update()
    {
        UpdateText();
    }

    public void UpdateText()
    {

        if (objSettingUI.activeSelf == false && Input.GetMouseButtonDown(0))
        {
            if(isTyping == true)
            {
                typingSpeed = 0f;
            }

            else if(isTyping == false)
            {
                typingSpeed = 0.1f;

                if (objSelectText1.activeSelf == false)
                {
                    StartCoroutine(OnTypingText());
                    StartCoroutine(CheckSelectEvent());

                }
            }
        }
    }


    private IEnumerator OnTypingText()
    {
        yield return new WaitForSeconds(typingSpeed);

        isTyping = true;

        prevText += currText + "\n\n";

        int count = 0;
        while(count < mainText.DialogText[currentDialogIndex].textContents.Length)
        {
            currText = mainText.DialogText[currentDialogIndex].textContents.Substring(0, count + 1);
            objText.text = prevText + currText;

            count++;

            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        currentDialogIndex++;
        typingSpeed = 0.1f;
    }

    private IEnumerator CheckSelectEvent()
    {
        selectEvent = mainText.DialogText[currentDialogIndex].hasSelectEvent;
        

        if (selectEvent == 1)
        {
            objSelectText1.SetActive(true);
            objSelectText2.SetActive(true);
            objSelectText3.SetActive(true);

            selectText1.text = mainText.SelectText[eventCode].selectText1;
            selectText2.text = mainText.SelectText[eventCode].selectText2;
            selectText3.text = mainText.SelectText[eventCode].selectText3;

            animEventImage.Play("ImageFadeUI", -1, 0f);
            objEventImage.sprite = Resources.Load<Sprite>($"{mainText.SelectText[0].eventImage}");

        }

        yield return new WaitForSeconds(0f);

    }

    public void SelectAndJump1()
    {
        hasSelectedText = 1;
        animEventImage.Play("ImageFadeUI", -1, 0f);
        eventPath = mainText.SelectText[eventCode].triggerEvent1;

        ChooseRandomNumber();
        objEventImage.sprite = Resources.Load<Sprite>($"{mainText.RandomEventTest[randomEncounterManager.GetComponent<RandomEvent>().testRandomEvent[randomNumber]].printEventImageResult}");

        StartCoroutine(ReadEvent());

        objSelectText1.SetActive(false);
        objSelectText2.SetActive(false);
        objSelectText3.SetActive(false);

        eventCode++;
    }

    public void SelectAndJump2()
    {
        hasSelectedText = 2;
        animEventImage.Play("ImageFadeUI", -1, 0f);
        eventPath = mainText.SelectText[eventCode].triggerEvent2;

        ChooseRandomNumber();
        objEventImage.sprite = Resources.Load<Sprite>($"{mainText.RandomEventTest[randomEncounterManager.GetComponent<RandomEvent>().testRandomEvent[randomNumber]].printEventImageResult}");

        StartCoroutine(ReadEvent());

        objSelectText1.SetActive(false);
        objSelectText2.SetActive(false);
        objSelectText3.SetActive(false);

        eventCode++;
    }

    public void SelectAndJump3()
    {
        hasSelectedText = 3;
        animEventImage.Play("ImageFadeUI", -1, 0f);
        eventPath = mainText.SelectText[eventCode].triggerEvent3;

        ChooseRandomNumber();
        objEventImage.sprite = Resources.Load<Sprite>($"{mainText.RandomEventTest[randomEncounterManager.GetComponent<RandomEvent>().testRandomEvent[randomNumber]].printEventImageResult}");

        StartCoroutine(ReadEvent());

        objSelectText1.SetActive(false);
        objSelectText2.SetActive(false);
        objSelectText3.SetActive(false);

        eventCode++;
    }

    public void SelectAndJump4()
    {
        hasSelectedText = 4;
        animEventImage.Play("ImageFadeUI", -1, 0f);
        eventPath = mainText.SelectText[eventCode].triggerEvent4;

        ChooseRandomNumber();
        objEventImage.sprite = Resources.Load<Sprite>($"{mainText.RandomEventTest[randomEncounterManager.GetComponent<RandomEvent>().testRandomEvent[randomNumber]].printEventImageResult}");

        StartCoroutine(ReadEvent());

        objSelectText1.SetActive(false);
        objSelectText2.SetActive(false);
        objSelectText3.SetActive(false);

        eventCode++;
    }

    private IEnumerator PrintSelectedResult()
    {
        yield return new WaitForSeconds(typingSpeed);

        isTyping = true;

        prevText += currText + "\n\n";

        int count = 0;

        switch (hasSelectedText)
        {
            case 1:
                while (count < mainText.SelectText[eventCode].printResult1.Length)
                {
                    currText = mainText.SelectText[eventCode].printResult1.Substring(0, count + 1);
                    objText.text = prevText + currText;

                    count++;

                    yield return new WaitForSeconds(typingSpeed);
                }
                break;

            case 2:
                while (count < mainText.SelectText[eventCode].printResult2.Length)
                {
                    currText = mainText.SelectText[eventCode].printResult2.Substring(0, count + 1);
                    objText.text = prevText + currText;

                    count++;

                    yield return new WaitForSeconds(typingSpeed);
                }
                break;

            case 3:
                while (count < mainText.SelectText[eventCode].printResult3.Length)
                {
                    currText = mainText.SelectText[eventCode].printResult3.Substring(0, count + 1);
                    objText.text = prevText + currText;

                    count++;

                    yield return new WaitForSeconds(typingSpeed);
                }
                break;

            case 4:
                while (count < mainText.SelectText[eventCode].printResult4.Length)
                {
                    currText = mainText.SelectText[eventCode].printResult4.Substring(0, count + 1);
                    objText.text = prevText + currText;

                    count++;

                    yield return new WaitForSeconds(typingSpeed);
                }
                break;
        }

        isTyping = false;
        hasSelectedText = 0;
        currentDialogIndex++;
        typingSpeed = 0.1f;
    }

    public void ChooseRandomNumber()
    {
        randomNumber = 0;

        switch(eventPath)
        {
            case "testRandomEvent":
                randomNumber = Random.Range(0, randomEncounterManager.GetComponent<RandomEvent>().testRandomEvent.Length);
                eventID = randomEncounterManager.GetComponent<RandomEvent>().testRandomEvent[randomNumber];
                Debug.Log($"배열 인덱스 번호는 {randomNumber}! \n 실제 숫자는 {eventID}");
                break;

            case "eventBuyFood":
                randomNumber = Random.Range(0, randomEncounterManager.GetComponent<RandomEvent>().eventBuyFood.Length);
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventBuyFood[randomNumber];
                Debug.Log($"배열 인덱스 번호는 {randomNumber}! \n 실제 숫자는 {eventID}");
                break;

            case "eventEarphone":
                randomNumber = Random.Range(0, randomEncounterManager.GetComponent<RandomEvent>().eventEarphone.Length);
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventEarphone[randomNumber];
                Debug.Log($"배열 인덱스 번호는 {randomNumber}! \n 실제 숫자는 {eventID}");
                break;
        }

    }

    public IEnumerator ReadEvent()
    {
        Debug.Log($"{mainText.RandomEventTest[eventID].printResult}");


        yield return new WaitForSeconds(typingSpeed);

        isTyping = true;

        prevText += currText + "\n\n";

        int count = 0;


        while (count < mainText.RandomEventTest[eventID].printResult.Length)
        {
            currText = mainText.RandomEventTest[eventID].printResult.Substring(0, count + 1);
            objText.text = prevText + currText;

            count++;

            yield return new WaitForSeconds(typingSpeed);
        }



        isTyping = false;
        hasSelectedText = 0;
        currentDialogIndex++;
        typingSpeed = 0.1f;

    }
}

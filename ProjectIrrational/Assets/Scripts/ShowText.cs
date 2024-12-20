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

    [SerializeField] float typingSpeed = 0.01f;


    [Header("선택지 출력")]

    public int currentEventPath = 0;
    public int eventNumber;             //액셀 파일의 selectEventNumber 변수
    public int selectEvent;            //액셀 파일의 hasSelectEvent 변수

    public TextMeshProUGUI selectText1;
    public TextMeshProUGUI selectText2;
    public TextMeshProUGUI selectText3;

    public GameObject objSelectText1;
    public GameObject objSelectText2;
    public GameObject objSelectText3;

    public int hasSelectedText = 0;

    public int selectJumpToValue;

    [Header("이미지 페이드 연출")]
    public Image objEventImage;
    public Animator animEventImage;

    [Header("설정")]
    public GameObject objSettingUI;

    [Header("랜덤 이벤트")]
    public int randomNumber = 0;
    public int eventID;
    public string eventPath;

    [Header("스텟 가감")]
    public StatManagement statManagement;

    [Header("아이템 제어")]
    public Transform itemUIParent;
    public GameObject itemObject;

    [Header("스크롤 제어")]
    [SerializeField] private ScrollViewController scrollviewController;

    private void Awake()
    {
        currentDialogIndex = mainText.DialogText[0].number;

        objEventImage.GetComponent<Image>();
        animEventImage.GetComponent<Animator>();

        objText.text = prevText + " ";

        StartCoroutine(OnTypingText());
    }

    private void Start()
    {
        Canvas.ForceUpdateCanvases();
    }

    private void Update()
    {
        UpdateText();
    }

    public void UpdateText()
    {

        if (objSettingUI.activeSelf == false && Input.GetMouseButtonDown(0))
        {
            if (isTyping == true)
            {
                typingSpeed = 0f;
                scrollviewController.AutomaticScroll();
            }

            else if (isTyping == false)
            {
                typingSpeed = 0.01f;

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
        while (count < mainText.DialogText[currentDialogIndex].textContents.Length)
        {
            currText = mainText.DialogText[currentDialogIndex].textContents.Substring(0, count + 1);
            objText.text = prevText + currText;

            count++;
            //scrollviewController.AutomaticScroll();

            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;


        currentDialogIndex++;


        typingSpeed = 0.01f;
    }

    private IEnumerator CheckSelectEvent()
    {
        eventNumber = mainText.SelectText[currentEventPath].selectEventNumber;
        selectEvent = mainText.DialogText[currentDialogIndex].hasSelectEvent;

        if (selectEvent == 1)   //선택지 출력
        {
            selectText1.text = mainText.SelectText[eventNumber].selectText1;
            selectText2.text = mainText.SelectText[eventNumber].selectText2;
            selectText3.text = mainText.SelectText[eventNumber].selectText3;

            if(selectText1.text == "-")
            {
                objSelectText1.SetActive(false);
            }
            else
            {
                objSelectText1.SetActive(true);
            }

            if (selectText2.text == "-")
            {
                objSelectText2.SetActive(false);
            }
            else
            {
                objSelectText2.SetActive(true);
            }

            if (selectText3.text == "-")
            {
                objSelectText3.SetActive(false);
            }
            else
            {
                objSelectText3.SetActive(true);
            }
            
            //null이 아닐 때에만 이미지 불러오기(이미지를 할당하지 않는 이벤트 고려)
            if (mainText.SelectText[eventNumber].eventImage.ToString() != "null")
            {
                animEventImage.Play("ImageFadeUI", -1, 0f);
                objEventImage.sprite = Resources.Load<Sprite>($"{mainText.SelectText[eventNumber].eventImage}");
            }
        }

        else if (selectEvent == 2)   //이미지 출력
        {
            //null이 아닐 때에만 이미지 불러오기(이미지를 할당하지 않는 이벤트 고려)
            if (mainText.DialogText[currentDialogIndex].mainImage.ToString() != "null")
            {
                animEventImage.Play("ImageFadeUI", -1, 0f);
                objEventImage.sprite = Resources.Load<Sprite>($"{mainText.DialogText[currentDialogIndex].mainImage}");
            }
        }

        else if (selectEvent == 3)   //스텟 가감
        {
            Debug.Log("일안해?");

            switch (mainText.DialogText[currentDialogIndex].statType)
            {
                case "statHeart":
                    statManagement.CalculateHeart();
                    break;

                case "statCoin":
                    statManagement.CalculateCoin();
                    break;

                case "statMental":
                    statManagement.CalculateMental();
                    break;

                case "null":
                    Debug.Log("아이템 추가를 기대해줘~");
                    break;
            }
        }

        else if (selectEvent == 4)   //아이템 획득
        {
            Instantiate(itemObject, transform.position, Quaternion.identity, itemUIParent);
        }

        else if(selectEvent == 5)   // 선택지 이벤트를 점프
        {
            selectJumpToValue = mainText.DialogText[currentDialogIndex].selectEventJumpTo;
            eventNumber = mainText.SelectText[selectJumpToValue].selectEventNumber;

            objSelectText1.SetActive(true);
            objSelectText2.SetActive(true);
            objSelectText3.SetActive(true);

            selectText1.text = mainText.SelectText[eventNumber].selectText1;
            selectText2.text = mainText.SelectText[eventNumber].selectText2;
            selectText3.text = mainText.SelectText[eventNumber].selectText3;

            //null이 아닐 때에만 이미지 불러오기(이미지를 할당하지 않는 이벤트 고려)
            if (mainText.SelectText[eventNumber].eventImage.ToString() != "null")
            {
                animEventImage.Play("ImageFadeUI", -1, 0f);
                objEventImage.sprite = Resources.Load<Sprite>($"{mainText.SelectText[eventNumber].eventImage}");
            }

            if (selectText1.text == "-")
            {
                objSelectText1.SetActive(false);
            }
            else
            {
                objSelectText1.SetActive(true);
            }

            if (selectText2.text == "-")
            {
                objSelectText2.SetActive(false);
            }
            else
            {
                objSelectText2.SetActive(true);
            }

            if (selectText3.text == "-")
            {
                objSelectText3.SetActive(false);
            }
            else
            {
                objSelectText3.SetActive(true);
            }
        }


        yield return new WaitForSeconds(0f);

    }

    public void SelectAndJump1()
    {
        hasSelectedText = 1;
        eventPath = mainText.SelectText[eventNumber].triggerEvent1;

        ChooseRandomNumber();

        //null이 아닐 때에만 이미지 불러오기(이미지를 할당하지 않는 이벤트 고려)
        if (mainText.RandomEventTest[randomEncounterManager.GetComponent<RandomEvent>().testRandomEvent[randomNumber]].printEventImageResult.ToString() != "null")
        {
            animEventImage.Play("ImageFadeUI", -1, 0f);
            objEventImage.sprite = Resources.Load<Sprite>($"{mainText.RandomEventTest[randomEncounterManager.GetComponent<RandomEvent>().testRandomEvent[randomNumber]].printEventImageResult}");
        }

        StartCoroutine(ReadEvent());

        objSelectText1.SetActive(false);
        objSelectText2.SetActive(false);
        objSelectText3.SetActive(false);

        currentEventPath++;
    }

    public void SelectAndJump2()
    {
        hasSelectedText = 2;
        eventPath = mainText.SelectText[eventNumber].triggerEvent2;

        ChooseRandomNumber();

        //null이 아닐 때에만 이미지 불러오기(이미지를 할당하지 않는 이벤트 고려)
        if (mainText.RandomEventTest[randomEncounterManager.GetComponent<RandomEvent>().testRandomEvent[randomNumber]].printEventImageResult.ToString() != "null")
        {
            animEventImage.Play("ImageFadeUI", -1, 0f);
            objEventImage.sprite = Resources.Load<Sprite>($"{mainText.RandomEventTest[randomEncounterManager.GetComponent<RandomEvent>().testRandomEvent[randomNumber]].printEventImageResult}");
        }

        StartCoroutine(ReadEvent());

        objSelectText1.SetActive(false);
        objSelectText2.SetActive(false);
        objSelectText3.SetActive(false);

        currentEventPath++;
    }

    public void SelectAndJump3()
    {
        hasSelectedText = 3;
        eventPath = mainText.SelectText[eventNumber].triggerEvent3;

        ChooseRandomNumber();

        //null이 아닐 때에만 이미지 불러오기(이미지를 할당하지 않는 이벤트 고려)
        if (mainText.RandomEventTest[randomEncounterManager.GetComponent<RandomEvent>().testRandomEvent[randomNumber]].printEventImageResult.ToString() != "null")
        {
            animEventImage.Play("ImageFadeUI", -1, 0f);
            objEventImage.sprite = Resources.Load<Sprite>($"{mainText.RandomEventTest[randomEncounterManager.GetComponent<RandomEvent>().testRandomEvent[randomNumber]].printEventImageResult}");
        }

        StartCoroutine(ReadEvent());

        objSelectText1.SetActive(false);
        objSelectText2.SetActive(false);
        objSelectText3.SetActive(false);

        currentEventPath++;
    }

    public void SelectAndJump4()
    {
        hasSelectedText = 4;
        eventPath = mainText.SelectText[eventNumber].triggerEvent4;

        ChooseRandomNumber();

        //null이 아닐 때에만 이미지 불러오기(이미지를 할당하지 않는 이벤트 고려)
        if (mainText.RandomEventTest[randomEncounterManager.GetComponent<RandomEvent>().testRandomEvent[randomNumber]].printEventImageResult.ToString() != "null")
        {
            animEventImage.Play("ImageFadeUI", -1, 0f);
            objEventImage.sprite = Resources.Load<Sprite>($"{mainText.RandomEventTest[randomEncounterManager.GetComponent<RandomEvent>().testRandomEvent[randomNumber]].printEventImageResult}");
        }

        StartCoroutine(ReadEvent());

        objSelectText1.SetActive(false);
        objSelectText2.SetActive(false);
        objSelectText3.SetActive(false);

        currentEventPath++;
    }

    public void ChooseRandomNumber()
    {
        randomNumber = 0;

        switch (eventPath)
        {
            case "eventID0":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest1;
                break;

            case "eventID1":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest2;
                break;

            case "eventID2":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest3;
                break;

            case "eventID3":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest4;
                break;

            case "eventID4":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest5;
                break;

            case "eventID5":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest6;
                break;

            case "eventID6":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest7;
                break;

            case "eventID7":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest8;
                break;

            case "eventID8":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest9;
                break;

            case "eventID9":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest10;
                break;

            case "eventID10":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest11;
                break;

            case "eventID11":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest12;
                break;

            case "eventID12":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest13;
                break;

            case "eventID13":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest14;
                break;

            case "eventID14":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest15;
                break;

            case "eventID15":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest16;
                break;

            case "eventID16":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest17;
                break;

            case "eventID17":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest18;
                break;

            case "eventID18":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest19;
                break;
            case "eventID19":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest20;
                break;
            case "eventID20":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest21;
                break;
            case "eventID21":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest22;
                break;
            case "eventID22":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest23;
                break;
            case "eventID23":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest24;
                break;
            case "eventID24":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest25;
                break;
        }

    }

    public IEnumerator ReadEvent()
    {
        Debug.Log($"CurrentEventPath: {currentEventPath}, eventPath: {eventPath}, EventID: {eventID}, " +
            $"{mainText.RandomEventTest[eventID].printResult}, 다이얼로그 번호: {mainText.RandomEventTest[eventID].mainDialogJumpTo}");


        yield return new WaitForSeconds(typingSpeed);

        isTyping = false;
        hasSelectedText = 0;
        //currentDialogIndex++;

        if (mainText.RandomEventTest[eventID].mainDialogJumpTo != 0)
        {
            currentDialogIndex = mainText.RandomEventTest[eventID].mainDialogJumpTo;

        }

        typingSpeed = 0.01f;
    }

    public IEnumerator ReadEventOrigin()
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
            //scrollviewController.AutomaticScroll();

            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        hasSelectedText = 0;
        //currentDialogIndex++;

        if (mainText.RandomEventTest[eventID].mainDialogJumpTo != 0)
        {
            currentDialogIndex = mainText.RandomEventTest[eventID].mainDialogJumpTo;
        }

        typingSpeed = 0.01f;
    }
}

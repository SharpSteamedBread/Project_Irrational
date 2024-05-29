using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowText : MonoBehaviour
{
    public MainText mainText;
    public GameObject randomEncounterManager;

    [Header("�ؽ�Ʈ Ÿ����")]

    [SerializeField] private TextMeshProUGUI objText;

    public int currentDialogIndex = 0;

    private string prevText;
    private string currText;

    public bool isTyping = true;

    [SerializeField] float typingSpeed = 0.1f;


    [Header("������ ���")]

    public int currentEventPath = 0;
    public int eventNumber;             //�׼� ������ selectEventNumber ����
    private int selectEvent;            //�׼� ������ hasSelectEvent ����

    public TextMeshProUGUI selectText1;
    public TextMeshProUGUI selectText2;
    public TextMeshProUGUI selectText3;

    public GameObject objSelectText1;
    public GameObject objSelectText2;
    public GameObject objSelectText3;

    public int hasSelectedText = 0;

    [Header("�̹��� ���̵� ����")]
    public Image objEventImage;
    public Animator animEventImage;

    [Header("����")]
    public GameObject objSettingUI;

    [Header("���� �̺�Ʈ")]
    public int randomNumber = 0;
    public int eventID;
    public string eventPath;

    [Header("���� ����")]
    public StatManagement statManagement;

    [Header("������ ����")]
    public Transform itemUIParent;
    public GameObject itemObject;

    [Header("��ũ�� ����")]
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
            if(isTyping == true)
            {
                typingSpeed = 0f;
                scrollviewController.AutomaticScroll();
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
        while (count < mainText.DialogText[currentDialogIndex].textContents.Length)
        {
            currText = mainText.DialogText[currentDialogIndex].textContents.Substring(0, count + 1);
            objText.text = prevText + currText;

            count++;
            //scrollviewController.AutomaticScroll();

            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;

        if (currentDialogIndex == 42 || currentDialogIndex == 50 || currentDialogIndex == 58)
        {
            currentDialogIndex = 63;
        }
        else if (currentDialogIndex == 73 || currentDialogIndex == 77 || currentDialogIndex == 80)
        {
            currentDialogIndex = 81;
        }
        else
        {
            currentDialogIndex++;
        }


        typingSpeed = 0.1f;
    }

    private IEnumerator CheckSelectEvent()
    {
        eventNumber = mainText.SelectText[currentEventPath].selectEventNumber;
        selectEvent = mainText.DialogText[currentDialogIndex].hasSelectEvent;

        if (selectEvent == 1)   //������ ���
        {
            objSelectText1.SetActive(true);
            objSelectText2.SetActive(true);
            objSelectText3.SetActive(true);

            selectText1.text = mainText.SelectText[eventNumber].selectText1;
            selectText2.text = mainText.SelectText[eventNumber].selectText2;
            selectText3.text = mainText.SelectText[eventNumber].selectText3;

            //null�� �ƴ� ������ �̹��� �ҷ�����(�̹����� �Ҵ����� �ʴ� �̺�Ʈ ���)
            if(mainText.SelectText[eventNumber].eventImage.ToString() != "null")
            {
                animEventImage.Play("ImageFadeUI", -1, 0f);
                objEventImage.sprite = Resources.Load<Sprite>($"{mainText.SelectText[eventNumber].eventImage}");
            }
        }

        else if(selectEvent == 2)   //�̹��� ���
        {
            //null�� �ƴ� ������ �̹��� �ҷ�����(�̹����� �Ҵ����� �ʴ� �̺�Ʈ ���)
            if (mainText.DialogText[currentDialogIndex].mainImage.ToString() != "null")
            {
                animEventImage.Play("ImageFadeUI", -1, 0f);
                objEventImage.sprite = Resources.Load<Sprite>($"{mainText.DialogText[currentDialogIndex].mainImage}");
            }
        }

        else if(selectEvent == 3)   //���� ����
        {
            switch(mainText.DialogText[currentDialogIndex].statType)
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
                    Debug.Log("������ �߰��� �������~");
                    break;
            }
        }

        else if(selectEvent == 4)   //������ ȹ��
        {
            Instantiate(itemObject, transform.position, Quaternion.identity, itemUIParent);
        }
                

        yield return new WaitForSeconds(0f);

    }

    public void SelectAndJump1()
    {
        hasSelectedText = 1;
        eventPath = mainText.SelectText[eventNumber].triggerEvent1;

        ChooseRandomNumber();

        //null�� �ƴ� ������ �̹��� �ҷ�����(�̹����� �Ҵ����� �ʴ� �̺�Ʈ ���)
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

        //null�� �ƴ� ������ �̹��� �ҷ�����(�̹����� �Ҵ����� �ʴ� �̺�Ʈ ���)
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

        //null�� �ƴ� ������ �̹��� �ҷ�����(�̹����� �Ҵ����� �ʴ� �̺�Ʈ ���)
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

        //null�� �ƴ� ������ �̹��� �ҷ�����(�̹����� �Ҵ����� �ʴ� �̺�Ʈ ���)
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

        switch(eventPath)
        {
            case "testRandomEvent":
                randomNumber = Random.Range(0, randomEncounterManager.GetComponent<RandomEvent>().testRandomEvent.Length);
                eventID = randomEncounterManager.GetComponent<RandomEvent>().testRandomEvent[randomNumber];
                Debug.Log($"�迭 �ε��� ��ȣ�� {randomNumber}! \n ���� ���ڴ� {eventID}");
                break;

            case "eventBuyFood":
                randomNumber = Random.Range(0, randomEncounterManager.GetComponent<RandomEvent>().eventBuyFood.Length);
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventBuyFood[randomNumber];
                Debug.Log($"�迭 �ε��� ��ȣ�� {randomNumber}! \n ���� ���ڴ� {eventID}");
                break;

            case "eventEarphone":
                randomNumber = Random.Range(0, randomEncounterManager.GetComponent<RandomEvent>().eventEarphone.Length);
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventEarphone[randomNumber];
                Debug.Log($"�迭 �ε��� ��ȣ�� {randomNumber}! \n ���� ���ڴ� {eventID}");
                break;

            case "eventTest1":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest1;
                break;

            case "eventTest2":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest2;
                break;

            case "eventTest3":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest3;
                break;

            case "eventTest4":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest4;
                break;

            case "eventTest5":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest5;
                break;

            case "eventTest6":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest6;
                break;

            case "eventTest7":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest7;
                break;

            case "eventTest8":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest8;
                break;

            case "eventTest9":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest9;
                break;

            case "eventTest10":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest10;
                break;

            case "eventTest11":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest11;
                break;

            case "eventTest12":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest12;
                break;

            case "eventTest13":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest13;
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
            //scrollviewController.AutomaticScroll();

            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        hasSelectedText = 0;
        //currentDialogIndex++;

        if(mainText.RandomEventTest[eventID].mainDialogJumpTo != 0)
        {
            currentDialogIndex = mainText.RandomEventTest[eventID].mainDialogJumpTo;
        }

        typingSpeed = 0.1f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowTextJson : MonoBehaviour
{
    public DialogManager dialogManager;

    public DialogList dialogList01;             // JSON���� �ҷ��� Dialog ������ ����Ʈ
    public Selection01List selectText01;        // JSON���� �ҷ��� Selection ������ ����Ʈ
    public RandomEvent01List randomEvent01;     // JSON���� �ҷ��� RandomEvent ������ ����Ʈ

    /*
     json list ����

    dialogList01.dialogSection01[currentDialogIndex]
    selectText01.selection01[eventNumber]
    randomEvent01.randomEvent01[��¼��]
     */

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
    public int eventNumber;             // �׼� ������ selectEventNumber ����
    public int selectEvent;             // �׼� ������ hasSelectEvent ����

    public TextMeshProUGUI selectText1;
    public TextMeshProUGUI selectText2;
    public TextMeshProUGUI selectText3;

    public GameObject objSelectText1;
    public GameObject objSelectText2;
    public GameObject objSelectText3;

    public int hasSelectedText = 0;

    public int selectJumpToValue;

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

        objEventImage.GetComponent<Image>();
        animEventImage.GetComponent<Animator>();

        objText.text = prevText + " ";

        StartCoroutine(OnTypingText());
    }

    private void Start()
    {
        Canvas.ForceUpdateCanvases();

        dialogList01 = dialogManager.dialogList01;
        selectText01 = dialogManager.selectText01;
        randomEvent01 = dialogManager.randomEvent01;

        currentDialogIndex = dialogList01.dialogSection01[0].number;
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
        while (count < dialogList01.dialogSection01[currentDialogIndex].textContents.Length)
        {
            currText = dialogList01.dialogSection01[currentDialogIndex].textContents.Substring(0, count + 1);
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

        eventNumber = selectText01.selection01[currentEventPath].selectEventNumber;
        selectEvent = dialogList01.dialogSection01[currentDialogIndex].hasSelectEvent;

        if (selectEvent == 1)   // ������ ���
        {
            selectText1.text = selectText01.selection01[eventNumber].selectText1;
            selectText2.text = selectText01.selection01[eventNumber].selectText2;
            selectText3.text = selectText01.selection01[eventNumber].selectText3;

            objSelectText1.SetActive(selectText1.text != "-");
            objSelectText2.SetActive(selectText2.text != "-");
            objSelectText3.SetActive(selectText3.text != "-");

            // �̹��� �ε� (null�� �ƴ� ��쿡��)
            if (!string.IsNullOrEmpty(selectText01.selection01[eventNumber].eventImage) && selectText01.selection01[eventNumber].eventImage != "null")
            {
                animEventImage.Play("ImageFadeUI", -1, 0f);
                objEventImage.sprite = Resources.Load<Sprite>($"{selectText01.selection01[eventNumber].eventImage}");
            }
        }
        else if (selectEvent == 2)   // �̹��� ���
        {
            if (!string.IsNullOrEmpty(dialogList01.dialogSection01[currentDialogIndex].mainImage) && dialogList01.dialogSection01[currentDialogIndex].mainImage != "null")
            {
                animEventImage.Play("ImageFadeUI", -1, 0f);
                objEventImage.sprite = Resources.Load<Sprite>($"{dialogList01.dialogSection01[currentDialogIndex].mainImage}");
            }
        }
        else if (selectEvent == 3)   // ���� ����
        {
            switch (dialogList01.dialogSection01[currentDialogIndex].statType)
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

        else if (selectEvent == 4)   // ������ ȹ��
        {
            Instantiate(itemObject, transform.position, Quaternion.identity, itemUIParent);
        }

        else if (selectEvent == 5)   // ������ �̺�Ʈ ����
        {
            selectJumpToValue = dialogList01.dialogSection01[currentDialogIndex].selectEventJumpTo;
            eventNumber = selectText01.selection01[selectJumpToValue].selectEventNumber;

            objSelectText1.SetActive(true);
            objSelectText2.SetActive(true);
            objSelectText3.SetActive(true);

            selectText1.text = selectText01.selection01[eventNumber].selectText1;
            selectText2.text = selectText01.selection01[eventNumber].selectText2;
            selectText3.text = selectText01.selection01[eventNumber].selectText3;

            if (!string.IsNullOrEmpty(selectText01.selection01[eventNumber].eventImage) && selectText01.selection01[eventNumber].eventImage != "null")
            {
                animEventImage.Play("ImageFadeUI", -1, 0f);
                objEventImage.sprite = Resources.Load<Sprite>($"{selectText01.selection01[eventNumber].eventImage}");
            }
        }

        yield return null;
    }

    public void SelectAndJump1()
    {
        hasSelectedText = 1;
        if(selectText01.selection01[eventNumber].triggerEvent1 != "-")
        {
            eventPath = selectText01.selection01[eventNumber].triggerEvent1;
        }

        ChooseRandomNumber();
        StartCoroutine(ReadEvent());

        objSelectText1.SetActive(false);
        objSelectText2.SetActive(false);
        objSelectText3.SetActive(false);

        currentEventPath++;
    }

    public void SelectAndJump2()
    {
        hasSelectedText = 2;
        if (selectText01.selection01[eventNumber].triggerEvent2 != "-")
        {
            eventPath = selectText01.selection01[eventNumber].triggerEvent2;
        }

        ChooseRandomNumber();
        StartCoroutine(ReadEvent());

        objSelectText1.SetActive(false);
        objSelectText2.SetActive(false);
        objSelectText3.SetActive(false);

        currentEventPath++;
    }

    public void SelectAndJump3()
    {
        hasSelectedText = 3;
        if (selectText01.selection01[eventNumber].triggerEvent3 != "-")
        {
            eventPath = selectText01.selection01[eventNumber].triggerEvent3;
        }

        ChooseRandomNumber();
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
            $"{randomEvent01.randomEvent01[eventID].printResult}, ���̾�α� ��ȣ: {randomEvent01.randomEvent01[eventID].mainDialogJumpTo}");

        yield return new WaitForSeconds(typingSpeed);

        isTyping = false;
        hasSelectedText = 0;

        if (randomEvent01.randomEvent01[eventID].mainDialogJumpTo != 0)
        {
            currentDialogIndex = randomEvent01.randomEvent01[eventID].mainDialogJumpTo;
        }

        typingSpeed = 0.1f;
    }
}
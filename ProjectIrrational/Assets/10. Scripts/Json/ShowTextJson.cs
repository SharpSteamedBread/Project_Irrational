using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowTextJson : MonoBehaviour
{
    public DialogManager dialogManager;

    public DialogList dialogList01;             // JSON에서 불러온 Dialog 데이터 리스트
    public Selection01List selectText01;        // JSON에서 불러온 Selection 데이터 리스트
    public RandomEvent01List randomEvent01;     // JSON에서 불러온 RandomEvent 데이터 리스트

    /*
     json list 사용법

    dialogList01.dialogSection01[currentDialogIndex]
    selectText01.selection01[eventNumber]
    randomEvent01.randomEvent01[어쩌구]
     */

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
    public int eventNumber;             // 액셀 파일의 selectEventNumber 변수
    public int selectEvent;             // 액셀 파일의 hasSelectEvent 변수

    public TextMeshProUGUI selectText1;
    public TextMeshProUGUI selectText2;
    public TextMeshProUGUI selectText3;
    public TextMeshProUGUI selectText4;

    public GameObject objSelectText1;
    public GameObject objSelectText2;
    public GameObject objSelectText3;
    public GameObject objSelectText4;

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

    [Header("진행도 체크")]
    [SerializeField] private MapClearPercent mapClearPercent;
    public int currentMapPercent = 0;

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
        while (count < dialogList01.dialogSection01[currentDialogIndex].textContents.Length)
        {
            currText = dialogList01.dialogSection01[currentDialogIndex].textContents.Substring(0, count + 1);
            objText.text = prevText + currText;

            count++;

            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        currentDialogIndex++;
        currentMapPercent = currentMapPercent + randomEvent01.randomEvent01[eventID].eventEndChecker;

        typingSpeed = 0.01f;
    }

    private IEnumerator CheckSelectEvent()
    {

        eventNumber = selectText01.selection01[currentEventPath].selectEventNumber;
        selectEvent = dialogList01.dialogSection01[currentDialogIndex].hasSelectEvent;

        if (selectEvent == 1)   // 선택지 출력
        {
            selectText1.text = selectText01.selection01[eventNumber].selectText1;
            selectText2.text = selectText01.selection01[eventNumber].selectText2;
            selectText3.text = selectText01.selection01[eventNumber].selectText3;
            //selectText4.text = selectText01.selection01[eventNumber].selectText4;


            objSelectText1.SetActive(selectText1.text != "-");
            objSelectText2.SetActive(selectText2.text != "-");
            objSelectText3.SetActive(selectText3.text != "-");
            objSelectText4.SetActive(selectText4.text != "-");


            // 이미지 로드 (null이 아닌 경우에만)
            if (!string.IsNullOrEmpty(selectText01.selection01[eventNumber].eventImage) && selectText01.selection01[eventNumber].eventImage != "null")
            {
                animEventImage.Play("ImageFadeUI", -1, 0f);
                objEventImage.sprite = Resources.Load<Sprite>($"{selectText01.selection01[eventNumber].eventImage}");
            }
        }
        else if (selectEvent == 2)   // 이미지 출력
        {
            if (!string.IsNullOrEmpty(dialogList01.dialogSection01[currentDialogIndex].mainImage) && dialogList01.dialogSection01[currentDialogIndex].mainImage != "null")
            {
                animEventImage.Play("ImageFadeUI", -1, 0f);
                objEventImage.sprite = Resources.Load<Sprite>($"{dialogList01.dialogSection01[currentDialogIndex].mainImage}");
            }
        }
        else if (selectEvent == 3)   // 스텟 가감
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
                    Debug.Log("아이템 추가를 기대해줘~");
                    break;
            }
        }

        else if (selectEvent == 4)   // 아이템 획득
        {
            Instantiate(itemObject, transform.position, Quaternion.identity, itemUIParent);
        }

        else if (selectEvent == 5)   // 선택지 이벤트 점프
        {
            selectJumpToValue = dialogList01.dialogSection01[currentDialogIndex].selectEventJumpTo;
            eventNumber = selectText01.selection01[selectJumpToValue].selectEventNumber;

            //1번 5번 합친 기능(본문 이동 기능)
            currentDialogIndex = dialogList01.dialogSection01[currentDialogIndex].dialogJumpTo;

            selectText1.text = selectText01.selection01[eventNumber].selectText1;
            selectText2.text = selectText01.selection01[eventNumber].selectText2;
            selectText3.text = selectText01.selection01[eventNumber].selectText3;
            selectText4.text = selectText01.selection01[eventNumber].selectText4;

            objSelectText1.SetActive(selectText1.text != "-");
            objSelectText2.SetActive(selectText2.text != "-");
            objSelectText3.SetActive(selectText3.text != "-");
            objSelectText4.SetActive(selectText4.text != "-");

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
        objSelectText4.SetActive(false);

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
        objSelectText4.SetActive(false);
        
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
        objSelectText4.SetActive(false);

        currentEventPath++;
    }

    public void SelectAndJump4()
    {
        hasSelectedText = 4;
        if (selectText01.selection01[eventNumber].triggerEvent4 != "-")
        {
            eventPath = selectText01.selection01[eventNumber].triggerEvent4;
        }

        ChooseRandomNumber();
        StartCoroutine(ReadEvent());

        objSelectText1.SetActive(false);
        objSelectText2.SetActive(false);
        objSelectText3.SetActive(false);
        objSelectText4.SetActive(false);

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
            case "eventID25":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest26;
                break;
            case "eventID26":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest27;
                break;
            case "eventID27":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest28;
                break;
            case "eventID28":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest29;
                break;
            case "eventID29":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest30;
                break;
            case "eventID30":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest31;
                break;
            case "eventID31":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest32;
                break;
            case "eventID32":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest33;
                break;
            case "eventID33":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest34;
                break;
            case "eventID34":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest35;
                break;
            case "eventID35":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest36;
                break;
            case "eventID36":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest37;
                break;
            case "eventID37":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest38;
                break;
            case "eventID38":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest39;
                break;
            case "eventID39":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest40;
                break;
            case "eventID40":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest41;
                break;
            case "eventID41":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest42;
                break;
            case "eventID42":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest43;
                break;
            case "eventID43":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest44;
                break;
            case "eventID44":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest45;
                break;
            case "eventID45":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest46;
                break;
            case "eventID46":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest47;
                break;
            case "eventID47":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest48;
                break;
            case "eventID48":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest49;
                break;
            case "eventID49":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest50;
                break;
            case "eventID50":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest51;
                break;
            case "eventID51":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest52;
                break;
            case "eventID52":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest53;
                break;
            case "eventID53":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest54;
                break;
            case "eventID54":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest55;
                break;
            case "eventID55":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest56;
                break;
            case "eventID56":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest57;
                break;
            case "eventID57":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest58;
                break;
            case "eventID58":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest59;
                break;
            case "eventID59":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest60;
                break;
            case "eventID60":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest61;
                break;
            case "eventID61":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest62;
                break;
            case "eventID62":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest63;
                break;
            case "eventID63":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest64;
                break;
            case "eventID64":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest65;
                break;
            case "eventID65":
                eventID = randomEncounterManager.GetComponent<RandomEvent>().eventTest66;
                break;
        }
    }

    public IEnumerator ReadEvent()
    {
        yield return new WaitForSeconds(typingSpeed);

        isTyping = false;
        hasSelectedText = 0;

        //mainDialogJumpTo 체크!!!!!!
        if (randomEvent01.randomEvent01[eventID].mainDialogJumpTo != 0)
        {
            Debug.Log("되냐?");
            currentDialogIndex = randomEvent01.randomEvent01[eventID].mainDialogJumpTo;
        }

        Debug.Log($"CurrentEventPath: {currentEventPath}, eventPath: {eventPath}, EventID: {eventID}, " +
    $"{randomEvent01.randomEvent01[eventID].printResult}, 다이얼로그 번호: {randomEvent01.randomEvent01[eventID].mainDialogJumpTo}");

        typingSpeed = 0.01f;
    }
}

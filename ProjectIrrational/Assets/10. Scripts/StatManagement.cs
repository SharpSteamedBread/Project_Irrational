using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI 요소를 위한 네임스페이스 추가
using UnityEngine.SceneManagement;

public class StatManagement : MonoBehaviour
{
    public DialogManager dialogManager;
    public DialogList dialogList01;             // JSON에서 불러온 Dialog 데이터 리스트
    public Selection01List selectText01;        // JSON에서 불러온 Selection 데이터 리스트
    public RandomEvent01List randomEvent01;     // JSON에서 불러온 RandomEvent 데이터 리스트
    public GameObject objTextController;
    public int getCurrDialogIndex;

    public GradientDamageEffect gradientDamageEffect; // 피해 이펙트
    public GradientHealingEffect gradientHealingEffect; // 힐링 이펙트
    
    [Header("스텟 오브젝트")]
    [SerializeField] private int _valueHeart;
    [SerializeField] private int _valueCoin;
    [SerializeField] private int _valueMental;     
    
    // UI 요소를 위한 변수
    [SerializeField] private Image heartImage;  // Heart UI 이미지
    [SerializeField] private Image coinImage;   // Coin UI 이미지
    [SerializeField] private Image mentalImage;  // Mental UI 이미지

    [SerializeField] private GameObject[] buttons; // 버튼 배열


    // 프로퍼티를 통한 스탯 접근
    public int valueHeart
    {
        get { return _valueHeart; }
        set
        {
            _valueHeart = Mathf.Clamp(value, 0, 4);          

             UpdateUI(); // UI 업데이트 호출
        }
    }

    public int valueCoin
    {
        get { return _valueCoin; }
        set
        {
            _valueCoin = Mathf.Clamp(value, 0, 6);
            UpdateUI(); // UI 업데이트 호출
        }
    }

    
    public int valueMental
    {
        get { return _valueMental; }
        set
        {
            _valueMental = Mathf.Clamp(value, 0, 4);
            UpdateUI(); // UI 업데이트 호출
            UpdateButtons(); // 버튼 상태 업데이트
        }
    }      
   
    private void Awake()
    {
        // 인스펙터에서 조정한 값으로 초기화
        valueHeart = _valueHeart;
        valueCoin = _valueCoin;
        valueMental = _valueMental;  
        
        // 현재 활성화된 씬의 이름을 가져옴
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "Scene_Main")
        {
            getCurrDialogIndex = objTextController.GetComponent<ShowTextJson>().currentDialogIndex;
        }
        else if (sceneName == "Scene_Zehupe")
        {
            getCurrDialogIndex = objTextController.GetComponent<ShowTextZehupeJson>().currentDialogIndex;
        }
    }

    private void Start()
    {
        dialogList01 = dialogManager.dialogList01;
        selectText01 = dialogManager.selectText01;
        randomEvent01 = dialogManager.randomEvent01;

        UpdateUI(); // 시작 시 UI 업데이트
        gradientDamageEffect = FindObjectOfType<GradientDamageEffect>();
        gradientHealingEffect = FindObjectOfType<GradientHealingEffect>();

        UpdateButtons(); // 시작 시 버튼 상태 업데이트
    }

    private void UpdateButtons()
    {
        bool isMentalZero = (_valueMental == 0);
        foreach (var button in buttons)
        {
            button.SetActive(isMentalZero); // _valueMental이 0일 때 버튼 활성화
        }
    }


    private void UpdateUI()
    //public  void UpdateUI()
    {
        // 각 스탯의 fill amount를 업데이트
        heartImage.fillAmount = (float)valueHeart / 4; // 최대 4
        coinImage.fillAmount = (float)valueCoin / 6; // 최대 6
        mentalImage.fillAmount = (float)valueMental / 4; // 최대 4        
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
            
            gradientHealingEffect.TakeHealing(); // 힐링 이펙트 호출
        }
        else if (dialogList01.dialogSection01[getCurrDialogIndex].statValue < 0)
        {
            for (int i = 0; i < Mathf.Abs(dialogList01.dialogSection01[getCurrDialogIndex].statValue); ++i)
            {
                valueHeart--;
            }
            gradientDamageEffect.TakeDamage(); // 피해 이펙트 호출
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
            gradientHealingEffect.TakeHealing(); // 힐링 이펙트 호출
        }
        else if (dialogList01.dialogSection01[getCurrDialogIndex].statValue < 0)
        {
            for (int i = 0; i < Mathf.Abs(dialogList01.dialogSection01[getCurrDialogIndex].statValue); ++i)
            {
                valueCoin--;
            }
            gradientDamageEffect.TakeDamage(); // 피해 이펙트 호출
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
            gradientHealingEffect.TakeHealing(); // 힐링 이펙트 호출
        }        

        else if (dialogList01.dialogSection01[getCurrDialogIndex].statValue < 0)
        {
            for (int i = 0; i < Mathf.Abs(dialogList01.dialogSection01[getCurrDialogIndex].statValue); ++i)
            {
                valueMental--;
            }
        }
        gradientDamageEffect.TakeDamage(); // 피해 이펙트 호출
    }
       
}

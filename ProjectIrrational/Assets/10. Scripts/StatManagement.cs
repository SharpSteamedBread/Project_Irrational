using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI ��Ҹ� ���� ���ӽ����̽� �߰�
using UnityEngine.SceneManagement;

public class StatManagement : MonoBehaviour
{
    public DialogManager dialogManager;
    public DialogList dialogList01;             // JSON���� �ҷ��� Dialog ������ ����Ʈ
    public Selection01List selectText01;        // JSON���� �ҷ��� Selection ������ ����Ʈ
    public RandomEvent01List randomEvent01;     // JSON���� �ҷ��� RandomEvent ������ ����Ʈ
    public GameObject objTextController;
    public int getCurrDialogIndex;

    public GradientDamageEffect gradientDamageEffect; // ���� ����Ʈ
    public GradientHealingEffect gradientHealingEffect; // ���� ����Ʈ
    
    [Header("���� ������Ʈ")]
    [SerializeField] private int _valueHeart;
    [SerializeField] private int _valueCoin;
    [SerializeField] private int _valueMental;     
    
    // UI ��Ҹ� ���� ����
    [SerializeField] private Image heartImage;  // Heart UI �̹���
    [SerializeField] private Image coinImage;   // Coin UI �̹���
    [SerializeField] private Image mentalImage;  // Mental UI �̹���

    [SerializeField] private GameObject[] buttons; // ��ư �迭


    // ������Ƽ�� ���� ���� ����
    public int valueHeart
    {
        get { return _valueHeart; }
        set
        {
            _valueHeart = Mathf.Clamp(value, 0, 4);          

             UpdateUI(); // UI ������Ʈ ȣ��
        }
    }

    public int valueCoin
    {
        get { return _valueCoin; }
        set
        {
            _valueCoin = Mathf.Clamp(value, 0, 6);
            UpdateUI(); // UI ������Ʈ ȣ��
        }
    }

    
    public int valueMental
    {
        get { return _valueMental; }
        set
        {
            _valueMental = Mathf.Clamp(value, 0, 4);
            UpdateUI(); // UI ������Ʈ ȣ��
            UpdateButtons(); // ��ư ���� ������Ʈ
        }
    }      
   
    private void Awake()
    {
        // �ν����Ϳ��� ������ ������ �ʱ�ȭ
        valueHeart = _valueHeart;
        valueCoin = _valueCoin;
        valueMental = _valueMental;  
        
        // ���� Ȱ��ȭ�� ���� �̸��� ������
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

        UpdateUI(); // ���� �� UI ������Ʈ
        gradientDamageEffect = FindObjectOfType<GradientDamageEffect>();
        gradientHealingEffect = FindObjectOfType<GradientHealingEffect>();

        UpdateButtons(); // ���� �� ��ư ���� ������Ʈ
    }

    private void UpdateButtons()
    {
        bool isMentalZero = (_valueMental == 0);
        foreach (var button in buttons)
        {
            button.SetActive(isMentalZero); // _valueMental�� 0�� �� ��ư Ȱ��ȭ
        }
    }


    private void UpdateUI()
    //public  void UpdateUI()
    {
        // �� ������ fill amount�� ������Ʈ
        heartImage.fillAmount = (float)valueHeart / 4; // �ִ� 4
        coinImage.fillAmount = (float)valueCoin / 6; // �ִ� 6
        mentalImage.fillAmount = (float)valueMental / 4; // �ִ� 4        
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
            
            gradientHealingEffect.TakeHealing(); // ���� ����Ʈ ȣ��
        }
        else if (dialogList01.dialogSection01[getCurrDialogIndex].statValue < 0)
        {
            for (int i = 0; i < Mathf.Abs(dialogList01.dialogSection01[getCurrDialogIndex].statValue); ++i)
            {
                valueHeart--;
            }
            gradientDamageEffect.TakeDamage(); // ���� ����Ʈ ȣ��
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
            gradientHealingEffect.TakeHealing(); // ���� ����Ʈ ȣ��
        }
        else if (dialogList01.dialogSection01[getCurrDialogIndex].statValue < 0)
        {
            for (int i = 0; i < Mathf.Abs(dialogList01.dialogSection01[getCurrDialogIndex].statValue); ++i)
            {
                valueCoin--;
            }
            gradientDamageEffect.TakeDamage(); // ���� ����Ʈ ȣ��
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
            gradientHealingEffect.TakeHealing(); // ���� ����Ʈ ȣ��
        }        

        else if (dialogList01.dialogSection01[getCurrDialogIndex].statValue < 0)
        {
            for (int i = 0; i < Mathf.Abs(dialogList01.dialogSection01[getCurrDialogIndex].statValue); ++i)
            {
                valueMental--;
            }
        }
        gradientDamageEffect.TakeDamage(); // ���� ����Ʈ ȣ��
    }
       
}

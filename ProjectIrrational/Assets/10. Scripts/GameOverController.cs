
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI ��Ҹ� ���� ���ӽ����̽� �߰�

public class GameOverController : MonoBehaviour
{
    public StatManagement statManagement;
    public GameObject bloodStainsUI;
    public GameObject objDeadUI;
    public static int countGameover = 0;

    [Header("���� ���� ī��Ʈ")]
    [SerializeField] private int inspectorCountGameover; // �ν����Ϳ��� ������ �� �ִ� ����
    [SerializeField] private Image fillAmountImage; // Fill Amount�� ǥ���� UI �̹���

    private void Awake()
    {
        // �ν����Ϳ��� ������ ���� static ������ ����
        countGameover = inspectorCountGameover;
    }

    void Start()
    {
        UpdateFillAmount(); // �ʱ� Fill Amount ������Ʈ
    }

    // Update is called once per frame
    void Update()
    {
        // static ������ ���� �ν����� ������ ������Ʈ
        inspectorCountGameover = countGameover;

        if (statManagement.valueHeart <= 0)
        {
            
            countGameover--; // countGameover�� 1 ����
            statManagement.valueHeart = 2; // valueHeart�� 2�� ȸ��
        }

        if (countGameover <= 1)
        {
            bloodStainsUI.SetActive(true);
        }

        if (countGameover <= 0)
        {
            objDeadUI.SetActive(true);
        }

        // Fill Amount UI ������Ʈ
        UpdateFillAmount();    
        Debug.Log($"����ī��Ʈ: {countGameover}");
    }

    private void UpdateFillAmount()
    {
        // Fill Amount�� countGameover�� ���� ������Ʈ
        fillAmountImage.fillAmount = (float)countGameover / 2; // �ִ� 2
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI ��Ҹ� ����ϱ� ���� ���ӽ����̽� �߰�
using TMPro; // TextMeshPro�� ����ϱ� ���� ���ӽ����̽� �߰�

public class MapClearPercent : MonoBehaviour
{
    public static int querulforest100 = 7;
    public static int zehupePercent100 = 4;

    [SerializeField] private ShowTextJson showTextMapPercent;
    public static int currQuerulforestPercentCount;
    //public int querulForestPercent;
    public float querulForestPercent;

    [SerializeField] private GameObject uiImageToDisable; // ��Ȱ��ȭ�� UI �̹���
    [SerializeField] private TMP_SubMeshUI progressText; // ������� ǥ���� TMP_SubMeshUI

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currQuerulforestPercentCount = showTextMapPercent.currentMapPercent;
        querulForestPercent = (currQuerulforestPercentCount / querulforest100)*50;

        Debug.Log($"���� ���൵ ī��Ʈ: {currQuerulforestPercentCount}, ���� ���൵ �ۼ�Ʈ: {querulForestPercent}");


        // ���൵�� 50 �̻��� �� UI �̹��� ��Ȱ��ȭ
        if (querulForestPercent >= 50)
        {
            uiImageToDisable.SetActive(false);
        }

        // ����� �ؽ�Ʈ ������Ʈ
        // TMP_SubMeshUI�� text �Ӽ��� �����Ƿ�, TMP_Text�� ��ȯ�Ͽ� ����
        /*
        if (progressText is TMP_Text textComponent)
        {
            textComponent.text = $"����� : {querulForestPercent}%";
        }
        */
    }
}

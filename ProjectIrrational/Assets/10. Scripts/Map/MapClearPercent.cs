using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI 요소를 사용하기 위한 네임스페이스 추가
using TMPro; // TextMeshPro를 사용하기 위한 네임스페이스 추가


public class MapClearPercent : MonoBehaviour
{
    public static int querulforest100 = 7;
    public static int zehupePercent100 = 4;

    [SerializeField] private ShowTextJson showTextMapPercent;
    public static int currQuerulforestPercentCount;
    //public int querulForestPercent;
    public float querulForestPercent;

    [SerializeField] private GameObject uiImageToDisable; // 비활성화할 UI 이미지
    [SerializeField] private TMP_Text progressText; // 진행률을 표시할 TMP Text


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currQuerulforestPercentCount = showTextMapPercent.currentMapPercent;
        querulForestPercent = (currQuerulforestPercentCount / querulforest100)*50;

        Debug.Log($"현재 진행도 카운트: {currQuerulforestPercentCount}, 현재 진행도 퍼센트: {querulForestPercent}");


        // 진행도가 50 이상일 때 UI 이미지 비활성화
        if (querulForestPercent >= 50)
        {
            uiImageToDisable.SetActive(false);
        }

        // TMP Text에 진행률 표시
        progressText.text = $"진행률 : {querulForestPercent}%";

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapClearPercent : MonoBehaviour
{
    public static int querulforest100 = 3;
    public static int zehupePercent100 = 4;

    [SerializeField] private ShowTextJson showTextMapPercent;
    public int currQuerulforestPercentCount;
    public int querulForestPercent;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currQuerulforestPercentCount = showTextMapPercent.currentMapPercent;
        querulForestPercent = currQuerulforestPercentCount / querulforest100;

        Debug.Log($"���� ���൵ ī��Ʈ: {currQuerulforestPercentCount}, ���� ���൵ �ۼ�Ʈ: {querulForestPercent}");
    }
}

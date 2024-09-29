using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    public StatManagement statManagement;
    public GameObject objGameOverUI;
    public GameObject objDeadUI;

    public static int countGameover = 0;

    private void Awake()
    {
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (statManagement.valueHeart <= 0 && countGameover <= 2)
        {
            objGameOverUI.SetActive(true);
        }

        if (countGameover >= 3)
        {
            objDeadUI.SetActive(true);
        }

        Debug.Log($"데스카운트: {countGameover}");
    }
}

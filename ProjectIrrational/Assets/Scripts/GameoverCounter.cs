using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameoverCounter : MonoBehaviour
{
    private void OnEnable()
    {
        GameOverController.countGameover++;
    }
}

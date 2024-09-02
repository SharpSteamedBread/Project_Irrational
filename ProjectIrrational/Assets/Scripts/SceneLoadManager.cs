using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
   public void PressStartButton()
    {
        SceneManager.LoadScene("Scene_Main");
    }

}

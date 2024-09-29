/*
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NameInputHandler : MonoBehaviour
{
    public TMP_InputField nameInputField; // TMP 입력 필드
    public GameObject continueUI; // 이어하기 UI 패널

    void Start()
    {
        continueUI.SetActive(false); // 시작 시 이어하기 UI 비활성화
        nameInputField.onEndEdit.AddListener(OnInputSubmit); // 엔터 입력 리스너 추가
    }

    void OnInputSubmit(string input)
    {
        // 엔터 키가 눌렸을 때
        if (Input.GetKeyDown(KeyCode.Return))
        {            
                continueUI.SetActive(true);
                // 입력된 이름을 사용하여 추가 작업 수행 가능            
        }
    }
}
*/


using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NameInputHandler : MonoBehaviour
{
    public TMP_InputField nameInputField; // TMP 입력 필드
    public GameObject continueUI; // 이어하기 UI 패널

    void Start()
    {
        continueUI.SetActive(false); // 시작 시 이어하기 UI 비활성화
        nameInputField.onEndEdit.AddListener(OnInputSubmit); // 엔터 입력 리스너 추가
    }

    void OnInputSubmit(string input)
    {
        // 입력 필드에서 포커스를 잃을 때 호출됨
        if (!string.IsNullOrEmpty(input))
        {
            continueUI.SetActive(true);
            // 입력된 이름을 사용하여 추가 작업 수행 가능
        }
    }

    void Update()
    {
        // 화면을 터치했을 때
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // 터치가 발생하면 입력 필드의 포커스를 잃게 하고 OnInputSubmit 호출
            nameInputField.DeactivateInputField(); // 입력 필드 비활성화
            OnInputSubmit(nameInputField.text); // 현재 입력된 텍스트로 OnInputSubmit 호출
        }
    }
}

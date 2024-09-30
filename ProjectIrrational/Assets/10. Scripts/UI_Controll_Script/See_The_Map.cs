using UnityEngine;
using UnityEngine.UI;

public class See_The_Map : MonoBehaviour
{
    public GameObject uiElement; // 토글할 UI 요소
    public Button toggleButton; // 클릭할 버튼

    private void Start()
    {
        // 버튼 클릭 이벤트에 ToggleUIElement 추가
        toggleButton.onClick.AddListener(ToggleUIElement);
    }

    private void ToggleUIElement()
    {
        // UI 요소의 활성화 상태를 토글
        uiElement.SetActive(!uiElement.activeSelf);
    }
}

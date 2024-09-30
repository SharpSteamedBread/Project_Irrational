using UnityEngine;
using UnityEngine.UI;

public class See_The_Map : MonoBehaviour
{
    public GameObject uiElement; // ����� UI ���
    public Button toggleButton; // Ŭ���� ��ư

    private void Start()
    {
        // ��ư Ŭ�� �̺�Ʈ�� ToggleUIElement �߰�
        toggleButton.onClick.AddListener(ToggleUIElement);
    }

    private void ToggleUIElement()
    {
        // UI ����� Ȱ��ȭ ���¸� ���
        uiElement.SetActive(!uiElement.activeSelf);
    }
}

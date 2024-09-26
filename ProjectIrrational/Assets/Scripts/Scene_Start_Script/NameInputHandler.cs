/*
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NameInputHandler : MonoBehaviour
{
    public TMP_InputField nameInputField; // TMP �Է� �ʵ�
    public GameObject continueUI; // �̾��ϱ� UI �г�

    void Start()
    {
        continueUI.SetActive(false); // ���� �� �̾��ϱ� UI ��Ȱ��ȭ
        nameInputField.onEndEdit.AddListener(OnInputSubmit); // ���� �Է� ������ �߰�
    }

    void OnInputSubmit(string input)
    {
        // ���� Ű�� ������ ��
        if (Input.GetKeyDown(KeyCode.Return))
        {            
                continueUI.SetActive(true);
                // �Էµ� �̸��� ����Ͽ� �߰� �۾� ���� ����            
        }
    }
}
*/


using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NameInputHandler : MonoBehaviour
{
    public TMP_InputField nameInputField; // TMP �Է� �ʵ�
    public GameObject continueUI; // �̾��ϱ� UI �г�

    void Start()
    {
        continueUI.SetActive(false); // ���� �� �̾��ϱ� UI ��Ȱ��ȭ
        nameInputField.onEndEdit.AddListener(OnInputSubmit); // ���� �Է� ������ �߰�
    }

    void OnInputSubmit(string input)
    {
        // �Է� �ʵ忡�� ��Ŀ���� ���� �� ȣ���
        if (!string.IsNullOrEmpty(input))
        {
            continueUI.SetActive(true);
            // �Էµ� �̸��� ����Ͽ� �߰� �۾� ���� ����
        }
    }

    void Update()
    {
        // ȭ���� ��ġ���� ��
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // ��ġ�� �߻��ϸ� �Է� �ʵ��� ��Ŀ���� �Ұ� �ϰ� OnInputSubmit ȣ��
            nameInputField.DeactivateInputField(); // �Է� �ʵ� ��Ȱ��ȭ
            OnInputSubmit(nameInputField.text); // ���� �Էµ� �ؽ�Ʈ�� OnInputSubmit ȣ��
        }
    }
}

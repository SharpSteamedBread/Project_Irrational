
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI ��Ҹ� ���� ���ӽ����̽� �߰�
using TMPro; // TextMeshPro�� ����ϱ� ���� ���ӽ����̽� �߰�

public class GameOverController : MonoBehaviour
{
    public StatManagement statManagement;
    public GameObject bloodStainsUI;
    public GameObject objDeadUI;
    public static int countGameover = 0;

    [Header("���� ���� ī��Ʈ")]
    [SerializeField] private int inspectorCountGameover; // �ν����Ϳ��� ������ �� �ִ� ����
    [SerializeField] private Image fillAmountImage; // Fill Amount�� ǥ���� UI �̹���
    [SerializeField] private Image fadeOutImage; // ���� UI �̹��� (���̵� �ƿ���)
    [SerializeField] private TextMeshProUGUI gameOverText; // TMP �ؽ�Ʈ ������Ʈ

    private void Awake()
    {
        // �ν����Ϳ��� ������ ���� static ������ ����
        countGameover = inspectorCountGameover;
    }

    void Start()
    {
        UpdateFillAmount(); // �ʱ� Fill Amount ������Ʈ
        fadeOutImage.color = new Color(fadeOutImage.color.r, fadeOutImage.color.g, fadeOutImage.color.b, 0); // ������ �� �����Ƽ�� 0���� ����
        gameOverText.color = new Color(gameOverText.color.r, gameOverText.color.g, gameOverText.color.b, 0); // ������ �� �����Ƽ�� 0���� ����
    }

    // Update is called once per frame
    void Update()
    {
        // static ������ ���� �ν����� ������ ������Ʈ
        inspectorCountGameover = countGameover;

        if (statManagement.valueHeart <= 0)
        {
            
            countGameover--; // countGameover�� 1 ����
            statManagement.valueHeart = 2; // valueHeart�� 2�� ȸ��
        }

        if (countGameover <= 1)
        {
            bloodStainsUI.SetActive(true);
        }

        if (countGameover <= 0)
        {
            //objDeadUI.SetActive(true);
            StartCoroutine(FadeInAndShowGameOverUI());
        }

        // Fill Amount UI ������Ʈ
        UpdateFillAmount();    
        Debug.Log($"����ī��Ʈ: {countGameover}");
    }

    private void UpdateFillAmount()
    {
        // Fill Amount�� countGameover�� ���� ������Ʈ
        fillAmountImage.fillAmount = (float)countGameover / 2; // �ִ� 2
    }

    private IEnumerator FadeInAndShowGameOverUI()
    {
        // ���̵� �� ȿ��
        float fadeDuration = 2f; // ���̵� �� ���� �ð�

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float newAlpha = Mathf.Lerp(0, 0.8f, t / fadeDuration);
            fadeOutImage.color = new Color(fadeOutImage.color.r, fadeOutImage.color.g, fadeOutImage.color.b, newAlpha);

            // TMP �ؽ�Ʈ�� �����Ƽ ����
            float textAlpha = Mathf.Lerp(0, 1, t / fadeDuration);
            gameOverText.color = new Color(gameOverText.color.r, gameOverText.color.g, gameOverText.color.b, textAlpha);

            yield return null; // ���� �����ӱ��� ���
        }

        fadeOutImage.color = new Color(fadeOutImage.color.r, fadeOutImage.color.g, fadeOutImage.color.b, 0.8f); // ���� �����Ƽ ����
        gameOverText.color = new Color(gameOverText.color.r, gameOverText.color.g, gameOverText.color.b, 1); // ���� �����Ƽ ����
        objDeadUI.SetActive(true); // objDeadUI Ȱ��ȭ
    }
}

using UnityEngine;
using TMPro; // TMP ����� ���� ���ӽ����̽� �߰�
using UnityEngine.UI;
using System.Collections;

public class StartUIController : MonoBehaviour
{
    public Image startUIImage; // ���� UI �̹���
    public TextMeshProUGUI startUIButton; // ���� UI ��ư (TMP Text�� ����)
    public float fadeDuration = 1f; // ���İ��� ���ϴ� �ð�
    private bool isFading = false; // ������ ����

    private void Start()
    {
        StartCoroutine(FadeInOutButton());
    }

    private void Update()
    {
        // ���콺 Ŭ�� �Ǵ� ��ġ �Է� ����
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            if (!isFading)
            {
                isFading = true; // ������ ���¸� true�� ����
                StopCoroutine(FadeInOutButton());
                StartCoroutine(FadeOutUI());
            }
        }
    }

    private IEnumerator FadeInOutButton()
    {
        Color buttonColor = startUIButton.color;

        while (true)
        {
            // ���İ� �ִ뿡�� �ּҷ�
            for (float t = 0; t < fadeDuration; t += Time.deltaTime)
            {
                float normalizedTime = t / fadeDuration;
                buttonColor.a = Mathf.Lerp(1, 0, normalizedTime);
                startUIButton.color = buttonColor;
                yield return null;
            }
            // ���İ� �ּҿ��� �ִ��
            for (float t = 0; t < fadeDuration; t += Time.deltaTime)
            {
                float normalizedTime = t / fadeDuration;
                buttonColor.a = Mathf.Lerp(0, 1, normalizedTime);
                startUIButton.color = buttonColor;
                yield return null;
            }
        }
    }

    private IEnumerator FadeOutUI()
    {
        float startAlpha = startUIImage.color.a; // ���� ���İ�
        float btnStartAlpha = startUIButton.color.a; // ��ư�� ���� ���İ�

        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, 0, elapsed / fadeDuration);
            Color imgColor = startUIImage.color;
            imgColor.a = alpha;
            startUIImage.color = imgColor;

            Color btnColor = startUIButton.color;
            btnColor.a = Mathf.Lerp(btnStartAlpha, 0, elapsed / fadeDuration); // ��ư�� ���İ�
            startUIButton.color = btnColor;

            yield return null;
        }

        // UI ��Ȱ��ȭ
        startUIImage.gameObject.SetActive(false);
        startUIButton.gameObject.SetActive(false);
    }
}

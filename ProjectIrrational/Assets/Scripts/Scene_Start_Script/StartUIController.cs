using UnityEngine;
using TMPro; // TMP 사용을 위한 네임스페이스 추가
using UnityEngine.UI;
using System.Collections;

public class StartUIController : MonoBehaviour
{
    public Image startUIImage; // 시작 UI 이미지
    public TextMeshProUGUI startUIButton; // 시작 UI 버튼 (TMP Text로 변경)
    public float fadeDuration = 1f; // 알파값이 변하는 시간
    private bool isFading = false; // 깜빡임 상태

    private void Start()
    {
        StartCoroutine(FadeInOutButton());
    }

    private void Update()
    {
        // 마우스 클릭 또는 터치 입력 감지
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            if (!isFading)
            {
                isFading = true; // 깜빡임 상태를 true로 변경
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
            // 알파값 최대에서 최소로
            for (float t = 0; t < fadeDuration; t += Time.deltaTime)
            {
                float normalizedTime = t / fadeDuration;
                buttonColor.a = Mathf.Lerp(1, 0, normalizedTime);
                startUIButton.color = buttonColor;
                yield return null;
            }
            // 알파값 최소에서 최대로
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
        float startAlpha = startUIImage.color.a; // 시작 알파값
        float btnStartAlpha = startUIButton.color.a; // 버튼의 시작 알파값

        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, 0, elapsed / fadeDuration);
            Color imgColor = startUIImage.color;
            imgColor.a = alpha;
            startUIImage.color = imgColor;

            Color btnColor = startUIButton.color;
            btnColor.a = Mathf.Lerp(btnStartAlpha, 0, elapsed / fadeDuration); // 버튼의 알파값
            startUIButton.color = btnColor;

            yield return null;
        }

        // UI 비활성화
        startUIImage.gameObject.SetActive(false);
        startUIButton.gameObject.SetActive(false);
    }
}

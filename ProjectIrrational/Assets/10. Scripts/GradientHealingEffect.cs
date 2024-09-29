
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GradientHealingEffect : MonoBehaviour
{
    public Image damagePanel; // 빨간색 패널 이미지
    public float flashDuration = 0.2f; // 이펙트 지속 시간
    public float fadeDuration = 0.5f; // 페이드 아웃 지속 시간
    public Button damageButton; // 버튼 참조
    public AudioSource damageSound; // 소리 재생을 위한 AudioSource


    private void Start()
    {
        // 패널을 처음에는 보이지 않도록 설정
        damagePanel.gameObject.SetActive(false);

        // 버튼 클릭 이벤트에 TakeDamage 추가
        damageButton.onClick.AddListener(TakeDamage);
    }

    public void TakeDamage()
    {
        // 핸드폰 진동 발생
        Handheld.Vibrate();

        // 소리 재생
        if (damageSound != null)
        {
            damageSound.Play();
        }

        StartCoroutine(FlashDamageEffect());
    }

    private IEnumerator FlashDamageEffect()
    {
        // 패널을 활성화
        damagePanel.gameObject.SetActive(true);

        // 그라디언트 적용
        SetGradient();

        // 즉시 이펙트가 보이도록 설정
        damagePanel.color = new Color(damagePanel.color.r, damagePanel.color.g, damagePanel.color.b, 1f);

        // 잠시 대기 (flashDuration 동안 유지)
        yield return new WaitForSeconds(flashDuration);

        // 알파 값을 서서히 줄이기
        float elapsedTime = 0f;
        Color panelColor = damagePanel.color;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            panelColor.a = alpha;
            damagePanel.color = panelColor;

            elapsedTime += Time.deltaTime;
            yield return null; // 다음 프레임까지 대기
        }

        // 최종적으로 알파 값을 0으로 설정
        panelColor.a = 0f;
        damagePanel.color = panelColor;

        // 패널을 비활성화
        damagePanel.gameObject.SetActive(false);
    }

    private void SetGradient()
    {
        int width = 900; // 텍스처의 너비
        int height = 2100; // 텍스처의 높이
        Texture2D texture = new Texture2D(width, height);

        // 그라디언트 색상 설정
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float normalizedX = (float)x / width;
                float normalizedY = (float)y / height;

                // 원형 그라디언트 계산
                float distance = Vector2.Distance(new Vector2(0.5f, 0.5f), new Vector2(normalizedX, normalizedY));

                // 가운데 투명, 가장자리 녹색
                Color color = Color.green * (0 + distance * 1.3f); // distance에 따라 색상 조정
                color.a = Mathf.Clamp01(color.a); // 알파값 클램프
                texture.SetPixel(x, y, color);
            }
        }

        texture.Apply();

        // 이미지에 텍스처 적용
        damagePanel.sprite = Sprite.Create(texture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f));
        damagePanel.SetNativeSize(); // 이미지 크기 조정

        // 패널의 위치 조정 (중앙에 위치하도록)
        RectTransform rectTransform = damagePanel.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.anchoredPosition = Vector2.zero; // 중앙에 위치
    }
}



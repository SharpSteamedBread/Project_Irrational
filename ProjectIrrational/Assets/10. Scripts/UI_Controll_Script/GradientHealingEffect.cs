
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GradientHealingEffect : MonoBehaviour
{
    public Image damagePanel; // ������ �г� �̹���
    public float flashDuration = 0.2f; // ����Ʈ ���� �ð�
    public float fadeDuration = 0.5f; // ���̵� �ƿ� ���� �ð�
    public Button damageButton; // ��ư ����
    public AudioSource damageSound; // �Ҹ� ����� ���� AudioSource


    private void Start()
    {
        // �г��� ó������ ������ �ʵ��� ����
        damagePanel.gameObject.SetActive(false);

        // ��ư Ŭ�� �̺�Ʈ�� TakeDamage �߰�
        damageButton.onClick.AddListener(TakeDamage);
    }

    public void TakeDamage()
    {
        // �ڵ��� ���� �߻�
        Handheld.Vibrate();

        // �Ҹ� ���
        if (damageSound != null)
        {
            damageSound.Play();
        }

        StartCoroutine(FlashDamageEffect());
    }

    private IEnumerator FlashDamageEffect()
    {
        // �г��� Ȱ��ȭ
        damagePanel.gameObject.SetActive(true);

        // �׶���Ʈ ����
        SetGradient();

        // ��� ����Ʈ�� ���̵��� ����
        damagePanel.color = new Color(damagePanel.color.r, damagePanel.color.g, damagePanel.color.b, 1f);

        // ��� ��� (flashDuration ���� ����)
        yield return new WaitForSeconds(flashDuration);

        // ���� ���� ������ ���̱�
        float elapsedTime = 0f;
        Color panelColor = damagePanel.color;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            panelColor.a = alpha;
            damagePanel.color = panelColor;

            elapsedTime += Time.deltaTime;
            yield return null; // ���� �����ӱ��� ���
        }

        // ���������� ���� ���� 0���� ����
        panelColor.a = 0f;
        damagePanel.color = panelColor;

        // �г��� ��Ȱ��ȭ
        damagePanel.gameObject.SetActive(false);
    }

    private void SetGradient()
    {
        int width = 900; // �ؽ�ó�� �ʺ�
        int height = 2100; // �ؽ�ó�� ����
        Texture2D texture = new Texture2D(width, height);

        // �׶���Ʈ ���� ����
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float normalizedX = (float)x / width;
                float normalizedY = (float)y / height;

                // ���� �׶���Ʈ ���
                float distance = Vector2.Distance(new Vector2(0.5f, 0.5f), new Vector2(normalizedX, normalizedY));

                // ��� ����, �����ڸ� ���
                Color color = Color.green * (0 + distance * 1.3f); // distance�� ���� ���� ����
                color.a = Mathf.Clamp01(color.a); // ���İ� Ŭ����
                texture.SetPixel(x, y, color);
            }
        }

        texture.Apply();

        // �̹����� �ؽ�ó ����
        damagePanel.sprite = Sprite.Create(texture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f));
        damagePanel.SetNativeSize(); // �̹��� ũ�� ����

        // �г��� ��ġ ���� (�߾ӿ� ��ġ�ϵ���)
        RectTransform rectTransform = damagePanel.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.anchoredPosition = Vector2.zero; // �߾ӿ� ��ġ
    }
}



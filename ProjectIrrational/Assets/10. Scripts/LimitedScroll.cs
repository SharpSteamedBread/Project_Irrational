using UnityEngine;
using UnityEngine.UI;

public class LimitedScroll : MonoBehaviour
{
    public ScrollRect scrollRect; // ScrollRect ������Ʈ
    public RectTransform content; // Content RectTransform
    public float xLimit = 800f; // x�� �̵� ���� ����

    private void Update()
    {
        // Content�� ���� ��ġ
        Vector2 position = content.anchoredPosition;

        // x�� ��ġ ����
        position.x = Mathf.Clamp(position.x, -xLimit, xLimit);

        // ���ѵ� ��ġ�� �ٽ� ����
        content.anchoredPosition = position;
    }
}
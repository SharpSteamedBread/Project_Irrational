/*
using UnityEngine;
using UnityEngine.UI;

public class LimitedScroll : MonoBehaviour
{
    public ScrollRect scrollRect; // ScrollRect 컴포넌트
    public RectTransform content; // Content RectTransform
    public float xLimit = 800f; // x축 이동 제한 범위

    private void Update()
    {
        // Content의 현재 위치
        Vector2 position = content.anchoredPosition;

        // x축 위치 제한
        position.x = Mathf.Clamp(position.x, -xLimit, xLimit);

        // 제한된 위치를 다시 설정
        content.anchoredPosition = position;
    }
}
*/

using UnityEngine;
using UnityEngine.UI;

public class LimitedScroll : MonoBehaviour
{
    public ScrollRect scrollRect; // ScrollRect 컴포넌트
    public RectTransform content; // Content RectTransform
    public float xLimit = 800f; // x축 이동 제한 범위
    public float yLimit = 600f; // y축 이동 제한 범위 추가

    private void Update()
    {
        // Content의 현재 위치
        Vector2 position = content.anchoredPosition;

        // x축 위치 제한
        position.x = Mathf.Clamp(position.x, -xLimit, xLimit);

        // y축 위치 제한 추가
        position.y = Mathf.Clamp(position.y, -yLimit, yLimit);

        // 제한된 위치를 다시 설정
        content.anchoredPosition = position;
    }
}


using UnityEngine;
using UnityEngine.UI;

public class MapZoom : MonoBehaviour
{
    public RectTransform content; // Scroll View의 Content
    public float zoomSpeed = 0.1f; // 줌 속도
    public float minZoom = 0.5f; // 최소 줌 비율
    public float maxZoom = 2f; // 최대 줌 비율

    private float currentZoom = 1f; // 현재 줌 비율

    void Update()
    {
        if (Input.touchCount == 2) // 두 손가락 터치 시
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // 이전 위치 계산
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // 터치 간 거리 계산
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // 거리 차이 계산 (반대로 처리)            
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // 줌 비율 조정 (반대로 처리)
            currentZoom -= deltaMagnitudeDiff * zoomSpeed; // 줌 비율을 증가시키도록 수정
            currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

            // 콘텐츠의 스케일 조정
            content.localScale = new Vector3(currentZoom, currentZoom, 1);
        }
    }
}








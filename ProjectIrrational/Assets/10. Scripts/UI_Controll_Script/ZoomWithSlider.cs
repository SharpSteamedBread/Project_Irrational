using UnityEngine;
using UnityEngine.UI;

public class ZoomWithSlider : MonoBehaviour
{
    public RectTransform content; // Scroll View의 Content
    public Slider slider; // 슬라이더
    public float minZoom = 0.0f; // 최소 줌 비율
    public float maxZoom = 2f; // 최대 줌 비율

    private void Start()
    {
        // 슬라이더의 초기값을 설정
        slider.value = 0.5f; // 중간값으로 설정
        UpdateZoom(); // 초기 줌 설정
    }

    // 슬라이더 값 변경 시 호출되는 메소드
    public void OnSliderValueChanged()
    {
        UpdateZoom();
    }

    private void UpdateZoom()
    {
        // 슬라이더 값을 기반으로 줌 비율 계산
        float zoomFactor = Mathf.Lerp(minZoom, maxZoom, slider.value);
        content.localScale = new Vector3(zoomFactor, zoomFactor, 1);

        // 디버그 로그로 값 확인
        Debug.Log("Zoom Factor: " + zoomFactor);
    }
}

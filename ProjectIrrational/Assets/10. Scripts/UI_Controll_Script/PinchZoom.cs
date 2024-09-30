using UnityEngine;

public class PinchZoom : MonoBehaviour
{
    public float perspectiveZoomSpeed = 0.5f;  // 줌인, 줌아웃할 때 속도 (perspective 모드 용)      
    public float orthoZoomSpeed = 0.5f;         // 줌인, 줌아웃할 때 속도 (Orthographic 모드 용)  
    private Camera cam; // 카메라 인스턴스

    void Start()
    {
        // Camera 컴포넌트를 가져옴
        cam = Camera.main; // 또는 GetComponent<Camera>(); 사용 가능
    }

    void Update()
    {
        if (Input.touchCount == 2) // 손가락 2개가 눌렸을 때
        {
            Touch touchZero = Input.GetTouch(0); // 첫번째 손가락 터치를 저장
            Touch touchOne = Input.GetTouch(1); // 두번째 손가락 터치를 저장

            // 터치에 대한 이전 위치값을 각각 저장함
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition; // deltaPosition은 이동 방향 추적할 때 사용
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // 각 프레임에서 터치 사이의 벡터 거리 구함
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude; // magnitude는 두 점간의 거리 비교(벡터)
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // 거리 차이 구함
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // 만약 카메라가 Orthographic 모드라면
            if (cam.orthographic) // cam.isOrthoGraphic -> cam.orthographic
            {
                cam.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;
                cam.orthographicSize = Mathf.Max(cam.orthographicSize, 0.1f);
            }
            else
            {
                cam.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;
                cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, 0.1f, 179.9f);
            }
        }
    }
}

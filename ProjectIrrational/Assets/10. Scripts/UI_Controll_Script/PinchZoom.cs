using UnityEngine;

public class PinchZoom : MonoBehaviour
{
    public float perspectiveZoomSpeed = 0.5f;  // ����, �ܾƿ��� �� �ӵ� (perspective ��� ��)      
    public float orthoZoomSpeed = 0.5f;         // ����, �ܾƿ��� �� �ӵ� (Orthographic ��� ��)  
    private Camera cam; // ī�޶� �ν��Ͻ�

    void Start()
    {
        // Camera ������Ʈ�� ������
        cam = Camera.main; // �Ǵ� GetComponent<Camera>(); ��� ����
    }

    void Update()
    {
        if (Input.touchCount == 2) // �հ��� 2���� ������ ��
        {
            Touch touchZero = Input.GetTouch(0); // ù��° �հ��� ��ġ�� ����
            Touch touchOne = Input.GetTouch(1); // �ι�° �հ��� ��ġ�� ����

            // ��ġ�� ���� ���� ��ġ���� ���� ������
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition; // deltaPosition�� �̵� ���� ������ �� ���
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // �� �����ӿ��� ��ġ ������ ���� �Ÿ� ����
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude; // magnitude�� �� ������ �Ÿ� ��(����)
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // �Ÿ� ���� ����
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // ���� ī�޶� Orthographic �����
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

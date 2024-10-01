
using UnityEngine;
using UnityEngine.UI;

public class MapZoom : MonoBehaviour
{
    public RectTransform content; // Scroll View�� Content
    public float zoomSpeed = 0.1f; // �� �ӵ�
    public float minZoom = 0.5f; // �ּ� �� ����
    public float maxZoom = 2f; // �ִ� �� ����

    private float currentZoom = 1f; // ���� �� ����

    void Update()
    {
        if (Input.touchCount == 2) // �� �հ��� ��ġ ��
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // ���� ��ġ ���
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // ��ġ �� �Ÿ� ���
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // �Ÿ� ���� ��� (�ݴ�� ó��)            
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // �� ���� ���� (�ݴ�� ó��)
            currentZoom -= deltaMagnitudeDiff * zoomSpeed; // �� ������ ������Ű���� ����
            currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

            // �������� ������ ����
            content.localScale = new Vector3(currentZoom, currentZoom, 1);
        }
    }
}








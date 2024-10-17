using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] private AudioSource backgroundMusic; // ����� �ҽ�
    [SerializeField] private Slider volumeSlider; // ���� ���� �����̴�

    private void Start()
    {
        // �ʱ� �������� 0.5�� �����ϰ� AudioSource�� �����̴��� �ݿ�
        backgroundMusic.volume = 0.5f; // �ʱ� ���� ����
        volumeSlider.value = backgroundMusic.volume; // �����̴��� ���� ���� AudioSource�� �������� ����

        // �����̴��� ���� ����� �� UpdateVolume �޼��带 ȣ���ϵ��� �̺�Ʈ �߰�
        volumeSlider.onValueChanged.AddListener(UpdateVolume);
    }

    // ������ ������Ʈ�ϴ� �޼���
    private void UpdateVolume(float value)
    {
        backgroundMusic.volume = value; // AudioSource�� ���� ����
    }
}

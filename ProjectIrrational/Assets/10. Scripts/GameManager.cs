using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] private AudioSource backgroundMusic; // 배경음 소스
    [SerializeField] private Slider volumeSlider; // 볼륨 조절 슬라이더

    private void Start()
    {
        // 초기 볼륨값을 0.5로 설정하고 AudioSource와 슬라이더에 반영
        backgroundMusic.volume = 0.5f; // 초기 볼륨 설정
        volumeSlider.value = backgroundMusic.volume; // 슬라이더의 현재 값을 AudioSource의 볼륨으로 설정

        // 슬라이더의 값이 변경될 때 UpdateVolume 메서드를 호출하도록 이벤트 추가
        volumeSlider.onValueChanged.AddListener(UpdateVolume);
    }

    // 볼륨을 업데이트하는 메서드
    private void UpdateVolume(float value)
    {
        backgroundMusic.volume = value; // AudioSource의 볼륨 설정
    }
}

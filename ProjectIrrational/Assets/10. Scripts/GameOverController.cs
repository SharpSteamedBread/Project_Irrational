
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI 요소를 위한 네임스페이스 추가
using TMPro; // TextMeshPro를 사용하기 위한 네임스페이스 추가

public class GameOverController : MonoBehaviour
{
    public StatManagement statManagement;
    public GameObject bloodStainsUI;
    public GameObject objDeadUI;
    public static int countGameover = 0;

    [Header("게임 오버 카운트")]
    [SerializeField] private int inspectorCountGameover; // 인스펙터에서 조정할 수 있는 변수
    [SerializeField] private Image fillAmountImage; // Fill Amount를 표시할 UI 이미지
    [SerializeField] private Image fadeOutImage; // 검은 UI 이미지 (페이드 아웃용)
    [SerializeField] private TextMeshProUGUI gameOverText; // TMP 텍스트 오브젝트

    private void Awake()
    {
        // 인스펙터에서 조정한 값을 static 변수에 복사
        countGameover = inspectorCountGameover;
    }

    void Start()
    {
        UpdateFillAmount(); // 초기 Fill Amount 업데이트
        fadeOutImage.color = new Color(fadeOutImage.color.r, fadeOutImage.color.g, fadeOutImage.color.b, 0); // 시작할 때 오페시티를 0으로 설정
        gameOverText.color = new Color(gameOverText.color.r, gameOverText.color.g, gameOverText.color.b, 0); // 시작할 때 오페시티를 0으로 설정
    }

    // Update is called once per frame
    void Update()
    {
        // static 변수의 값을 인스펙터 변수로 업데이트
        inspectorCountGameover = countGameover;

        if (statManagement.valueHeart <= 0)
        {
            
            countGameover--; // countGameover를 1 감소
            statManagement.valueHeart = 2; // valueHeart를 2로 회복
        }

        if (countGameover <= 1)
        {
            bloodStainsUI.SetActive(true);
        }

        if (countGameover <= 0)
        {
            //objDeadUI.SetActive(true);
            StartCoroutine(FadeInAndShowGameOverUI());
        }

        // Fill Amount UI 업데이트
        UpdateFillAmount();    
        Debug.Log($"데스카운트: {countGameover}");
    }

    private void UpdateFillAmount()
    {
        // Fill Amount를 countGameover에 따라 업데이트
        fillAmountImage.fillAmount = (float)countGameover / 2; // 최대 2
    }

    private IEnumerator FadeInAndShowGameOverUI()
    {
        // 페이드 인 효과
        float fadeDuration = 2f; // 페이드 인 지속 시간

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float newAlpha = Mathf.Lerp(0, 0.8f, t / fadeDuration);
            fadeOutImage.color = new Color(fadeOutImage.color.r, fadeOutImage.color.g, fadeOutImage.color.b, newAlpha);

            // TMP 텍스트의 오페시티 조절
            float textAlpha = Mathf.Lerp(0, 1, t / fadeDuration);
            gameOverText.color = new Color(gameOverText.color.r, gameOverText.color.g, gameOverText.color.b, textAlpha);

            yield return null; // 다음 프레임까지 대기
        }

        fadeOutImage.color = new Color(fadeOutImage.color.r, fadeOutImage.color.g, fadeOutImage.color.b, 0.8f); // 최종 오페시티 설정
        gameOverText.color = new Color(gameOverText.color.r, gameOverText.color.g, gameOverText.color.b, 1); // 최종 오페시티 설정
        objDeadUI.SetActive(true); // objDeadUI 활성화
    }
}

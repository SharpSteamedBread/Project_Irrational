using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewController : MonoBehaviour
{
    private ScrollRect scrollRect;
    [SerializeField] private RectTransform objContent;  //Content

    [SerializeField] private float space = 65f;

    [SerializeField] private RectTransform textBox;     //Content 자식 텍스트박스
    //viewPort


    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    private void Start()
    {
        Canvas.ForceUpdateCanvases();
    }

    public void AutomaticScroll()
    {
        scrollRect.verticalNormalizedPosition = 0f;
    }

    public void AutomaticScrollOrigin()
    {
        textBox.anchoredPosition = new Vector2(textBox.anchoredPosition.x, -objContent.rect.height);

        //content 박스 height 조정
        objContent.sizeDelta = new Vector2(objContent.sizeDelta.x, textBox.sizeDelta.y);

        scrollRect.verticalNormalizedPosition = 0f;
        //scrollRect.normalizedPosition = new Vector2(0, 0);
    }
}

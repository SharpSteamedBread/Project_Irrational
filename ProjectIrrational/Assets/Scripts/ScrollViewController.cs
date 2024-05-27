using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewController : MonoBehaviour
{
    private ScrollRect scrollRect;
    [SerializeField] private RectTransform objContent;

    [SerializeField] private float space = 65f;

    [SerializeField] private RectTransform textBox;

    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    private void Start()
    {
        Canvas.ForceUpdateCanvases();
    }

    private void Update()
    {
        AutomaticScroll();
    }

    public void AutomaticScroll()
    {
        textBox.anchoredPosition = new Vector2(textBox.anchoredPosition.x, -objContent.rect.height);

        //content 박스 크기 조정
        objContent.sizeDelta = new Vector2(objContent.sizeDelta.x, textBox.sizeDelta.y);

        scrollRect.verticalNormalizedPosition = 0f;
    }
}

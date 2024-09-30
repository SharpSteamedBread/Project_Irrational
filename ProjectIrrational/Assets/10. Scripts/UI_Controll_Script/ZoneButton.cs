
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ZoneButton : MonoBehaviour
{
    public float AlphaThreshold = 0.5f;

    void Start()
    {
        this.GetComponent<Image>().alphaHitTestMinimumThreshold = AlphaThreshold;
    }

}
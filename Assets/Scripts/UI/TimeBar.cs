using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBar : MonoBehaviour
{
    public Image mask;
    float originalSize;

    void Awake()
    {
        originalSize = mask.rectTransform.rect.width;
        gameObject.SetActive(false);
    }

    public void SetSize(float size)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * size);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Custom_Slider : MonoBehaviour
{
    [SerializeField][Range(0, 1.0f)] float fillAmount = 0.0f;

    [SerializeField] Image sliderFill = null;
    [SerializeField] Image sliderHandle = null;

    public void SetAmount(float value)
    {
        fillAmount = value; 

        sliderFill.fillAmount = fillAmount;
        sliderHandle.rectTransform.anchoredPosition = Vector2.zero;
        sliderHandle.rectTransform.anchorMin = new Vector2(fillAmount, 0.5f);
        sliderHandle.rectTransform.anchorMax = new Vector2(fillAmount, 0.5f);
    }

    private void OnGUI()
    {
        sliderFill.fillAmount = fillAmount;
        sliderHandle.rectTransform.anchoredPosition = Vector2.zero;
        sliderHandle.rectTransform.anchorMin = new Vector2(fillAmount, 0.5f);
        sliderHandle.rectTransform.anchorMax = new Vector2(fillAmount, 0.5f);
    }
}

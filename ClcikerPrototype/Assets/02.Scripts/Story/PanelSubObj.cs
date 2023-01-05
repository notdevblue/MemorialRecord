using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelSubObj : MonoBehaviour
{
    [SerializeField] Image[] imgSubObj;

    public void FadeInSubObject(int idx, float duration)
    {
        foreach (var item in imgSubObj)
        {
            item.gameObject.SetActive(false);
        }

        gameObject.SetActive(true);
        imgSubObj[idx].gameObject.SetActive(true);
        GetComponent<CanvasGroup>().DOFade(1.0f, duration);
    }

    public void FadeOutSubObject(float duration)
    {
        GetComponent<CanvasGroup>().DOFade(0.0f, duration).OnComplete(() =>
        {
            foreach (var item in imgSubObj)
            {
                item.gameObject.SetActive(false);
            }

            gameObject.SetActive(false);
        });
    }
}

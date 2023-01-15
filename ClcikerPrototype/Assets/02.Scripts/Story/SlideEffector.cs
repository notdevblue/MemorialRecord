using System.Collections;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SlideEffector : MonoBehaviour
{
    [SerializeField] RectTransform[] slidePanels;

    public void SlideInFrom(Direction direction, float duration)
    {
        slidePanels[(int)direction].DOScaleX(30, duration);
    }

    public void SlideInFrom(Direction direction, float duration, Action onComplete)
    {
        slidePanels[(int)direction].DOScaleX(30, duration).OnComplete(() => onComplete());
    }

    public void SlideOutTo(Direction direction, float duration)
    {
        foreach (var item in slidePanels)
        {
            item.transform.localScale = Vector3.one;
        }

        slidePanels[(int)direction].transform.localScale += Vector3.right * 30;
        slidePanels[(int)direction].DOScaleX(0, duration);
    }

    public void SlideOutTo(Direction direction, float duration, Action onComplete)
    {
        foreach (var item in slidePanels)
        {
            item.transform.localScale = Vector3.one;
        }

        slidePanels[(int)direction].transform.localScale += Vector3.right * 30;
        slidePanels[(int)direction].DOScaleX(0, duration).OnComplete(() => onComplete());
    }
}

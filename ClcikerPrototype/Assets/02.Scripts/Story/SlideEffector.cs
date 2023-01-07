using System.Collections;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class SlideEffector : MonoBehaviour
{
    [SerializeField] RectTransform[] slidePanels;

    public void SlideInFrom(Direction direction, float duration)
    {
        slidePanels[(int)direction].DOScaleX(10, duration);
    }

    public void SlideOutTo(Direction direction, float duration)
    {
        foreach (var item in slidePanels)
        {
            item.transform.localScale = Vector3.one;
        }

        slidePanels[(int)direction].transform.localScale = Vector3.right * 10;
        slidePanels[(int)direction].DOScaleX(0, duration);
    }
}

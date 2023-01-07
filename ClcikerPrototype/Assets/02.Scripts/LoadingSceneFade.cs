using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingSceneFade : MonoBehaviour
{
    [SerializeField] Image image;

    public Action onFadeOutComplete;
    public Action onFadeInComplete;

    private void Awake()
    {
        image.DOFade(1.0f, 2f).OnComplete(() =>
        {
            onFadeOutComplete?.Invoke();
            image.DOFade(0.0f, 2f).OnComplete(() => onFadeInComplete?.Invoke());
        });
    }
}

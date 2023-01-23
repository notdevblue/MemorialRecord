using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllustrationDirector : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] illurstRenderers = null;

    int curIdx = 0;

    public void FadeInIllurst(int idx, float duration)
    {
        curIdx = idx;
        illurstRenderers[curIdx].gameObject.SetActive(true);
        illurstRenderers[curIdx].DOFade(1.0f, duration);
    }

    public void FadeOutIllurst(float duration)
    {
        illurstRenderers[curIdx].DOFade(0.0f, duration);
        illurstRenderers[curIdx].gameObject.SetActive(false);
    }
}

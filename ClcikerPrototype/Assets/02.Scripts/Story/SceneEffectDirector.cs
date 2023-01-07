using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEffectDirector : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] backgrounds = null;
    [SerializeField] Camera cam = null;

    [SerializeField] SlideEffector se;

    [SerializeField] CanvasGroup whiteBlank;
    [SerializeField] CanvasGroup blackBlank;

    int curIdx = -1;

    public void FadeInBackground(int idx, float duration)
    {
        curIdx = idx;

        backgrounds[curIdx].gameObject.SetActive(true);
        backgrounds[curIdx].color = new Color(1, 1, 1, 0);
        backgrounds[curIdx].DOFade(1.0f, duration);
    }

    public void FadeOutBackground(float duration)
    {
        backgrounds[curIdx].DOFade(0.0f, duration).OnComplete(() => backgrounds[curIdx].gameObject.SetActive(false));
    }

    public float ShakeBackground(Axis axis)
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(backgrounds[curIdx].transform.DOMove(10 * (axis == Axis.Horizontal ? Vector2.up : Vector2.right), 0.5f));
        seq.Append(backgrounds[curIdx].transform.DOMove(10 * (axis == Axis.Horizontal ? Vector2.down : Vector2.left), 0.5f));

        seq.SetLoops(10, LoopType.Yoyo);

        seq.OnComplete(() => backgrounds[curIdx].transform.DOMove(Vector2.zero, 0.25f));
        return 1.25f;
    }

    public void FadeInWhiteBlank(float duration)
    {
        whiteBlank.DOFade(1.0f, duration);
    }

    public void FadeOutWhiteBlank(float duration)
    {
        whiteBlank.DOFade(0.0f, duration);
    }

    public void FadeInBlackBlank(float duration)
    {
        blackBlank.DOFade(1.0f, duration);
    }

    public void FadeOutBlackBlank(float duration)
    {
        blackBlank.DOFade(0.0f, duration);
    }

    public void SlideInFrom(Direction direction, float duration)
    {
        se.SlideInFrom(direction, duration);
    }

    public void SlideOutTo(Direction direction, float duration)
    {
        se.SlideOutTo(direction, duration);
    }
}

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

        foreach (var item in backgrounds)
        {
            item.gameObject.gameObject.SetActive(false);
        }

        backgrounds[curIdx].gameObject.SetActive(true);
        backgrounds[curIdx].color = new Color(1, 1, 1, 0);
        backgrounds[curIdx].DOFade(1.0f, duration);
    }

    public void FadeOutBackground(float duration)
    {
        backgrounds[curIdx].DOFade(0.0f, duration).OnComplete(() => backgrounds[curIdx].gameObject.SetActive(false));
    }

    public void ShakeBackground(Axis axis)
    {
        StartCoroutine(Shake(axis, 1.25f));
    }

    private IEnumerator Shake(Axis axis, float duration)
    {
        float timer = 0f;
        Vector2 originPos = backgrounds[curIdx].transform.position;
        while (timer <= duration)
        {
            Debug.Log(Mathf.Cos(timer));
            Vector2 plusPos = (axis == Axis.Horizontal ? Vector2.right : Vector2.up) * Mathf.Cos(timer * 100) / 100;
            backgrounds[curIdx].transform.position += (Vector3)plusPos;

            timer += Time.deltaTime / duration;
            yield return null;
        }
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

    public void DisappearAll(float duration)
    {
        foreach (var item in backgrounds)
        {
            item.DOFade(0.0f, duration).OnComplete(() => item.gameObject.SetActive(false));
        }
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

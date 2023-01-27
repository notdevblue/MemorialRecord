using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelCredit : MonoBehaviour
{
    //95~3250
    [SerializeField] RectTransform imageCreditBg;
    [SerializeField] RectTransform imageCredit;

    [SerializeField] float moveSpeed = 1.0f;

    [SerializeField] Image imageFade;
    [SerializeField] Button btnClose;

    CanvasGroup cg = null;

    public bool isStop = true;

    Vector2 pos = Vector2.zero;

    Vector2[] originPos = { Vector2.zero, Vector2.zero };

    float timer = 0f;

    private void Awake()
    {
        cg = GetComponent<CanvasGroup>();

        originPos[0] = imageCreditBg.anchoredPosition;
        originPos[1] = imageCredit.anchoredPosition;

        btnClose.onClick.AddListener(() =>
        {
            imageFade.gameObject.SetActive(true);
            imageFade.DOFade(1.0f, 1.0f).OnComplete(() =>
            {
                transform.gameObject.SetActive(false);

                imageFade.DOFade(0.0f, 0.0f).OnComplete(() => imageFade.gameObject.SetActive(false));
            });
        });
    }

    public void CallPanel()
    {
        gameObject.SetActive(true);

        cg.alpha = 0;
        imageFade.gameObject.SetActive(true);
        imageFade.DOFade(1.0f, 1.0f).OnComplete(() =>
        {
            cg.alpha = 1;

            imageFade.DOFade(0.0f, 0.0f).OnComplete(() => 
            {
                imageFade.gameObject.SetActive(false);
                isStop = false;
            });
        });
    }

    private void OnEnable()
    {
        timer = 0f;

        imageCreditBg.anchoredPosition = originPos[0];
        imageCredit.anchoredPosition = originPos[1];
    }

    private void Update()
    {
        pos = imageCredit.anchoredPosition;
        pos.y += timer;
        imageCredit.anchoredPosition = pos;
        pos = imageCreditBg.anchoredPosition;
        pos.y = Mathf.Lerp(95, 3250, imageCredit.anchoredPosition.y / imageCredit.sizeDelta.y);
        imageCreditBg.anchoredPosition = pos;

        if (pos.y >= imageCredit.sizeDelta.y)
            isStop = true;

        if(!isStop)
        {
            timer += Time.deltaTime * moveSpeed;
        }
    }

    private void OnDisable()
    {
        isStop = true;
    }
}

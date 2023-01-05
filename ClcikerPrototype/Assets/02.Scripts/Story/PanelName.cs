using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelName : MonoBehaviour
{
    [SerializeField] InputField inputField = null;
    [SerializeField] Button btnOK = null;

    private void Awake()
    {
        inputField.onValueChanged.AddListener((text) =>
        {
            btnOK.interactable = text != "";
        });

        btnOK.onClick.AddListener(OnClickOKBtn);
    }

    public Func<bool> FadeInNameBox(float duration)
    {
        gameObject.SetActive(true);
        GetComponent<CanvasGroup>().DOFade(1, duration);
        return () => { return gameObject.activeSelf; };
    }

    public Func<bool> FadeOutNameBox(float duration)
    {
        GetComponent<CanvasGroup>().DOFade(0, duration).OnComplete(() => gameObject.SetActive(false));
        btnOK.interactable = false;
        return () => { return gameObject.activeSelf; };
    }

    private void OnClickOKBtn()
    {
        FadeOutNameBox(3.0f);
        SaveManager.SaveName(inputField.text);
    }


}

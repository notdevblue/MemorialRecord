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
            text = text.Trim('#');
            text = text.Trim('\'');
            text = text.Trim('"');
            text = text.Trim(',');
            inputField.SetTextWithoutNotify(text);

            btnOK.interactable = text.Trim(' ') != "";
        });

        btnOK.onClick.AddListener(OnClickOKBtn);
    }

    public Func<bool> FadeInNameBox(float duration)
    {
        gameObject.SetActive(true);
        btnOK.interactable = inputField.text.Trim(' ') != "";
        GetComponent<CanvasGroup>().DOFade(1, duration);
        return () => { return !gameObject.activeSelf; };
    }

    public Func<bool> FadeOutNameBox(float duration)
    {
        GetComponent<CanvasGroup>().DOFade(0, duration).OnComplete(() => gameObject.SetActive(false));
        btnOK.interactable = false;
        return () => { return !gameObject.activeSelf; };
    }

    public void SetNamePanelInMain()
    {
        transform.parent.gameObject.SetActive(true);
        btnOK.onClick.RemoveAllListeners();
        btnOK.onClick.AddListener(() => SaveManager.CharName = inputField.text);
        btnOK.onClick.AddListener(() => transform.parent.gameObject.SetActive(false));
        btnOK.onClick.AddListener(() => btnOK.onClick.RemoveAllListeners());
    }

    private void OnClickOKBtn()
    {
        FadeOutNameBox(0.5f);
        SaveManager.CharName = inputField.text;
    }


}

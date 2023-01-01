using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameExitUI : MonoBehaviour
{
    [SerializeField] RectTransform noticeUI;

    [SerializeField] Text textNoticeMsg;
    [SerializeField] Button btnOk;
    [SerializeField] Button btnCancel;

    public void SetNoticePanel(Action onOk, Action onCancel, string okBtnText, string cancelBtnText, string noticeMsg)
    {
        noticeUI.gameObject.SetActive(true);

        textNoticeMsg.text = noticeMsg;
        btnOk.GetComponentInChildren<Text>().text = okBtnText;
        btnCancel.GetComponentInChildren<Text>().text = cancelBtnText;

        onCancel += () => noticeUI.gameObject.SetActive(false);

        btnOk.onClick.AddListener(() => onOk?.Invoke());
        btnOk.onClick.AddListener(() => btnOk.onClick.RemoveAllListeners());

        btnCancel.onClick.AddListener(() => onCancel?.Invoke());
        btnCancel.onClick.AddListener(() => btnCancel.onClick.RemoveAllListeners());
    }

    private void Update()
    {
        if(!noticeUI.gameObject.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                btnCancel.onClick.Invoke();
                noticeUI.gameObject.SetActive(false);
            }
        }
    }
}

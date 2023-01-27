using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelGameNotice : MonoBehaviour
{
    [SerializeField] RectTransform noticeUI;

    [SerializeField] Text textNoticeTitle;
    [SerializeField] Text textNoticeMsg;
    [SerializeField] Text textPrice;
    [SerializeField] Button btnOk;
    [SerializeField] Button btnBuy;
    [SerializeField] Button btnCancel;

    public void SetNoticePanel(Action onOk, Action onCancel, string okBtnText, string cancelBtnText, string noticeTitle, string noticeMsg)
    {
        noticeUI.gameObject.SetActive(true);

        textNoticeTitle.text = noticeTitle;
        textNoticeMsg.text = noticeMsg;
        btnOk.GetComponentInChildren<Text>().text = okBtnText;
        btnCancel.GetComponentInChildren<Text>().text = cancelBtnText;

        btnBuy.gameObject.SetActive(false);
        btnOk.gameObject.SetActive(true);

        onCancel += () => noticeUI.gameObject.SetActive(false);
        onOk += () => noticeUI.gameObject.SetActive(false);

        btnOk.onClick.AddListener(() => onOk?.Invoke());
        btnOk.onClick.AddListener(() => btnOk.onClick.RemoveAllListeners());

        btnCancel.onClick.AddListener(() => onCancel?.Invoke());
        btnCancel.onClick.AddListener(() => btnCancel.onClick.RemoveAllListeners());
    }

    public void SetShopNoticePanel(Action onBuy, Action onCancel, string buyBtnText, string cancelBtnText, string noticeTitle, string noticeMsg, int price)
    {
        noticeUI.gameObject.SetActive(true);

        textNoticeTitle.text = noticeTitle;
        textNoticeMsg.text = noticeMsg;
        textPrice.text = price.ToString();
        btnBuy.GetComponentInChildren<Text>().text = buyBtnText;
        btnCancel.GetComponentInChildren<Text>().text = cancelBtnText;

        btnBuy.gameObject.SetActive(true);
        btnOk.gameObject.SetActive(false);

        onCancel += () => noticeUI.gameObject.SetActive(false);
        onBuy += () => noticeUI.gameObject.SetActive(false);

        btnBuy.onClick.AddListener(() => OnBuy(price, onBuy, onCancel));
        btnBuy.onClick.AddListener(() => btnBuy.onClick.RemoveAllListeners());

        btnCancel.onClick.AddListener(() => onCancel?.Invoke());
        btnCancel.onClick.AddListener(() => btnCancel.onClick.RemoveAllListeners());
    }

    private void OnBuy(int price, Action onSuccess, Action onFailed)
    {
        if (SaveManager.CurInk < price)
        {
            onFailed?.Invoke();
            return;
        }
        else
        {
            SaveManager.CurInk -= price;
            onSuccess?.Invoke();
        }

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

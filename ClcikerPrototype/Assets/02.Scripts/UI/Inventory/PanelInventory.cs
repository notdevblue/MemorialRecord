using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelInventory : MonoBehaviour
{
    [SerializeField] Text _textName;
    [SerializeField] Text _textInfo;

    [SerializeField] Button _btnOk;
    [SerializeField] Button _btnUse;
    [SerializeField] Button _btnCancel;

    [SerializeField] Image _imageIcon;

    public void SetUsePanel(Action onUse, Sprite itemSprite, string textName, string textInfo)
    {
        transform.parent.gameObject.SetActive(true);

        _imageIcon.sprite = itemSprite;

        _btnCancel.gameObject.SetActive(true);
        _btnUse.gameObject.SetActive(true);

        _textName.text = textName;
        _textInfo.text = textInfo;

        _btnUse.onClick.AddListener(() => onUse?.Invoke());
        _btnUse.onClick.AddListener(() => transform.parent.gameObject.SetActive(false));
        _btnUse.onClick.AddListener(() => _btnUse.onClick.RemoveAllListeners());
        _btnUse.onClick.AddListener(() => FindObjectOfType<InventoryManager>(true).Refresh());

        _btnOk.gameObject.SetActive(false);
    }

    public void SetOKPanel(Sprite itemSprite, string textName, string textInfo)
    {
        transform.parent.gameObject.SetActive(true);

        _imageIcon.sprite = itemSprite;

        _btnCancel.gameObject.SetActive(false);
        _btnUse.gameObject.SetActive(false);
        _btnOk.gameObject.SetActive(true);

        _textName.text = textName;
        _textInfo.text = textInfo;
    }
}

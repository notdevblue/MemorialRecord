using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelMusic_Content : MonoBehaviour
{
    [SerializeField] Text _textTitle;
    [SerializeField] Text _textInfo;
    [SerializeField] Image _playImage;

    [SerializeField] Transform _btnLock;
    [SerializeField] Transform _btnUnlock;

    Animator _anim;

    Sprite _playSprite = null;
    Sprite _pauseSprite = null;

    public Toggle _toggle = null;
    public Custom_Slider _slider;
    public Button _btnBuy;
    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
        _anim = GetComponent<Animator>();
    }

    public void InitContent(string title, string info, bool isUnlocked, Sprite playSprite, Sprite pauseSprite)
    {
        _textTitle.text = title;
        _textInfo.text = info;

        _toggle.interactable = isUnlocked;

        _playSprite = playSprite;
        _pauseSprite = pauseSprite;

        if(isUnlocked)
        {
            _btnUnlock.gameObject.SetActive(true);
            _btnLock.gameObject.SetActive(false);
        }
        else
        {
            _btnUnlock.gameObject.SetActive(false);
            _btnLock.gameObject.SetActive(true);
        }

        _toggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    private void OnToggleValueChanged(bool value)
    {
        if(value)
        {
            _anim.SetTrigger("ToBig");
        }
        else
        {
            _anim.SetTrigger("ToSmall");
        }
        _playImage.sprite = value ? _playSprite : _pauseSprite;
    }
}

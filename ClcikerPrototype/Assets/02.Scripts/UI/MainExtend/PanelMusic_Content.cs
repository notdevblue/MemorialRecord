using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelMusic_Content : MonoBehaviour
{
    [SerializeField] Text _textTitle;
    [SerializeField] Text _textInfo;
    [SerializeField] Image _playImage;

    Sprite _playSprite = null;
    Sprite _pauseSprite = null;

    public Toggle _toggle = null;
    public Custom_Slider _slider;
    public Button _btnBuy;

    public void InitContent(string title, string info, bool isUnlocked, Sprite playSprite, Sprite pauseSprite)
    {
        _textTitle.text = title;
        _textInfo.text = info;

        _toggle.interactable = isUnlocked;

        _playSprite = playSprite;
        _pauseSprite = pauseSprite;
    }

    private void OnChangedToggle(bool value)
    {
        _playImage.sprite = value ? _playSprite : _pauseSprite;
    }

    private void OnClickBtn()
    {

    }

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
    }
}

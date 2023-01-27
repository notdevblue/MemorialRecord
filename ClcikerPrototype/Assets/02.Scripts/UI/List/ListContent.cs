using MemorialRecord.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ListContent<T> : MonoBehaviour where T : DataParent
{
    bool isPressed = false;

    [SerializeField] protected Text _textTitle;
    [SerializeField] protected Text _textWriter;
    [SerializeField] protected Text _textOutput;
    [SerializeField] protected Text _textLevelUp;
    [SerializeField] protected Text _textLevelUpCost;
    [SerializeField] protected Text _textReward;

    [SerializeField] protected Image _lockImage = null;
    [SerializeField] protected Image _contentImage = null;
    [SerializeField] protected UpgradeBtn _btnLevelup = null;

    public Action OnLockOff;
    protected T _cachedData = null;

    float timer = 0f;
    float clickInterval = 0.05f;

    protected virtual void Update()
    {
        if(isPressed)
        {
            timer += Time.deltaTime;
            if(clickInterval <= timer)
            {
                LevelUp(_cachedData);
                timer = 0f;
            }
        }
    }

    protected virtual void CommonRefresh(int level)
    {
        _lockImage.gameObject.SetActive(level < 0); 
        _btnLevelup.gameObject.SetActive(level > -1);
    }

    public virtual void InitContent(T data)
    {
        _cachedData = data;
        _contentImage.sprite = data._image;

        _btnLevelup.onButtonDown += () => isPressed = true;
        _btnLevelup.onButtonUp += () => isPressed = false;
        _btnLevelup.onClick.AddListener(() => LevelUp(_cachedData));
    }

    public abstract void RefreshContent(T data);

    protected abstract void LevelUp(T data);
    protected abstract void RefreshBtn(T data, int level);

    private void OnDestroy()
    {
        OnLockOff = null;
    }
}

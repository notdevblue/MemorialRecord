using DG.Tweening;
using MemorialRecord.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ContentListView : MonoBehaviour
{
    [SerializeField] protected ListContent _contentOriginPrefab;
    [SerializeField] protected Transform _contentParent;

    protected DataSO _data = null;

    protected Sequence _seq = null;

    protected List<ListContent> _children = new List<ListContent>();

    public void SetData(DataSO data)
    {
        _data = data;
    }

    protected virtual void InitChildren<T>(List<T> data) where T : DataParent
    {
        foreach (var item in data)
        {
            AddItem(item);
        }
    }

    protected virtual void RefreshItems<T>(List<T> data) where T : DataParent
    {
        _seq = DOTween.Sequence();

        foreach (var item in _children)
        {
            item.GetComponent<CanvasGroup>().alpha = 0f;
        }

        for (int i = 0; i < data.Count; i++)
        {
            _seq.Append(_children[i].GetComponent<CanvasGroup>().DOFade(1f, 0.1f));
        }
        _seq.Play();
    }

    protected virtual void AddItem<T>(T data) where T : DataParent
    {
        ListContent listContent = null;
        listContent = Instantiate(_contentOriginPrefab, _contentParent);

        listContent.InitContent(data);

        _children.Add(listContent);
    }

    public virtual void HideItems(Action callback)
    {
        _seq.OnComplete(() => gameObject.SetActive(false));
        _seq.OnComplete(() => callback?.Invoke());
        _seq.SmoothRewind();
    }

}

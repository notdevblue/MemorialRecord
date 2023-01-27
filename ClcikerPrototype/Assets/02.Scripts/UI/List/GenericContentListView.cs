using System;
using System.Linq;
using System.Collections;
using DG.Tweening;
using MemorialRecord.Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class GenericContentListView<T> : ListView where T : DataParent
{
    [SerializeField] protected ListContent<T> _contentOriginPrefab;
    [SerializeField] protected Transform _contentParent;

    protected InitDataSO _data = null;

    protected Sequence _seq = null;

    protected List<ListContent<T>> _children = new List<ListContent<T>>();

    public override void SetData(InitDataSO data)
    {
        _data = data;
    }

    protected virtual void InitChildren(List<T> data)
    {
        foreach (var item in data)
        {
            AddItem(item).OnLockOff += () => FindObjectsOfType<GenericContentListView<T>>().ToList().ForEach((x) => x.RefreshOnlyData(data));
        }
        _contentParent.position = Vector3.zero;
    }

    protected virtual void RefreshItems(List<T> data)
    {
        _seq = DOTween.Sequence();

        foreach (var item in _children)
        {
            item.GetComponent<CanvasGroup>().alpha = 0f;
        }

        for (int i = 0; i < _children.Count; i++)
        {
            _seq.Append(_children[i].GetComponent<CanvasGroup>().DOFade(1f, 0.1f));
            _children[i].RefreshContent(data[i]);
        }
        _seq.Play();
    }

    public virtual void RefreshOnlyData(List<T> data)
    {
        for (int i = 0; i < _children.Count; i++)
        {
            _children[i].RefreshContent(data[i]);
        }
    }

    protected virtual ListContent<T> AddItem(T data)
    {
        ListContent<T> listContent = null;
        listContent = Instantiate(_contentOriginPrefab, _contentParent);

        listContent.InitContent(data);

        _children.Add(listContent);
        return listContent;
    }

    public override void HideItems(Action callback)
    {
        _seq = DOTween.Sequence();

        foreach (var item in _children)
        {
            item.GetComponent<CanvasGroup>().alpha = 1f;
        }

        for (int i = 0; i < _children.Count; i++)
        {
            _seq.Append(_children[i].GetComponent<CanvasGroup>().DOFade(0f, 0.1f));
        }

        _seq.OnComplete(() => gameObject.SetActive(false));
        _seq.OnComplete(() => callback?.Invoke());
        _seq.Play();
    }

    private void OnDestroy()
    {
        _children.ForEach(x => x.OnLockOff = null);
    }

    public override void InitChildren(List<DataParent> data)
    {
        foreach (var item in data)
        {
            AddItem(item as T).OnLockOff += () => FindObjectsOfType<GenericContentListView<T>>().ToList().ForEach((x) => x.RefreshOnlyData(data));
        }
        _contentParent.position = Vector3.zero;
    }

    public override void RefreshOnlyData(List<DataParent> data)
    {
        for (int i = 0; i < _children.Count; i++)
        {
            _children[i].RefreshContent(data[i] as T);
        }
    }

    public Transform GetTransfrom()
    {
        return gameObject.transform;
    }
}

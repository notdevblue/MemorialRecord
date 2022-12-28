using System;
using System.Linq;
using System.Collections;
using DG.Tweening;
using MemorialRecord.Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ContentListView : MonoBehaviour
{
    [SerializeField] protected ListContent _contentOriginPrefab;
    [SerializeField] protected Transform _contentParent;

    protected InitDataSO _data = null;

    protected Sequence _seq = null;

    protected List<ListContent> _children = new List<ListContent>();

    public void SetData(InitDataSO data)
    {
        _data = data;
    }

    protected virtual void InitChildren<T>(List<T> data) where T : DataParent
    {
        foreach (var item in data)
        {
            AddItem(item).OnLockOff += () => FindObjectsOfType<ContentListView>().ToList().ForEach((x) => x.RefreshOnlyData(data));
        }
        _contentParent.position = Vector3.zero;
    }

    protected virtual void RefreshItems<T>(List<T> data) where T : DataParent
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

    public virtual void RefreshOnlyData<T>(List<T> data) where T : DataParent
    {
        for (int i = 0; i < _children.Count; i++)
        {
            _children[i].RefreshContent(data[i]);
        }
    }

    protected virtual ListContent AddItem<T>(T data) where T : DataParent
    {
        ListContent listContent = null;
        listContent = Instantiate(_contentOriginPrefab, _contentParent);

        listContent.InitContent(data);

        _children.Add(listContent);
        return listContent;
    }

    public virtual void HideItems(Action callback)
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

}

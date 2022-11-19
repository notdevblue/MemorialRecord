using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MemorialRecord.Data;

public class ContentListView_Book : ContentListView
{
    Sequence _seq = null;
    BookListSO _data = null;

    private void Start()
    {
        InitChildren(_data.bookDatas);
        RefreshItems(_data.bookDatas);
    }

    private void OnEnable()
    {
        RefreshItems(_data.bookDatas);
    }

    protected override void InitChildren<T>(List<T> data)
    {
        foreach (var item in data)
        {
            AddItem(item);
        }
    }

    protected override void RefreshItems<T>(List<T> data)
    {
        if(_seq != null)
        {
            foreach (var item in _children)
            {
                item.GetComponent<CanvasGroup>().alpha = 0f;
            }
            _seq.Restart();
        }
        else
        {
            ForceRefreshItems(data);
        }
    }

    protected override void ForceRefreshItems<T>(List<T> data)
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

    protected override void AddItem<T>(T data)
    {
        ListContent listContent = null;
        listContent = Instantiate(_contentOriginPrefab, _contentParent);

        listContent.InitContent(data);

        _children.Add(listContent);
    }
}

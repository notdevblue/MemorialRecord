using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MemorialRecord.Data;

public class ContentListView_Book : ContentListView
{
    private void Start()
    {
        InitChildren(_data._bookListSO.bookDatas);
        RefreshItems(_data._bookListSO.bookDatas);
    }

    private void OnEnable()
    {
        try
        {
            RefreshItems(_data._bookListSO.bookDatas);
        }
        catch
        {
            Debug.Log($"{name}:: We have problem in Refresh Items");
        }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MemorialRecord.Data;
using System;

public class ContentListView_Bookmark : ContentListView
{
    private void Start()
    {
        InitChildren(_data._bookmarkListSO.bookmarkDatas);
        RefreshItems(_data._bookmarkListSO.bookmarkDatas);
    }

    private void OnEnable()
    {
        _contentParent.position = new Vector3(0, 0, 0);
        try
        {
            RefreshItems(_data._bookmarkListSO.bookmarkDatas);
        }
        catch
        {
            Debug.Log($"{name}:: We have problem in Refresh Items");
        }
    }
}

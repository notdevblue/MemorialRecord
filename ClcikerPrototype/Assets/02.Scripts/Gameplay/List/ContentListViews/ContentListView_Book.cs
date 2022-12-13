using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MemorialRecord.Data;
using System;

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
}

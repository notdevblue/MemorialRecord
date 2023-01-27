using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MemorialRecord.Data;

public class ContentListView_Book : GenericContentListView<BookData>
{
    private void Start()
    {
        InitChildren(_data._bookListSO.bookDatas);
        RefreshItems(_data._bookListSO.bookDatas);
    }

    private void OnEnable()
    {
        _contentParent.position = new Vector3(0, 0, 0);
        try
        {
            RefreshItems(_data._bookListSO.bookDatas);
        }
        catch
        {
            Debug.Log($"{name}::We have problem in Refresh Items");
        }
    }
}

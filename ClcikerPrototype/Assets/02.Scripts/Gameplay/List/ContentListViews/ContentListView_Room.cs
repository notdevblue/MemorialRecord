using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MemorialRecord.Data;
using System;

public class ContentListView_Room : ContentListView
{
    private void Start()
    {
        InitChildren(_data._roomListSO.roomDatas);
        RefreshItems(_data._roomListSO.roomDatas);
    }

    private void OnEnable()
    {
        try
        {
            RefreshItems(_data._roomListSO.roomDatas);
        }
        catch
        {
            Debug.Log($"{name}:: We have problem in Refresh Items");
        }
    }
}

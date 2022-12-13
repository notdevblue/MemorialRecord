using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MemorialRecord.Data;
using System;

public class ContentListView_Acc : ContentListView
{
    private void Start()
    {
        InitChildren(_data._accessoryListSO.accessoryDatas);
        RefreshItems(_data._accessoryListSO.accessoryDatas);
    }

    private void OnEnable()
    {
        try
        {
            RefreshItems(_data._accessoryListSO.accessoryDatas);
        }
        catch
        {
            Debug.Log($"{name}:: We have problem in Refresh Items");
        }
    }
}

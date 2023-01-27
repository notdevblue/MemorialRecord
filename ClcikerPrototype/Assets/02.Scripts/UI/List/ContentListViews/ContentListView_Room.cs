using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MemorialRecord.Data;
using System;

public class ContentListView_Room : GenericContentListView<RoomData>
{
    private void Start()
    {
        InitChildren(_data._roomListSO.roomDatas);
        RefreshItems(_data._roomListSO.roomDatas);
    }

    private void OnEnable()
    {
        _contentParent.position = new Vector3(0, 0, 0);
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

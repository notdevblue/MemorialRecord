using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentListView_Research : ContentListView
{
    private void Start()
    {
        InitChildren(_data._accessoryListSO.accessoryDatas);
        RefreshItems(_data._accessoryListSO.accessoryDatas);
    }

    private void OnEnable()
    {
        _contentParent.position = new Vector3(0, 0, 0);
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

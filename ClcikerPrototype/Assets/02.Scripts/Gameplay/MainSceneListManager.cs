using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneListManager : MonoBehaviour
{
    [SerializeField] DataSO _listInfos;
    [SerializeField] ContentListView[] lists;

    private void Awake()
    {
        foreach (var item in lists)
        {
            item.SetData(_listInfos);
        }
    }
}

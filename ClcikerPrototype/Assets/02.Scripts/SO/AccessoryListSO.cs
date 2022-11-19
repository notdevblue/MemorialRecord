using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MemorialRecord.Data;

public class AccessoryListSO : ScriptableObject
{
    public List<AccessoryData> AccessoryDatas;

    private void OnEnable()
    {
        AccessoryDatas = new List<AccessoryData>();
    }


    public AccessoryData this[int idx]
    {
        get
        {
            return AccessoryDatas[idx];
        }
        set
        {
            AccessoryDatas[idx] = value;
        }
    }
}

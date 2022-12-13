using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MemorialRecord.Data;

public class AccessoryListSO : ScriptableObject
{
    public List<AccessoryData> accessoryDatas = new List<AccessoryData>();

    public AccessoryData this[int idx]
    {
        get
        {
            return accessoryDatas[idx];
        }
        set
        {
            accessoryDatas[idx] = value;
        }
    }
}

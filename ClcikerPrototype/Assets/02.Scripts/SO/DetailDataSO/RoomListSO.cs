using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MemorialRecord.Data;

[System.Serializable]
public class RoomListSO : ScriptableObject
{
    public List<RoomData> roomDatas = new List<RoomData>();

    public RoomData this[int idx] 
    { 
        get
        {
            return roomDatas[idx];
        }
        set
        {
            roomDatas[idx] = value;
        }
    }
}

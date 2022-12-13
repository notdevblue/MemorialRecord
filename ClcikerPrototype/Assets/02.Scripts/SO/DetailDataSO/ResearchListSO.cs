using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MemorialRecord.Data;

[System.Serializable]
public class ResearchListSO : ScriptableObject
{
    public List<researchData> researchDatas = new List<researchData>();

    public researchData this[int idx]
    {
        get
        {
            return researchDatas[idx];
        }
        set
        {
            researchDatas[idx] = value;
        }
    }

}

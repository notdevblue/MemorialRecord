using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MemorialRecord.Data;

[System.Serializable]
public class ResearchListSO : ScriptableObject
{
    public List<ResearchData> researchDatas = new List<ResearchData>();

    public ResearchData this[int idx]
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

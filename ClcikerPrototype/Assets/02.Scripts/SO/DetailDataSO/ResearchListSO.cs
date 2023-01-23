using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MemorialRecord.Data;

[System.Serializable]
[CreateAssetMenu(fileName = "ResearchDataListSO", menuName = "ScriptableObject/Research")]
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

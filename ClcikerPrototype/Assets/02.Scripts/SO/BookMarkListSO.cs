using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MemorialRecord.Data;

[System.Serializable]
public class BookMarkListSO : ScriptableObject
{
    public List<BookMarkData> bookMarkDatas;

    private void OnEnable()
    {
        bookMarkDatas = new List<BookMarkData>();
    }


    public BookMarkData this[int idx]
    {
        get
        {
            return bookMarkDatas[idx];
        }
        set
        {
            bookMarkDatas[idx] = value;
        }
    }
}

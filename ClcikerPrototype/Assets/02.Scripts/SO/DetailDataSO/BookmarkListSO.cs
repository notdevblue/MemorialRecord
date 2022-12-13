using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MemorialRecord.Data;

[System.Serializable]
public class BookmarkListSO : ScriptableObject
{
    public List<BookmarkData> bookmarkDatas = new List<BookmarkData>();

    public BookmarkData this[int idx]
    {
        get
        {
            return bookmarkDatas[idx];
        }
        set
        {
            bookmarkDatas[idx] = value;
        }
    }
}

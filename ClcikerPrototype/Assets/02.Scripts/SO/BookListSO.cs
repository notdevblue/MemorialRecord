using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MemorialRecord.Data;

[System.Serializable]
public class BookListSO : ScriptableObject
{
    public List<BookData> bookDatas;

    private void OnEnable()
    {
        bookDatas = new List<BookData>();
    }

    public BookData this[int idx] 
    { 
        get
        {
            return bookDatas[idx];
        }
        set
        {
            bookDatas[idx] = value;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListContent_Book : ListContent
{
    public override void InitContent<T>(T data)
    {
        _textTitle.text = $"{data._title} Lv.{SaveManager.GetContentLevel(DataType.Book, data._idx)}";
        _textWriter.text = data._writer;
    }

    public override void RefreshContent<T>(T data)
    {

    }
}

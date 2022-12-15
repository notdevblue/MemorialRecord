using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListContent_Book : ListContent
{
    public override void InitContent<T>(T data)
    {
        int level = SaveManager.GetContentLevel(DataType.Book, data._idx);
        CommonInit(level);
        _textTitle.text = $"{data._title} Lv.{(level < 0 ? 0 : level)}";
        _textWriter.text = data._writer;
    }

    public override void RefreshContent<T>(T data)
    {
        int level = SaveManager.GetContentLevel(DataType.Book, data._idx);
        CommonInit(level);
        _textTitle.text = $"{data._title} Lv.{(level < 0 ? 0 : level)}";
    }
}

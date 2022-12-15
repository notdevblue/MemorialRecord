using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListContent_Room : ListContent
{
    public override void InitContent<T>(T data)
    {
        int level = SaveManager.GetContentLevel(DataType.Room, data._idx);
        CommonInit(level);
        _textTitle.text = $"{data._name} Lv.{(level < 0 ? 0 : level)}";
    }

    public override void RefreshContent<T>(T data)
    {
        int level = SaveManager.GetContentLevel(DataType.Room, data._idx);
        CommonInit(level);
        _textTitle.text = $"{data._name} Lv.{(level < 0 ? 0 : level)}";
    }
}

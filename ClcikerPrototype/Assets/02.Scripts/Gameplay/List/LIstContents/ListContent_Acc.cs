using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListContent_Acc : ListContent
{
    public override void InitContent<T>(T data)
    {
        int level = SaveManager.GetContentLevel(DataType.Accessory, data._idx);
        _textTitle.text = $"{data._name} Lv.{(level < 0 ? 1 : level)}";
        _textWriter.text = data._writer;
    }

    public override void RefreshContent<T>(T data)
    {

    }
}

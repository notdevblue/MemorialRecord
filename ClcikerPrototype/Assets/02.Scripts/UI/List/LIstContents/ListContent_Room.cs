using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListContent_Room : ListContent
{
    public override void InitContent<T>(T data)
    {
        int level = SaveManager.GetContentLevel(DataType.Room, data._idx);
        CommonRefresh(level);
        _contentImage.sprite = data._image;
        _textTitle.text = $"{data._name} Lv.{(level < 0 ? 0 : level)}";
    }

    public override void RefreshContent<T>(T data)
    {
        int level = SaveManager.GetContentLevel(DataType.Room, data._idx);
        CommonRefresh(level);
        _textTitle.text = $"{data._name} Lv.{(level < 0 ? 0 : level)}";
    }

    protected override void LevelUp<T>(T data)
    {
        throw new System.NotImplementedException();
    }

    protected override void RefreshBtn<T>(T data, int level)
    {
        if (level >= 100)
        {
            _textLevelUp.text = "구매 완료";
            _textLevelUpCost.gameObject.SetActive(false);
            _btnLevelup.onClick.RemoveAllListeners();
        }
        else
        {
            _textLevelUp.text = "구매\n";
            _textLevelUpCost.text = $"{DataManager.GetValueF(ValueCalculator.GetUpgradeValue(DataType.BookMark, data) * 1.7)}";
            _textOutput.text = $"{DataManager.GetValueF(ValueCalculator.GetOutputValue(DataType.BookMark, data) * 5 / 60d)} / 초당";
        }
    }
}

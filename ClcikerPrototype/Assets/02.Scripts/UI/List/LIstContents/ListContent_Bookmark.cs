using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListContent_Bookmark : ListContent
{
    public override void InitContent<T>(T data)
    {
        base.InitContent(data);

        int level = SaveManager.GetContentLevel(DataType.BookMark, data._idx);

        _textTitle.text = $"{data._name} Lv.{(level < 0 ? 0 : level)}";
        _textWriter.text = data._writer;
        _textOutput.text = $"{DataManager.GetValueF(ValueCalculator.GetOutputValue(DataType.BookMark, data) * 5 / 60d)} / 초당";

        CommonRefresh(level);
        RefreshBtn(data, level);
    }

    public override void RefreshContent<T>(T data)
    {
        int level = SaveManager.GetContentLevel(DataType.BookMark, data._idx);
        _textTitle.text = $"{data._name} Lv.{(level < 0 ? 0 : level)}";

        CommonRefresh(level);
        RefreshBtn(data, level);
    }

    protected override void LevelUp<T>(T data)
    {
        if (SaveManager.GetContentLevel(DataType.BookMark, data._idx) >= 100)
            return;

        double upgradeValue = ValueCalculator.GetUpgradeValue(DataType.BookMark, data) * 1.7d;
        if (SaveManager.CurMemorial >= upgradeValue)
        {
            SaveManager.CurMemorial -= upgradeValue;
            SaveManager.ContentLevelUp(data._idx, DataType.BookMark);
            RefreshContent(data);
            RefreshBtn(data, SaveManager.GetContentLevel(DataType.BookMark, data._idx));
        }
    }

    protected override void RefreshBtn<T>(T data, int level)
    {
        if (level == 50 && SaveManager.GetContentLevel(DataType.Accessory, data._idx) == -1)
        {
            SaveManager.SetContentLevel(data._idx, DataType.Accessory, 0);
            OnLockOff();
        }

        if (level >= 100)
        {
            _textLevelUp.text = "완성";
            _textLevelUpCost.gameObject.SetActive(false); 
            _textOutput.text = $"{DataManager.GetValueF((ValueCalculator.GetOutputValue(DataType.BookMark, data) * 20 * (SaveManager.GetContentLevel(DataType.Accessory, data._idx) > 0 ? 2 : 1)) / 60d)} / 초당";
            _btnLevelup.onClick.RemoveAllListeners();
        }
        else
        {
            _textLevelUp.text = "구매\n";
            _textLevelUpCost.text = $"{DataManager.GetValueF(ValueCalculator.GetUpgradeValue(DataType.BookMark, data) * 1.7)}";
            _textOutput.text = $"{DataManager.GetValueF((ValueCalculator.GetOutputValue(DataType.BookMark, data) * 20 * (SaveManager.GetContentLevel(DataType.Accessory, data._idx) > 0 ? 2 : 1)) / 60d)} / 초당";
        }
    }
}

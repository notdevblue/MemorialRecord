using MemorialRecord.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListContent_Acc : ListContent<AccessoryData>
{
    public override void InitContent(AccessoryData data)
    {
        base.InitContent(data);

        int level = SaveManager.GetContentLevel(DataType.Accessory, data._idx);

        _textTitle.text = $"{data._name} Lv.{(level < 0 ? 0 : level)}";
        _textOutput.text = $"{DataManager.GetDataSO()._accessoryListSO.accessoryDatas[data._idx]._name} 의 생산량 2배";

        CommonRefresh(level);
    }

    public override void RefreshContent(AccessoryData data)
    {
        int level = SaveManager.GetContentLevel(DataType.Accessory, data._idx);
        _textTitle.text = $"{data._name} Lv.{(level < 0 ? 0 : level)}";

        CommonRefresh(level);
        RefreshBtn(data, level);
    }

    protected override void LevelUp(AccessoryData data)
    {
        if (SaveManager.GetContentLevel(DataType.Accessory, data._idx) >= 1)
            return;

        double upgradeValue = ValueCalculator.GetUpgradeValueOnLevel(data._idx, 50);
        if (SaveManager.CurMemorial >= upgradeValue)
        {
            SaveManager.CurMemorial -= upgradeValue;
            SaveManager.ContentLevelUp(data._idx, DataType.Accessory);
            RefreshContent(data);
            RefreshBtn(data, SaveManager.GetContentLevel(DataType.Accessory, data._idx));
        }
    }

    protected override void RefreshBtn(AccessoryData data, int level)
    {
        if (level >= 1)
        {
            _textLevelUp.text = "완성";
            _textLevelUpCost.gameObject.SetActive(false);
            _textOutput.text = $"{DataManager.GetDataSO()._bookmarkListSO.bookmarkDatas[data._idx]._name} 의 생산량 2배";
            _btnLevelup.onClick.RemoveAllListeners();
        }
        else if (level == 0)
        {
            _textLevelUp.text = "구매\n";
            _textLevelUpCost.text = $"{DataManager.GetValueF(ValueCalculator.GetUpgradeValueOnLevel(data._idx, 50))}";
            _textOutput.text = $"{DataManager.GetDataSO()._bookmarkListSO.bookmarkDatas[data._idx]._name} 의 생산량 2배";
        }
    }
}

using MemorialRecord.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListContent_Room : ListContent<RoomData>
{
    public override void InitContent(RoomData data)
    {
        base.InitContent(data);

        int level = SaveManager.GetContentLevel(DataType.Room, data._idx);

        _textTitle.text = $"{data._name} Lv.{(level < 0 ? 0 : level)}";
        _textOutput.text = $"모든 생산량 {data._idx + 1}배";

        CommonRefresh(level);
    }

    public override void RefreshContent(RoomData data)
    {
        int level = SaveManager.GetContentLevel(DataType.Room, data._idx);
        _textTitle.text = $"{data._name} Lv.{(level < 0 ? 0 : level)}";

        CommonRefresh(level);
        RefreshBtn(data, level);
    }

    protected override void LevelUp(RoomData data)
    {
        if (SaveManager.GetContentLevel(DataType.Room, data._idx) >= 1)
            return;

        double upgradeValue = GetUpgradeValue(data._idx);
        if (SaveManager.CurMemorial >= upgradeValue)
        {
            SaveManager.CurMemorial -= upgradeValue;
            SaveManager.ContentLevelUp(data._idx, DataType.Room);
            RefreshContent(data);
            RefreshBtn(data, SaveManager.GetContentLevel(DataType.Room, data._idx));
        }
    }

    protected override void RefreshBtn(RoomData data, int level)
    {
        if (level == 1 && SaveManager.GetContentLevel(DataType.Room, data._idx + 1) == -1)
        {
            SaveManager.SetContentLevel(data._idx + 1, DataType.Room, 0);
            OnLockOff();
        }

        if (level >= 1)
        {
            _textLevelUp.text = "구매\n완료";
            _textLevelUpCost.gameObject.SetActive(false);
            _textOutput.text = $"모든 생산량 {data._idx + 1}배";
            _btnLevelup.onClick.RemoveAllListeners();
        }
        else
        {
            _textLevelUp.text = "구매\n";
            _textLevelUpCost.text = $"{DataManager.GetValueF(GetUpgradeValue(data._idx))}";
            _textOutput.text = $"모든 생산량 {data._idx + 1}배";
        }
    }

    private double GetUpgradeValue(int idx)
    {
        switch (idx)
        {
            case 1:
                return 50000;
            case 2:
                return 50000000;
            case 3:
                return 5000000000;
            case 4:
                return 100000000000;
            default:
                return -1;
        }
    }
}

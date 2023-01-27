using MemorialRecord.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListContent_Book : ListContent<BookData>
{
    public override void InitContent(BookData data)
    {
        base.InitContent(data);

        int level = SaveManager.GetContentLevel(DataType.Book, data._idx);
        
        _textTitle.text = $"{data._title} Lv.{(level < 0 ? 0 : level)}";
        _textWriter.text = data._writer;
        _textOutput.text = $"{DataManager.GetValueF(ValueCalculator.GetOutputValue(DataType.Book, data))} / 터치";

        CommonRefresh(level);
        RefreshBtn(data, level);
    }

    public override void RefreshContent(BookData data)
    {
        int level = SaveManager.GetContentLevel(DataType.Book, data._idx);
        _textTitle.text = $"{data._title} Lv.{(level < 0 ? 0 : level)}";

        CommonRefresh(level);
        RefreshBtn(data, level);
    }

    protected override void LevelUp(BookData data)
    {
        if (SaveManager.GetContentLevel(DataType.Book, data._idx) >= 100)
            return;


        double upgradeValue = ValueCalculator.GetUpgradeValue(DataType.Book, data);
        if (SaveManager.CurMemorial >= upgradeValue)
        {
            SaveManager.CurMemorial -= upgradeValue;
            SaveManager.ContentLevelUp(data._idx, DataType.Book);

            if (SaveManager.GetContentLevel(DataType.Book, data._idx) == 1)
            {
                SaveManager.UnlockNewChapter();
                Debug.Log(SaveManager.UnlockedStories.Count);
                FindObjectOfType<StoryAlarm>().CallStoryAlarm(SaveManager.UnlockedStories.Count - 1);
            }

            RefreshContent(data);
            RefreshBtn(data, SaveManager.GetContentLevel(DataType.Book, data._idx));
        }
    }

    protected override void RefreshBtn(BookData data, int level)
    {
        if (level == 20 && SaveManager.GetContentLevel(DataType.Book, data._idx + 1) == -1)
        {
            SaveManager.GetContentLevel(DataType.BookMark, data._idx + 1);

            SaveManager.SetContentLevel(data._idx + 1, DataType.Book, 0);
            SaveManager.SetContentLevel(data._idx + 1, DataType.BookMark, 0);
            OnLockOff();
        }

        if(level >= 100)
        {
            _textLevelUp.text = "완독";
            _textLevelUpCost.gameObject.SetActive(false);
            _textOutput.text = $"{DataManager.GetValueF(ValueCalculator.GetOutputValue(DataType.Book, data))} / 터치";
            _btnLevelup.onClick.RemoveAllListeners();
        }
        else if (level == 0)
        {
            _textLevelUp.text = "구매\n";
            _textLevelUpCost.text = $"{DataManager.GetValueF(ValueCalculator.GetUpgradeValue(DataType.Book, data))}";
            _textOutput.text = $"{DataManager.GetValueF(ValueCalculator.GetOutputValue(DataType.Book, data))} / 터치";
        }
        else
        {
            _textLevelUp.text = "읽기\n";
            _textLevelUpCost.text = $"{DataManager.GetValueF(ValueCalculator.GetUpgradeValue(DataType.Book, data))}";
            _textOutput.text = $"{DataManager.GetValueF(ValueCalculator.GetOutputValue(DataType.Book, data))} / 터치";
        }
    }
}

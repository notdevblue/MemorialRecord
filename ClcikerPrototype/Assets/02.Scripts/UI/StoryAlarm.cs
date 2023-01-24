using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryAlarm : MonoBehaviour
{
    private void Start()
    {
        if(SaveManager.IdxCurStory == 0)
        {
            CustomSceneManager.StorySceneChangeFromMain(0);
            SaveManager.SetStoryDict(0, true);
            SaveManager.IdxCurStory += 1;
        }
        else if(SaveManager.CharName == "$InitalizedPlayerName$")
        {
            FindObjectOfType<PanelName>(true).SetNamePanelInMain();
        }

        if (SaveManager.IsNewUser)
        {
            SaveManager.InventoryItems = new List<Inventory.ItemParent>() { new Inventory.Item_Memorial(0, 1), new Inventory.Item_Ink(0, 1), new Inventory.Item_ResearchResource(0, 1) };
            SaveManager.IsNewUser = false;
        }
    }

    public void CallStoryAlarm(int idx)
    {
        SaveManager.SetStoryDict(idx, false);
        FindObjectOfType<PanelGameNotice>().SetNoticePanel(() => 
        {
            CustomSceneManager.StorySceneChangeFromMain(idx);
            SaveManager.SetStoryDict(idx, true);
        }
        , () => { }
        , "예", "아니오", "책 해금", "새로운 책이 해금되었습니다.\n지금 확인하시겠습니까?");
        SaveManager.IdxCurStory += 1;
    }
}

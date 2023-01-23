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
        else if(SaveManager.CharName == null)
        {
            FindObjectOfType<PanelName>(true).SetNamePanelInMain();
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

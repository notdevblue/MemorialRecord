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
        , "��", "�ƴϿ�", "å �ر�", "���ο� å�� �رݵǾ����ϴ�.\n���� Ȯ���Ͻðڽ��ϱ�?");
        SaveManager.IdxCurStory += 1;
    }
}

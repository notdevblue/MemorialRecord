using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryAlarm : MonoBehaviour
{
    private void Start()
    {
        if(SaveManager.IdxCurStory == 0)
        {
            CallStoryAlarm(0);
        }
    }

    public void CallStoryAlarm(int idx)
    {
        SaveManager.SetStoryDict(idx, false);
        FindObjectOfType<GameNoticePanel>().SetNoticePanel(() => 
        {
            CustomSceneManager.StorySceneChangeFromMain(idx);
            SaveManager.SetStoryDict(idx, true);
        }
        , () => { }
        , "��", "�ƴϿ�", "å �ر�", "���ο� å�� �رݵǾ����ϴ�.\n���� Ȯ���Ͻðڽ��ϱ�?");
        SaveManager.IdxCurStory += 1;
    }
}

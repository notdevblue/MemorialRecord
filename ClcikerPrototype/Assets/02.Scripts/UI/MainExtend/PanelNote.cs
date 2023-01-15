using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelNote : MonoBehaviour
{
    [SerializeField] Button btnRight;
    [SerializeField] Button btnLeft;
    [SerializeField] Button btnClose;

    [SerializeField] Text[] textTitle;
    [SerializeField] Button[] btns;

    [SerializeField] string[] titles;
 
    int curIdx = 0;

    System.Action<int> onChangeChapter;

    private void Awake()
    {
        onChangeChapter += (curIdx) =>
        {
            for (int i = 0; i < 8; i++)
            {
                if(i + (curIdx * 8) > titles.Length - 1)
                {
                    textTitle[i].text = "";
                    btns[i].onClick.RemoveAllListeners();
                    continue;
                }
                int y = i;

                textTitle[i].text = curIdx + i + " " + titles[(curIdx * 8) + i];
                btns[i].onClick.AddListener(() => CustomSceneManager.StorySceneChangeFromMain((curIdx * 8) + y));
            }
        };

        btnRight.onClick.AddListener(() => {
            if (curIdx < (titles.Length - 1) / 8)
            {
                curIdx++;
                onChangeChapter(curIdx);
            }
        });

        btnLeft.onClick.AddListener(() => {
            if (curIdx > 0)
            {
                curIdx--;
                onChangeChapter(curIdx);
            }
        });

        btnClose.onClick.AddListener(() => transform.parent.gameObject.SetActive(false));
        onChangeChapter?.Invoke(curIdx);
    }
}

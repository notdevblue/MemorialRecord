using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelNote : MonoBehaviour
{
    [SerializeField] Button btnRight;
    [SerializeField] Button btnLeft;
    [SerializeField] Button btnClose;

    [SerializeField] Text[] textTitles;
    [SerializeField] Text[] textIdxs;
    [SerializeField] Image[] imageChecks;
    [SerializeField] Button[] btns;
 
    int curIdx = 0;

    System.Action<int> onChangeChapter;

    private void Start()
    {
        InitPanel(curIdx);
        onChangeChapter += InitPanel;
        onChangeChapter += (value) =>
        {
            btnLeft.gameObject.SetActive(curIdx != 0);
            btnRight.gameObject.SetActive(curIdx < (SaveManager.UnlockedStories.Count - 1) / 10);
        };


        btnRight.onClick.AddListener(() => {
            if (curIdx < (SaveManager.UnlockedStories.Count - 1) / 10)
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

    private void OnEnable()
    {
        InitPanel(curIdx);
    }

    public void InitPanel(int curIdx)
    {
        var datas = DataManager.GetDataSO()._bookListSO.bookDatas;

        for (int i = 0; i < textTitles.Length; i++)
        {
            int y = i;

            imageChecks[i].gameObject.SetActive(false);
            imageChecks[i].transform.parent.gameObject.SetActive(true);
            btns[i].onClick.RemoveAllListeners();

            if (i + (curIdx * textTitles.Length) > SaveManager.UnlockedStories.Count - 1)
            {
                textTitles[i].text = "";
                imageChecks[i].transform.parent.gameObject.SetActive(false);
                btns[i].onClick.RemoveAllListeners();
                continue;
            }

            textTitles[i].text = (datas[(curIdx * textTitles.Length) + i]._idx + 1) + " " + datas[(curIdx * textTitles.Length) + i]._title;

            imageChecks[i].gameObject.SetActive(SaveManager.WatchedStories[(curIdx * textTitles.Length) + i]);

            btns[i].onClick.AddListener(() => CustomSceneManager.StorySceneChangeFromMain((curIdx * textTitles.Length) + y));
        }
    }
}

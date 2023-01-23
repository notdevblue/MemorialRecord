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
    [SerializeField] Image[] ImageChecks;
    [SerializeField] Button[] btns;
 
    int curIdx = 0;

    System.Action<int> onChangeChapter;

    int bookCount = 0;

    private void Awake()
    {
        bookCount = SaveManager.GetCountOfUnlockDatas(DataType.Book);

        var datas = DataManager.GetDataSO()._bookListSO.bookDatas;

        for (int i = 0; i < textTitles.Length; i++)
        {
            int y = i;

            if(i + (curIdx * textTitles.Length) > bookCount - 1)
            {
                textTitles[i].text = "";
                btns[i].onClick.RemoveAllListeners();
                continue;
            }

            textTitles[i].text = curIdx + i + " " + datas[(curIdx * textTitles.Length) + i];
            
            btns[i].onClick.AddListener(() => CustomSceneManager.StorySceneChangeFromMain((curIdx * 8) + y));
        }

        onChangeChapter += (curIdx) =>
        {
            for (int i = 0; i < textTitles.Length; i++)
            {
                int y = i;

                btns[i].onClick.RemoveAllListeners();
                if (i + (curIdx * textTitles.Length) > bookCount - 1)
                {
                    textTitles[i].text = "";
                    ImageChecks[i].gameObject.SetActive(false);
                    continue;
                }

                textTitles[i].text = curIdx + i + " " + datas[(curIdx * textTitles.Length) + i];
                btns[i].onClick.AddListener(() => CustomSceneManager.StorySceneChangeFromMain((curIdx * 8) + y));
            }
        };

        btnRight.onClick.AddListener(() => {
            if (curIdx < (bookCount - 1) / 8)
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
        bookCount = SaveManager.GetCountOfUnlockDatas(DataType.Book);
    }
}

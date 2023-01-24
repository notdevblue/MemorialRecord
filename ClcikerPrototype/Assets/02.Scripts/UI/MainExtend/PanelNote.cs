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

    int bookCount = 0;

    private void Awake()
    {
        bookCount = SaveManager.GetCountOfUnlockDatas(DataType.Book);
    }

    private void Start()
    {
        InitPanel(curIdx);
        onChangeChapter += InitPanel;

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

    public void InitPanel(int curIdx)
    {
        var datas = DataManager.GetDataSO()._bookListSO.bookDatas;

        for (int i = 0; i < textTitles.Length; i++)
        {
            int y = i;

            imageChecks[i].gameObject.SetActive(false);
            imageChecks[i].transform.parent.gameObject.SetActive(true);
            btns[i].onClick.RemoveAllListeners();

            if (i + (curIdx * textTitles.Length) > bookCount - 1)
            {
                textTitles[i].text = "";
                imageChecks[i].transform.parent.gameObject.SetActive(false);
                btns[i].onClick.RemoveAllListeners();
                continue;
            }

            textTitles[i].text = (datas[(curIdx * textTitles.Length) + i]._idx + 1) + " " + datas[(curIdx * textTitles.Length) + i]._title;
            
            if((curIdx * textTitles.Length) + i < SaveManager.IdxCurStory)
            {
                imageChecks[i].gameObject.SetActive(true);
            }

            btns[i].onClick.AddListener(() => CustomSceneManager.StorySceneChangeFromMain((curIdx * textTitles.Length) + y));
        }
    }

    private void OnEnable()
    {
        bookCount = SaveManager.GetCountOfUnlockDatas(DataType.Book);
    }
}

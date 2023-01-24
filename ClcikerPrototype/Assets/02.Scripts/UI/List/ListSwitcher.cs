using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListSwitcher : MonoBehaviour
{
    public Button btnClose;
    public Button btnOpen;

    public List<Button> btns;
    public List<ContentListView> switchLists = null;

    private bool isSwitching = false;

    int curIdx = 0;

    private void Start()
    {
        for (int i = 0; i < btns.Count; i++)
        {
            int y = i;
            btns[i].onClick.AddListener(() => {
                if(curIdx != y)
                SwitchList(y);
            });
        }

        btnClose.onClick.AddListener(() =>
        {
            btnClose.GetComponent<RectTransform>().DOAnchorPosX(300, 0.2f);
            btnOpen.gameObject.SetActive(true);
        });

        btnOpen.onClick.AddListener(() =>
        {
            btnClose.GetComponent<RectTransform>().DOAnchorPosX(0, 0.2f);
            btnOpen.gameObject.SetActive(false);
        });
    }

    public void SwitchList(int listNum)
    {
        if (isSwitching)
            return;

        curIdx = listNum;
        isSwitching = true;
        switchLists.Find(x => x.gameObject.activeSelf).HideItems(() =>
        {
            switchLists.ForEach(x => x.gameObject.SetActive(false));
            switchLists[listNum].gameObject.SetActive(true);
            isSwitching = false;
        });
    }
}

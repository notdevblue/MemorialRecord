using DG.Tweening;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomChanger : MonoBehaviour
{
    [SerializeField] Button btnLeft;
    [SerializeField] Button btnRight;

    [SerializeField] Transform[] roomTrms;
    [SerializeField] Button[] middleBtns;

    [SerializeField] Image fadeImage;

    int curIdx = 0;
    int maxIdx = 0;
    private void Awake()
    {
        btnLeft.onClick.AddListener(OnClickLeft);
        btnRight.onClick.AddListener(OnClickRight);
        fadeImage.gameObject.SetActive(true);
    }

    private void Start()
    {
        if (maxIdx < SaveManager.GetRoomValue() - 1)
        {
            maxIdx = SaveManager.GetRoomValue() - 1;
        }
            
        SaveManager.OnUpdateLevel += (idx, type, level) => {
            if(type == DataType.Room && level > 0)
            {
                if (maxIdx < idx)
                {
                    maxIdx = idx;
                    btnLeft.gameObject.SetActive(true);
                    btnRight.gameObject.SetActive(true);
                }
            }
        };

        if (maxIdx == 0)
        {
            btnLeft.gameObject.SetActive(false);
            btnRight.gameObject.SetActive(false);
        }
        else
        {
            btnLeft.gameObject.SetActive(true);
            btnRight.gameObject.SetActive(false);
        }
    }

    private void FadeInOut(int originRoomIdx, int resultRoomIdx, float duration = 1f)
    {
        btnLeft.interactable = false;
        btnRight.interactable = false;

        fadeImage.DOFade(1f, duration / 2f).OnComplete(() =>
        {
            roomTrms[originRoomIdx].gameObject.SetActive(false);
            roomTrms[resultRoomIdx].gameObject.SetActive(true);

            btnLeft.interactable = true;
            btnRight.interactable = true;

            fadeImage.DOFade(0f, duration / 2f);
        });
    }

    private void OnClickLeft()
    {
        if (curIdx + 1 > maxIdx)
        {
            return;
        }

        int originIdx = curIdx;
        curIdx++;

        btnRight.gameObject.SetActive(true);
        btnLeft.gameObject.SetActive(true);

        middleBtns.ToList().ForEach(x => x.gameObject.SetActive(false));
        middleBtns[curIdx].gameObject.SetActive(true);

        FadeInOut(originIdx, curIdx);

        if (curIdx + 1 > maxIdx)
        {
            btnLeft.gameObject.SetActive(false);
        }
    }

    private void OnClickRight()
    {
        if (curIdx - 1 < 0)
        {
            return;
        }

        int originIdx = curIdx;
        curIdx--;

        btnRight.gameObject.SetActive(true);
        btnLeft.gameObject.SetActive(true);

        middleBtns.ToList().ForEach(x => x.gameObject.SetActive(false));
        middleBtns[curIdx].gameObject.SetActive(true);

        FadeInOut(originIdx, curIdx);

        if (curIdx - 1 < 0)
        {
            btnRight.gameObject.SetActive(false);
        }
    }

}

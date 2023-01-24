using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ContentListView_Research : ContentListView
{
    [SerializeField] Button btnExit = null;
    [SerializeField] Image imageFade;
    [SerializeField] Text textResearch = null;

    private void Awake()
    {
        imageFade.gameObject.SetActive(true);
        btnExit.onClick.AddListener(() =>
        {
            imageFade.gameObject.SetActive(true);
            imageFade.DOFade(1.0f, 1.0f).OnComplete(() =>
            {
                transform.parent.parent.gameObject.SetActive(false);

                imageFade.DOFade(0.0f, 0.0f).OnComplete(() => imageFade.gameObject.SetActive(false));
            });
        });

        SaveManager.ResearchSaveData.OnChangeResearchResources += (value) => 
        {
            textResearch.text = $"{value} / {SaveManager.ResearchSaveData.researchResourceMaxCount} °³";
        };
        textResearch.text = $"{SaveManager.ResearchSaveData.ResearchResources} / {SaveManager.ResearchSaveData.researchResourceMaxCount} °³";
    }

    private void Start()
    {
        InitChildren(_data._researchListSO.researchDatas);
        RefreshItems(_data._researchListSO.researchDatas);
    }

    public void Refresh()
    {
        for (int i = 0; i < _children.Count; i++)
        {
            _children[i].RefreshContent(_data._researchListSO.researchDatas[i]);
        }
    }

    protected override void InitChildren<T>(List<T> data)
    {
        foreach (var item in data)
        {
            AddItem(item).OnLockOff += () => FindObjectsOfType<ContentListView>().ToList().ForEach((x) => x.RefreshOnlyData(data));
        }
    }

    protected override void RefreshItems<T>(List<T> data)
    {
        for (int i = 0; i < _children.Count; i++)
        {
            _children[i].RefreshContent(data[i]);
        }
    }

    private void OnEnable()
    {
        try
        {
            RefreshItems(_data._researchListSO.researchDatas);
        }
        catch
        {
            Debug.Log($"{name}:: We have problem in Refresh Items");
        }
    }
}

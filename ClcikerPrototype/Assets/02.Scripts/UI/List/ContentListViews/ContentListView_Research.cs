using DG.Tweening;
using MemorialRecord.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ContentListView_Research : GenericContentListView<ResearchData>
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
            textResearch.text = $"{value} / {SaveManager.ResearchSaveData.researchResourceMaxCount} ��";
        };
        textResearch.text = $"{SaveManager.ResearchSaveData.ResearchResources} / {SaveManager.ResearchSaveData.researchResourceMaxCount} ��";
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

    protected override void InitChildren(List<ResearchData> data)
    {
        foreach (var item in data)
        {
            AddItem(item).OnLockOff += () => FindObjectsOfType<GenericContentListView<ResearchData>>().ToList().ForEach((x) => x.RefreshOnlyData(data));
        }
    }

    protected override void RefreshItems(List<ResearchData> data)
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

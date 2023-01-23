using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ContentListView_Research : ContentListView
{
    [SerializeField] Button btnExit = null;
    [SerializeField] Text textResearch = null;

    private void Awake()
    {
        btnExit.onClick.AddListener(() =>
        {
            FindObjectOfType<SlideEffector>().SlideInFrom(Direction.Bottom, 1f, () =>
            {
                transform.parent.parent.gameObject.SetActive(false);

                FindObjectOfType<SlideEffector>().SlideOutTo(Direction.Bottom, 1f);
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

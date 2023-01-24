using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MainSceneListManager : MonoBehaviour
{
    [SerializeField] InitDataSO _listInfos;
    [SerializeField] List<ContentListView> _contentListViews;
    [SerializeField] List<Transform> _views;
    [SerializeField] List<Toggle> _toggles;

    private void Awake()
    {
        foreach (var item in _contentListViews)
        {
            item.SetData(_listInfos);
        }
    }

    private void Start()
    {
        _toggles[0].Select();

        _toggles.ForEach(x =>
        {
            x.GetComponent<CanvasGroup>().alpha = x.isOn ? 1.0f : 0.5f;
            x.onValueChanged.AddListener(OnAnyTogglesOn);
        });
    }

    private void OnAnyTogglesOn(bool isOn)
    {
        for (int i = 0; i < _views.Count; i++)
        {
            _views[i].gameObject.SetActive(_toggles[i].isOn);
            _toggles[i].GetComponent<CanvasGroup>().alpha = _toggles[i].isOn ? 1.0f : 0.5f;
        }
    }
}

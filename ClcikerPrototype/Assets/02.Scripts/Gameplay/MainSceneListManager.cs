using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using MemorialRecord.Data;

public class MainSceneListManager : MonoBehaviour
{
    [SerializeField] InitDataSO _listInfos;

    [SerializeField] List<ListView> listViews = null;

    [SerializeField] List<Transform> _views;
    [SerializeField] List<Toggle> _toggles;

    private void Awake()
    {
        listViews.ForEach(item =>
        {
            item.SetData(_listInfos);
        });
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

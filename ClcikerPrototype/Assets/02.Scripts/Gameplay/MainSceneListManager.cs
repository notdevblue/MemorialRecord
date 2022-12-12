using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class MainSceneListManager : MonoBehaviour
{
    [SerializeField] DataSO _listInfos;
    [SerializeField] List<Transform> _views;
    [SerializeField] List<Toggle> _toggles;

    private void Awake()
    {
        ContentListView curList = null;
        foreach (var item in _views)
        {
            if(item.TryGetComponent<ContentListView>(out curList))
            {
                curList.SetData(_listInfos);
            }
            else if (item.GetComponentInChildren<ContentListView>() != null)
            {
                item.GetComponentsInChildren<ContentListView>().ToList().ForEach(x => x.SetData(_listInfos));
            }
        }

        _toggles.ForEach(x =>
        {
            x.onValueChanged.AddListener(OnAnyTogglesOn);
        });
    }
    
    private void OnAnyTogglesOn(bool isOn)
    {
        for (int i = 0; i < _views.Count; i++)
        {
            _views[i].gameObject.SetActive(_toggles[i].isOn);
        }
    }
}

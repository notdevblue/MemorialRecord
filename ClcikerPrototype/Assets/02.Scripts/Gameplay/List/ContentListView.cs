using DG.Tweening;
using MemorialRecord.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ContentListView : MonoBehaviour
{
    [SerializeField] protected ListContent _contentOriginPrefab;
    [SerializeField] protected Transform _contentParent;

    protected DataSO _data = null;

    protected Sequence _seq = null;

    protected List<ListContent> _children = new List<ListContent>();

    public void SetData(DataSO data)
    {
        _data = data;
    }

    protected abstract void InitChildren<T>(List<T> data) where T : DataParent;
    protected abstract void RefreshItems<T>(List<T> data) where T : DataParent;
    protected abstract void ForceRefreshItems<T>(List<T> data) where T : DataParent;
    protected abstract void AddItem<T>(T data) where T : DataParent;
}

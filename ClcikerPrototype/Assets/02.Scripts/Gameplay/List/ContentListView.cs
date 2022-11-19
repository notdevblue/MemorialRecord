using MemorialRecord.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ContentListView : MonoBehaviour
{
    [SerializeField] protected ListContent _contentOriginPrefab;
    [SerializeField] protected Transform _contentParent;

    protected List<ListContent> _children = null;

    protected abstract void InitChildren<T>(List<T> data) where T : DataParent;
    protected abstract void RefreshItems<T>(List<T> data) where T : DataParent;
    protected abstract void ForceRefreshItems<T>(List<T> data) where T : DataParent;
    protected abstract void AddItem<T>(T data) where T : DataParent;
}

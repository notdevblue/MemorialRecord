using MemorialRecord.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ListView : MonoBehaviour
{
    public abstract void InitChildren(List<DataParent> data);

    public abstract void RefreshOnlyData(List<DataParent> data);

    public abstract void HideItems(System.Action callback);

    public abstract void SetData(InitDataSO data);
}

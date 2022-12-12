using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListSwitcher : MonoBehaviour
{
    public List<ContentListView> switchLists = null;

    private bool isSwitching = false;

    public void SwitchList(int listNum)
    {
        if (isSwitching)
            return;

        isSwitching = true;
        switchLists.Find(x => x.gameObject.activeSelf).HideItems(() => 
        {
            switchLists[listNum].gameObject.SetActive(true);
            isSwitching = false;
        });
    }
}

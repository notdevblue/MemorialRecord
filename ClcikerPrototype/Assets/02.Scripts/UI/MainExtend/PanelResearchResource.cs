using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelResearchResource : MonoBehaviour
{
    [SerializeField] Image[] images = null;

    public void SetValue(int value)
    {
        foreach (var item in images)
        {
            item.gameObject.SetActive(false);
        }

        for (int i = 0; i < value; i++)
        {
            images[i].gameObject.SetActive(true);
        }
    }
}

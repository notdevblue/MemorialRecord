using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiddleBtns : MonoBehaviour
{
    [SerializeField] Transform[] panels;
    [SerializeField] Button[] btns;

    public void Start()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            int y = i;
            btns[y].onClick.AddListener(() => panels[y].gameObject.SetActive(true));
        }
    }
}   

using DG.Tweening;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtendMenuManager : MonoBehaviour
{
    [SerializeField] Camera main;
    [SerializeField] Camera sub;

    [SerializeField] Image imageFade;
    [SerializeField] bool[] hasEffect;
    [SerializeField] Button[] btns;
    [SerializeField] Transform[] panels;

    private void Awake()
    {
        for (int i = 0; i < btns.Length; i++)
        {
            int y = i;
            if(hasEffect[i])
            {
                btns[i].onClick.AddListener(() =>
                {
                    imageFade.gameObject.SetActive(true);
                    imageFade.DOFade(1.0f, 1f).OnComplete(() =>
                    {
                        panels[y].gameObject.SetActive(true);
                        
                        if (y == 1)
                        {
                            main.gameObject.SetActive(false);
                            sub.gameObject.SetActive(true);
                        }

                       
                        imageFade.DOFade(0.0f, 1f).OnComplete(() => imageFade.gameObject.SetActive(false));
                    });
                });
            }
            else
            {
                btns[i].onClick.AddListener(() => panels[y].gameObject.SetActive(true));
            }
        }
    }
}

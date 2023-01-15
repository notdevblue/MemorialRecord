using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtendMenuManager : MonoBehaviour
{
    [SerializeField] SlideEffector sliderEffector;
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
                    sliderEffector.SlideInFrom(Direction.Bottom, 1f, () =>
                    {
                        panels[y].gameObject.SetActive(true);
                        sliderEffector.SlideOutTo(Direction.Bottom, 1f);
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

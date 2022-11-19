using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ContentBox : MonoBehaviour
{
    protected Image _sr = null;

    private RectTransform _rect;

    [Header("Texts")]
    [SerializeField] protected Text _textTitle = null;
    [SerializeField] protected Text _writerText = null;
    [SerializeField] protected Text _outputText = null;
    [SerializeField] protected Text _levelUpText = null;
    [SerializeField] protected Text _costText;
    [SerializeField] protected Text _rewardText;

    [Header("Button")]
    [SerializeField] protected Button btnLevelup = null;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
    }

}

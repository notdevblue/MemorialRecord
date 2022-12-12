using MemorialRecord.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ListContent : MonoBehaviour
{

    [SerializeField] protected Text _textTitle;
    [SerializeField] protected Text _textWriter;
    [SerializeField] protected Text _textOutput;
    [SerializeField] protected Text _textLevelUp;
    [SerializeField] protected Text _textLevelUpCost;
    [SerializeField] protected Text _textReward;

    [SerializeField] protected Image contentImage = null;
    [SerializeField] protected Button btnLevelup = null;

    private void Awake()
    {
        
    }

    public abstract void InitContent<T>(T data) where T : DataParent;
    public abstract void RefreshContent<T>(T data) where T : DataParent;
}

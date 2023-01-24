using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValuePrinter : MonoBehaviour
{

    [SerializeField] Text _textMemorial = null;
    [SerializeField] Text _textPen = null;

    private void Awake()
    {
        SaveManager.OnChangeMemorial += UpdateMemorialValue;
        SaveManager.OnChangeInk += UpdatePenValue;

        UpdateMemorialValue(SaveManager.CurMemorial);
        UpdatePenValue(SaveManager.CurInk);
    }

    private void Start()
    {
        UpdateMemorialValue(SaveManager.CurMemorial);
        UpdatePenValue(SaveManager.CurInk);
    }

    public void UpdateMemorialValue(double value)
    {
        _textMemorial.text = $"{DataManager.GetValueF(value)}";
    }

    public void UpdatePenValue(long value)
    {
        _textPen.text = $"{DataManager.GetValueF(value)}";
    }

    private void OnDestroy()
    {
        SaveManager.OnChangeMemorial -= UpdateMemorialValue;
        SaveManager.OnChangeInk -= UpdatePenValue;
    }
}

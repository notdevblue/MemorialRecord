using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValuePrinter : MonoBehaviour
{
    public enum ValueType
    {
        memorial,
        pen,
    }

    [SerializeField] Text _textMemorial = null;
    [SerializeField] Text _textPen = null;

    private void Awake()
    {
        SaveManager.OnChangeMemorial += value => UpdateValues(ValueType.memorial, value);
        SaveManager.OnChangeQuillPen += value => UpdateValues(ValueType.pen, value);

        UpdateValues(ValueType.memorial, SaveManager.CurMemorial);
        UpdateValues(ValueType.pen, SaveManager.CurQuillPen);
    }

    public void UpdateValues(ValueType type, double value)
    {
        switch (type)
        {
            case ValueType.memorial:
                _textMemorial.text = $"{DataManager.GetValueF(value)}";
                break;
            case ValueType.pen:
                _textPen.text = $"{DataManager.GetValueF(value)}";
                break;
            default:
                break;
        }
    }
}

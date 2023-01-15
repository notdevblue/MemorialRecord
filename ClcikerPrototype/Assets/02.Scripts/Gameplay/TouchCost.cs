using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchCost : MonoBehaviour
{
    public void OnTouch()
    {
        SaveManager.CurMemorial += GetValuePerTouch();
    }

    private double GetValuePerTouch()
    {
        double value = 0d;
        double roomValue = SaveManager.GetRoomValue();

        InitDataSO datas = DataManager.GetDataSO();

        value += ValueCalculator.CalcBookValues(datas._bookListSO);

        value *= roomValue < 1 ? 1 : roomValue;

        return value;
    }
}

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

        //TODO : 책뿐만 아니라 악세사리나 책갈피 등도 값 산출에 사용하도록 만들 필요가 있음

        value *= roomValue < 1 ? 1 : roomValue;

        return value;
    }
}

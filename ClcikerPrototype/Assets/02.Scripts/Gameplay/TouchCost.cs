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

        //TODO : å�Ӹ� �ƴ϶� �Ǽ��縮�� å���� � �� ���⿡ ����ϵ��� ���� �ʿ䰡 ����

        value *= roomValue < 1 ? 1 : roomValue;

        return value;
    }
}

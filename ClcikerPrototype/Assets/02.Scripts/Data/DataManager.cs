using MemorialRecord.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance = null;
    [SerializeField] private InitDataSO _initDataSO = null;

    float timer = 0f;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this);
    }

    private void OnDestroy()
    {
        SaveManager.SaveData();
    }

    private void Update()
    {
        if(timer >= 1.0f)
        {
            timer = 0.0f;
            SaveManager.CurMemorial += SaveManager.GetBookmarkValuePerSec();
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    public static InitDataSO GetDataSO()
    {
        return Instance._initDataSO;
    }

    public static string GetValueF(double value)
    {
        double roundedValue = Math.Round(value);

        long log = (long)Math.Log10(roundedValue);
        if(log < 3)
        {
            return roundedValue.ToString();
        }
        else if (log < 6)
        {
            return Math.Round((roundedValue / Math.Pow(10, 3)), 2).ToString() + "K";
        }
        else if (log < 9)
        {
            return Math.Round((roundedValue / Math.Pow(10, 6)), 2).ToString() + "M";
        }
        else if (log < 12)
        {
            return Math.Round((roundedValue / Math.Pow(10, 9)), 2).ToString() + "B";
        }
        else if (log < 15)
        {
            return Math.Round((roundedValue / Math.Pow(10, 12)), 2).ToString() + "T";
        }
        else if (log < 18)
        {
            return Math.Round((roundedValue / Math.Pow(10, 15)), 2).ToString() + "Qa";
        }
        else if (log < 21)
        {
            return Math.Round((roundedValue / Math.Pow(10, 18)), 2).ToString() + "Qi";
        }
        else
        {
            return (Math.Round(roundedValue)).ToString();
        }
    }
}

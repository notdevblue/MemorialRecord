using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MemorialRecord.Data;

public static class ValueCalculator
{

    public static double CalcBookValues(BookListSO books)
    {
        double value = 0.0d;
        Func<int, double> func;
        int curItemLevel;
        for (int i = 0; i < books.bookDatas.Count; i++)
        {
            curItemLevel = SaveManager.GetContentLevel(DataType.Book, i);

            if(curItemLevel > -1)
            {
                func = GetBaseFormula(false, i);
                do
                {
                    value += func.Invoke(curItemLevel);
                    --curItemLevel;
                } while (curItemLevel > -1);
            }
        }

        return (value / 100d);
    }

    public static double GetOutputValue(DataType type, DataParent data)
    {
        int curItemLevel = SaveManager.GetContentLevel(type, data._idx);
        Func<int, double> func;
        double value = 0.0d;

        if (curItemLevel > -1)
        {
            func = GetBaseFormula(false, data._idx);
            do
            {
                value += func.Invoke(curItemLevel);
                --curItemLevel;
            } while (curItemLevel > -1);
        }

        return (value / 100d);
    }

    public static double GetOutputValueOnLevel(int idx, int level)
    {
        Func<int, double> func;
        double value = 0.0d;

        if (level > -1)
        {
            func = GetBaseFormula(false, idx);
            do
            {
                value += func.Invoke(level);
                --level;
            } while (level > -1);
        }

        return (value / 100d);
    }

    public static double GetUpgradeValue(DataType type, DataParent data)
    {
        return GetBaseFormula(true, data._idx).Invoke(SaveManager.GetContentLevel(type, data._idx));
    }
    public static double GetUpgradeValueOnLevel(int idx, int level)
    {
        return GetBaseFormula(true, idx).Invoke(level);
    }

    private static Func<int, double> GetBaseFormula(bool isUpgradeFormula, int idx)
    {
        if(isUpgradeFormula)
        {
            switch (idx)
            {
                case 0:
                    return level => { return (Math.Pow(1.1, level)) + level; };
                case 1:
                    return level => { return (Math.Pow(1.12, level)) + 10 * level + 100; };
                case 2:
                    return level => { return (Math.Pow(1.125, level)) + 100 * level + 600; };
                case 3:
                    return level => { return (Math.Pow(1.13, level)) + 1000 * level + 4700; };
                case 4:
                    return level => { return (Math.Pow(1.14, level)) + 5000 * level + 14300; };
                case 5:
                    return level => { return (Math.Pow(1.15, level)) + 20000 * level + 220000; };
                case 6:
                    return level => { return (Math.Pow(1.16, level)) + 50000 * level + 1224000; };
                case 7:
                    return level => { return (Math.Pow(1.165, level)) + 70000 * level + 1400000; };
                case 8:
                    return level => { return (Math.Pow(1.168, level)) + 100000 * level + 2130000; };
                case 9:
                    return level => { return (Math.Pow(1.17, level)) + 130000 * level + 4210500; };
                case 10:
                    return level => { return (Math.Pow(1.18, level)) + 370000 * level + 6520000; };
                case 11:
                    return level => { return (Math.Pow(1.2, level)) + 620000 * level + 13200000; };
                case 12:
                    return level => { return (Math.Pow(1.23, level)) + 970000 * level + 24100000; };
                case 13:
                    return level => { return (Math.Pow(1.25, level)) + 1280000 * level + 79600000; };
                case 14:
                    return level => { return (Math.Pow(1.3, level)) + 2500000 * level + 197200000; };
                default:
                    break;
            }
        }
        else
        {
            switch (idx)
            {
                case 0:
                    return level => { return (60 * level); };
                case 1:
                    return level => { return (180 * level); };
                case 2:
                    return level => { return (250 * level); };
                case 3:
                    return level => { return (500 * level); };
                case 4:
                    return level => { return (720 * level); };
                case 5:
                    return level => { return (1320 * level); };
                case 6:
                    return level => { return (3940 * level); };
                case 7:
                    return level => { return (4500 * level); };
                case 8:
                    return level => { return (9000 * level); };
                case 9:
                    return level => { return (12100 * level); };
                case 10:
                    return level => { return (31300 * level); };
                case 11:
                    return level => { return (43000 * level); };
                case 12:
                    return level => { return (56000 * level); };
                case 13:
                    return level => { return (177000 * level); };
                case 14:
                    return level => { return (250000 * level); };
                default:
                    break;
            }
        }

        return level => -1;
    }

}

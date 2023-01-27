using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchManager : MonoBehaviour
{
    public bool _isResearching = false;

    Action _onUpgradeCompelete = null;

    public void Update()
    {
        if(_isResearching)
        {
            SaveManager.ResearchSaveData.researchRemainTime -= Time.deltaTime;
            if(SaveManager.ResearchSaveData.researchRemainTime <= 0)
            {
                _onUpgradeCompelete?.Invoke();
            }
        }
    }

    public void SetResearch(int idx, Action onComplete)
    {
        if (idx == -1)
            return;

        _isResearching = true;
        SaveManager.ResearchSaveData.curResearchIdx = idx;
        SaveManager.ResearchSaveData.researchRemainTime = GetUpgradeTime(idx);

        _onUpgradeCompelete = onComplete;
        _onUpgradeCompelete += () => _onUpgradeCompelete = null;
        _onUpgradeCompelete += () => _isResearching = false;
        _onUpgradeCompelete += () => SaveManager.RefreshResearch();
        _onUpgradeCompelete += () => SaveManager.ResearchSaveData.curResearchIdx = -1;
        _onUpgradeCompelete += () => FindObjectOfType<ContentListView_Research>(true).Refresh();
    }

    public void SetResearch(int idx, double time, Action onComplete)
    {
        if (idx == -1)
            return;

        _isResearching = true;
        SaveManager.ResearchSaveData.curResearchIdx = idx;
        SaveManager.ResearchSaveData.researchRemainTime = time;

        _onUpgradeCompelete = onComplete;
        _onUpgradeCompelete += () => _onUpgradeCompelete = null;
        _onUpgradeCompelete += () => _isResearching = false;
        _onUpgradeCompelete += () => SaveManager.RefreshResearch();
        _onUpgradeCompelete += () => SaveManager.ResearchSaveData.curResearchIdx = -1;
        _onUpgradeCompelete += () => FindObjectOfType<ContentListView_Research>(true)?.Refresh();
    }

    /// <summary>
    /// 다음 레벨로 올라가는데 필요한 연구 시간을 보여줍니다.
    /// </summary>
    /// <param name="idx"></param>
    /// <returns></returns>
    public double GetUpgradeTime(int idx)
    {
        switch (idx)
        {
            case 0:
                return 30f;
            case 1:
                return 60 * 3; // 3분
            case 2:
                switch (SaveManager.GetContentLevel(DataType.Research, 2))
                {
                    case 0:
                        return 60 * 1;
                    case 1:
                        return 60 * 3;
                    case 2:
                        return 60 * 5;
                }
                return double.MaxValue;
            case 3:
                switch (SaveManager.GetContentLevel(DataType.Research, 2))
                {
                    case 0:
                        return 60 * 3;
                    case 1:
                        return 60 * 5;
                    case 2:
                        return 60 * 10;
                }
                return double.MaxValue;
            case 4:
                switch (SaveManager.GetContentLevel(DataType.Research, 2))
                {
                    case 0:
                        return 60 * 10;
                    case 1:
                        return 60 * 20;
                    case 2:
                        return 60 * 30;
                    case 3:
                        return 60 * 40;
                    case 4:
                        return 60 * 90;
                }
                return double.MaxValue;
            case 5:
                switch (SaveManager.GetContentLevel(DataType.Research, 2))
                {
                    case 0:
                        return 60 * 15;
                    case 1:
                        return 60 * 30;
                    case 2:
                        return 60 * 45;
                    case 3:
                        return 60 * 70;
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                        return 60 * 120;
                }
                return double.MaxValue;
            default:
                return double.MaxValue;
        }
    }
}

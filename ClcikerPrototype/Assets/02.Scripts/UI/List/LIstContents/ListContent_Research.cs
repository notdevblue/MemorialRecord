using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MemorialRecord.Data;
using UnityEngine.UI;

public class ListContent_Research : ListContent
{
    [SerializeField] PanelResearchResource _panelResource = null;
    [SerializeField] Image fillImage = null;

    ResearchManager researchManager = null;

    private void Start()
    {
        researchManager = FindObjectOfType<ResearchManager>();        
    }

    protected override void Update()
    {
        if(_cachedData._idx == SaveManager.ResearchSaveData.curResearchIdx)
        {
            fillImage.fillAmount = 1 - (float)(SaveManager.ResearchSaveData.researchRemainTime / researchManager.GetUpgradeTime(_cachedData._idx));
        }
    }

    public override void InitContent<T>(T data)
    {
        int level = SaveManager.GetContentLevel(DataType.Research, data._idx);

        _textTitle.text = $"{data._name} Lv.{(level < 0 ? 0 : level)}";
        _textOutput.text = data._info;

        _cachedData = data;
        _btnLevelup.onClick.AddListener(() => LevelUp(_cachedData));

        _lockImage.gameObject.SetActive(!CanUpgrade(data._idx));
        _btnLevelup.gameObject.SetActive(CanUpgrade(data._idx));

        RefreshBtn(data, SaveManager.GetContentLevel(DataType.Research, data._idx));
    }

    public override void RefreshContent<T>(T data)
    {
        int level = SaveManager.GetContentLevel(DataType.Research, data._idx);
        _textTitle.text = $"{data._name} Lv.{(level < 0 ? 0 : level)}";
        _textLevelUpCost.text = DataManager.GetValueF(GetUpgradeValue(data._idx));
        _panelResource.SetValue(GetUpgradeResourceValue(data._idx));
        fillImage.fillAmount = 0;
    }

    protected override void LevelUp<T>(T data)
    {
        if (CanUpgrade(data._idx) && !researchManager._isResearching)   
        {
            double upgradeValue = GetUpgradeValue(data._idx);
            int needResearchResource = GetUpgradeResourceValue(data._idx);
            if (SaveManager.CurMemorial >= upgradeValue && SaveManager.ResearchSaveData.ResearchResources >= needResearchResource)
            {
                SaveManager.CurMemorial -= upgradeValue;
                SaveManager.ResearchSaveData.ResearchResources -= needResearchResource;
                FindObjectOfType<ResearchManager>().SetResearch(data._idx, () => 
                {
                    SaveManager.ContentLevelUp(data._idx, DataType.Research); 
                    RefreshContent(data);
                });
                RefreshContent(data);
                RefreshBtn(data, SaveManager.GetContentLevel(DataType.Research, data._idx));
            }
        }
    }

    protected override void RefreshBtn<T>(T data, int level)
    {
        if(!CanUpgrade(data._idx))
        {
            _lockImage.gameObject.SetActive(true);
            _btnLevelup.gameObject.SetActive(false);

            if(!isUnlocked(data._idx))
            {
                GetComponent<CanvasGroup>().alpha = 0;
            }
            _lockImage.GetComponentInChildren<Text>().text = "연구\n완료";
            _btnLevelup.onClick.RemoveAllListeners();
        }
        else
        {
            _panelResource.SetValue(GetUpgradeResourceValue(data._idx));
            _textLevelUpCost.text = DataManager.GetValueF(GetUpgradeValue(data._idx));
        }
    }

    private bool CanUpgrade(int idx)
    {
        bool result = false;
        switch (idx)
        {
            case 0:
                result = !(SaveManager.GetContentLevel(DataType.Research, 0) >= 1);
                break;
            case 1:
                result = !(SaveManager.GetContentLevel(DataType.Research, 1) >= 1);
                break;
            case 2:
                result = !(SaveManager.GetContentLevel(DataType.Research, 2) >= 3);
                result = result && isUnlocked(idx);
                break;
            case 3:
                result = !(SaveManager.GetContentLevel(DataType.Research, 3) >= 3);
                result = result && isUnlocked(idx);
                break;
            case 4:
                result = !(SaveManager.GetContentLevel(DataType.Research, 4) >= 6);
                result = result && isUnlocked(idx);
                break;
            case 5:
                result = !(SaveManager.GetContentLevel(DataType.Research, 5) >= 6);
                result = result && isUnlocked(idx);
                break;
            default:
                return false;
        }

        return result;
    }

    private bool isUnlocked(int idx)
    {
        bool result = false;
        switch (idx)
        {
            case 0:
                result = true;
                break;
            case 1:
                result = true;
                break;
            case 2:
                result = (SaveManager.GetContentLevel(DataType.Research, 0) >= 1 && SaveManager.GetContentLevel(DataType.Research, 1) >= 1);
                break;
            case 3:
                result = (SaveManager.GetContentLevel(DataType.Research, 0) >= 1 && SaveManager.GetContentLevel(DataType.Research, 1) >= 1);
                break;
            case 4:
                result = (SaveManager.GetContentLevel(DataType.Research, 2) >= 3 && SaveManager.GetContentLevel(DataType.Research, 3) >= 3);
                break;
            case 5:
                result = (SaveManager.GetContentLevel(DataType.Research, 2) >= 3 && SaveManager.GetContentLevel(DataType.Research, 3) >= 3);
                break;
            default:
                return false;
        }

        return result;
    }

    private int GetUpgradeResourceValue(int idx)
    {
        int value = int.MaxValue;
        switch (idx)
        {
            case 0:
                value = 1;
                break;
            case 1:
                value = 2;
                break;
            case 2:
                value = 1;
                break;
            case 3:
                value = 2;
                break;
            case 4:
                value = 2;
                break;
            case 5:
                value = 3;
                break;
        }
        return value;
    }

    private double GetUpgradeValue(int idx)
    {
        switch (idx)
        {
            case 0:
                return 1000000; // 1M
            case 1:
                return 5000000;
            case 2:
                switch (SaveManager.GetContentLevel(DataType.Research, 2))
                {
                    case 0:
                        return 5000000;
                    case 1:
                        return 10000000;
                    case 2:
                        return 20000000;
                }
                return double.MaxValue;
            case 3:
                switch (SaveManager.GetContentLevel(DataType.Research, 2))
                {
                    case 0:
                        return 5000000;
                    case 1:
                        return 10000000;
                    case 2:
                        return 20000000;
                }
                return double.MaxValue;
            case 4:
                switch (SaveManager.GetContentLevel(DataType.Research, 2))
                {
                    case 0:
                        return 100000000;
                    case 1:
                        return 200000000;
                    case 2:
                        return 500000000;
                    case 3:
                        return 1000000000;
                    case 4:
                        return 5000000000;
                }
                return double.MaxValue;
            case 5:
                switch (SaveManager.GetContentLevel(DataType.Research, 2))
                {
                    case 0:
                        return 200000000;
                    case 1:
                        return 400000000;
                    case 2:
                        return 1000000000;
                    case 3:
                        return 2000000000;
                    case 4:
                        return 10000000000;
                    case 5:
                        return 30000000000;
                    case 6:
                        return 70000000000;
                    case 7:
                        return 150000000000;
                    case 8:
                        return 300000000000;
                    case 9:
                        return 1000000000000;
                }
                return double.MaxValue;
            default:
                return double.MaxValue;
        }
    }

}

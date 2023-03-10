using Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryContent : MonoBehaviour
{
    [SerializeField] Sprite[] _imageSprites = null;
    [SerializeField] Image _imageIcon = null;
    [SerializeField] Text _textCount = null;
    [SerializeField] Button _btnUse = null;

    public ItemParent Item
    {
        get { return _item; }
        set { _item = value; }
    }
    ItemParent _item = null;

    private void Awake()
    {
        _btnUse.onClick.AddListener(OnUse);
    }

    private void OnEnable()
    {
        RefreshInventoryItem();
    }

    public void InventoryInit(ItemParent item)
    {
        if(item._type == ItemType.Memorial)
        {
            SaveManager.OnChangeMemorial += (value) => _textCount.text = DataManager.GetValueF(value).ToString();
            _textCount.text = DataManager.GetValueF(SaveManager.CurMemorial).ToString();
        }
        else if(item._type == ItemType.ResearchResource)
        {
            SaveManager.ResearchSaveData.OnChangeResearchResources += (value) => _textCount.text = value.ToString();
            _textCount.text = SaveManager.ResearchSaveData.ResearchResources.ToString();
        }
        else if (item._type == ItemType.Ink)
        {
            SaveManager.OnChangeInk += (value) => _textCount.text = value.ToString();
            _textCount.text = SaveManager.CurInk.ToString();
        }

        gameObject.SetActive(true);

        _item = item;

        _imageIcon.sprite = _imageSprites[(int)item._type];

        _item.onUse += OnUse;

        RefreshInventoryItem();
    }

    public void Use()
    {
        switch (_item._type)
        {
            case ItemType.Booster:
                (_item as Item_Booster).OnUse();
                break;
            case ItemType.NameChanger:
                (_item as Item_NameChanger).OnUse();
                break;
            case ItemType.ResearchPlus:
                (_item as Item_ResearchPlus).OnUse();
                break;
            case ItemType.GetAllMusic:
                (_item as Item_GetAllMusic).OnUse();
                break;
            case ItemType.RemoveAds:
                (_item as Item_RemoveAds).OnUse();
                break;
            case ItemType.MemorialPack:
                (_item as Item_MemorialPack).OnUse();
                break;
            case ItemType.Memorial:
            case ItemType.Ink:
            case ItemType.ResearchResource:
            default:
                break;
        }
    }

    private void OnUse()
    {
        ItemType type = _item._type;
        switch (type)
        {
            case ItemType.Booster:
            case ItemType.NameChanger:
            case ItemType.ResearchPlus:
            case ItemType.GetAllMusic:
            case ItemType.RemoveAds:
            case ItemType.MemorialPack:
                FindObjectOfType<PanelInventory>(true).SetUsePanel(Use, _imageSprites[(int)type], GetItemName(type), GetItemInfo(type));
                break;
            case ItemType.Memorial:
            case ItemType.Ink:
            case ItemType.ResearchResource:
                FindObjectOfType<PanelInventory>(true).SetOKPanel(_imageSprites[(int)type], GetItemName(type), GetItemInfo(type));
                break;
            default:
                break;
        }
        RefreshInventoryItem();
    }

    public void RefreshInventoryItem()
    {
        _textCount.text = _item._count.ToString();
        if (_item._type == ItemType.Memorial)
        {
            _textCount.text = DataManager.GetValueF(SaveManager.CurMemorial).ToString();
        }
        else if (_item._type == ItemType.ResearchResource)
        {
            _textCount.text = SaveManager.ResearchSaveData.ResearchResources.ToString();
        }
        else if (_item._type == ItemType.Ink)
        {
            _textCount.text = SaveManager.CurInk.ToString();
        }
    }

    private string GetItemName(ItemType type)
    {
        switch (type)
        {
            case ItemType.Booster:
                return "??????";
            case ItemType.NameChanger:
                return "???? ??????";
            case ItemType.ResearchPlus:
                return "???? ???? ?????? ??????";
            case ItemType.GetAllMusic:
                return "???? ???? ??????";
            case ItemType.RemoveAds:
                return "???? ??????";
            case ItemType.MemorialPack:
                return "???????? ????";
            case ItemType.Memorial:
                return "????????";
            case ItemType.Ink:
                return "????";
            case ItemType.ResearchResource:
                return "???? ????";
            default:
                return "";
        }
    }

    private string GetItemInfo(ItemType type)
    {
        switch (type)
        {
            case ItemType.Booster:
                return $"???? ?? {(int)_item._boosterTime / 60} ?? ??\n???????? ???????? ??????????.";
            case ItemType.NameChanger:
                return $"?????? ?????? ?? ????????.";
            case ItemType.ResearchPlus:
                return $"???? ???? ???????? 1 ??????????.";
            case ItemType.GetAllMusic:
                return $"???? ?????? ???? ??????????.";
            case ItemType.RemoveAds:
                return $"?????? ??????????.";
            case ItemType.MemorialPack:
                return $"???????? ????????????.\n{(int)_item._boosterTime / 60} ?? ?????? ???????? ???? ??????????.";
            case ItemType.Memorial:
                return $"???? ???? ?????? ??????????????.";
            case ItemType.Ink:
                return $"?????? ???? ?? ?? ???? ?????? ?????? ??????????.";
            case ItemType.ResearchResource:
                return $"?????? ???????? ??????????.";
            default:
                return $"";
        }
    }
}

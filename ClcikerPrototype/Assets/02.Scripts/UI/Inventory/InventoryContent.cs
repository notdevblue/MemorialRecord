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
    }

    private string GetItemName(ItemType type)
    {
        switch (type)
        {
            case ItemType.Booster:
                return "부스터";
            case ItemType.NameChanger:
                return "이름 변경권";
            case ItemType.ResearchPlus:
                return "연구 자원 최대치 증가권";
            case ItemType.GetAllMusic:
                return "음악 전체 해금권";
            case ItemType.RemoveAds:
                return "광고 제거권";
            case ItemType.MemorialPack:
                return "메모리얼 더미";
            case ItemType.Memorial:
                return "메모리얼";
            case ItemType.Ink:
                return "잉크";
            case ItemType.ResearchResource:
                return "연구 자원";
            default:
                return "";
        }
    }

    private string GetItemInfo(ItemType type)
    {
        switch (type)
        {
            case ItemType.Booster:
                return $"사용 시 {(int)_item._boosterTime / 60} 분 간\n메모리얼 획득량이 증가합니다.";
            case ItemType.NameChanger:
                return $"이름을 변경할 수 있습니다.";
            case ItemType.ResearchPlus:
                return $"연구 자원 최대치가 1 늘어납니다.";
            case ItemType.GetAllMusic:
                return $"음악 전체를 잠금 해제합니다.";
            case ItemType.RemoveAds:
                return $"광고를 제거합니다.";
            case ItemType.MemorialPack:
                return $"메모리얼 꾸러미입니다.\n{(int)_item._boosterTime / 60} 분 동안의 생산량을 즉시 획득합니다.";
            case ItemType.Memorial:
                return $"여러 곳에 쓰이는 메모리얼입니다.";
            case ItemType.Ink:
                return $"지우가 글을 쓸 수 있게 해주는 잉크와 깃펜입니다.";
            case ItemType.ResearchResource:
                return $"연구에 사용되는 자원입니다.";
            default:
                return $"";
        }
    }
}

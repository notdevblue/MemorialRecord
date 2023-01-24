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
                return "�ν���";
            case ItemType.NameChanger:
                return "�̸� �����";
            case ItemType.ResearchPlus:
                return "���� �ڿ� �ִ�ġ ������";
            case ItemType.GetAllMusic:
                return "���� ��ü �رݱ�";
            case ItemType.RemoveAds:
                return "���� ���ű�";
            case ItemType.MemorialPack:
                return "�޸𸮾� ����";
            case ItemType.Memorial:
                return "�޸𸮾�";
            case ItemType.Ink:
                return "��ũ";
            case ItemType.ResearchResource:
                return "���� �ڿ�";
            default:
                return "";
        }
    }

    private string GetItemInfo(ItemType type)
    {
        switch (type)
        {
            case ItemType.Booster:
                return $"��� �� {(int)_item._boosterTime / 60} �� ��\n�޸𸮾� ȹ�淮�� �����մϴ�.";
            case ItemType.NameChanger:
                return $"�̸��� ������ �� �ֽ��ϴ�.";
            case ItemType.ResearchPlus:
                return $"���� �ڿ� �ִ�ġ�� 1 �þ�ϴ�.";
            case ItemType.GetAllMusic:
                return $"���� ��ü�� ��� �����մϴ�.";
            case ItemType.RemoveAds:
                return $"���� �����մϴ�.";
            case ItemType.MemorialPack:
                return $"�޸𸮾� �ٷ����Դϴ�.\n{(int)_item._boosterTime / 60} �� ������ ���귮�� ��� ȹ���մϴ�.";
            case ItemType.Memorial:
                return $"���� ���� ���̴� �޸𸮾��Դϴ�.";
            case ItemType.Ink:
                return $"���찡 ���� �� �� �ְ� ���ִ� ��ũ�� �����Դϴ�.";
            case ItemType.ResearchResource:
                return $"������ ���Ǵ� �ڿ��Դϴ�.";
            default:
                return $"";
        }
    }
}

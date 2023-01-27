using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Purchasing;
using System;
using DG.Tweening;

public class PanelStore : MonoBehaviour
{
    public enum ShopItemCash
    {
        package_removeads,
        package_boostup,
        ink_10,
        ink_30,
        ink_50,
        ink_150,
        ink_300,
        ink_1000,
    }

    public enum ShopInkItem
    {
        Memorial,
        RemoveAds,
        ResearchPlus,
        ChangeName,
    }

    [SerializeField] Button btnClose;
    [SerializeField] Image imageFade;

    [SerializeField] Text[] textMemorials;

    InventoryManager invenManager;

    public void Start()
    {
        invenManager = FindObjectOfType<InventoryManager>(true);
        btnClose.onClick.AddListener(() =>
        {
            imageFade.gameObject.SetActive(true);
            imageFade.DOFade(1.0f, 1.0f).OnComplete(() =>
            {
                transform.parent.gameObject.SetActive(false);

                imageFade.DOFade(0.0f, 1.0f).OnComplete(() => imageFade.gameObject.SetActive(false));
            });
        });
    }

    public void OnEnable()
    {
        textMemorials[0].text = $"�޸𸮾� {DataManager.GetValueF((SaveManager.GetBookmarkValuePerSec() * 60 * 5))} ��";
        textMemorials[1].text = $"�޸𸮾� {DataManager.GetValueF((SaveManager.GetBookmarkValuePerSec() * 60 * 15))} ��";
        textMemorials[2].text = $"�޸𸮾� {DataManager.GetValueF((SaveManager.GetBookmarkValuePerSec() * 60 * 30))} ��";
        textMemorials[3].text = $"�޸𸮾� {DataManager.GetValueF((SaveManager.GetBookmarkValuePerSec() * 60 * 60))} ��";
        textMemorials[4].text = $"�޸𸮾� {DataManager.GetValueF((SaveManager.GetBookmarkValuePerSec() * 60 * 120))} ��";
        textMemorials[5].text = $"�޸𸮾� {DataManager.GetValueF((SaveManager.GetBookmarkValuePerSec() * 60 * 240))} ��";
    }

    public void OnBuyItemWithInk(int itemIdx)
    {
        Action onBuy = null;
        int price = 0;
        string itemInfo = "";

        switch ((ShopInkItem)itemIdx)
        {
            case ShopInkItem.Memorial:
                break;
            case ShopInkItem.RemoveAds:
                price = 35;
                onBuy = () => new Inventory.Item_RemoveAds(0, 1).OnUse();
                itemInfo = "���� ����\n��ǰ�� �����Ͻðڽ��ϱ�?";
                break;
            case ShopInkItem.ResearchPlus:
                price = 10;
                onBuy = () => new Inventory.Item_ResearchPlus(0, 1).OnUse();
                itemInfo = "���� �ڿ� �Ѱ� ����\n��ǰ�� �����Ͻðڽ��ϱ�?";
                break;
            case ShopInkItem.ChangeName:
                price = 50;
                onBuy = () => invenManager.SetItem(new Inventory.Item_NameChanger(0, 1));
                itemInfo = "�̸� �����\n��ǰ�� �����Ͻðڽ��ϱ�?";
                break;
            default:
                break;
        }

        FindObjectOfType<PanelGameNotice>(true).SetShopNoticePanel(onBuy, () => { }, "����", "���", "����", itemInfo, price);
    }

    public void OnBuyMemorial(int minutes)
    {
        int price = int.MaxValue;

        switch (minutes)
        {
            case 5:
                price = 5;
                break;
            case 15:
                price = 15;
                break;
            case 30:
                price = 30;
                break;
            case 60:
                price = 55;
                break;
            case 120:
                price = 100;
                break;
            case 240:
                price = 200;
                break;
            default:
                break;
        }

        FindObjectOfType<PanelGameNotice>(true).SetShopNoticePanel(() =>
        {
            new Inventory.Item_MemorialPack(0, 1, 60 * minutes).OnUse();
        }, 
        () => { },"����","���","����",$"�޸𸮾�\n{DataManager.GetValueF(60 * minutes * SaveManager.GetBookmarkValuePerSec())} ���� �����Ͻðڽ��ϱ�?", price);
    }

    public void OnBuyCash(ShopItemCash itemType)
    {
        switch (itemType)
        {
            case ShopItemCash.package_removeads:
                invenManager.SetItem(new Inventory.Item_RemoveAds(0, 1));
                invenManager.SetItem(new Inventory.Item_MemorialPack(0, 5, 60 * 5));
                invenManager.SetItem(new Inventory.Item_MemorialPack(0, 1, 60 * 15));
                invenManager.SetItem(new Inventory.Item_ResearchPlus(0, 1));
                SaveManager.CurInk += 30;
                break;
            case ShopItemCash.package_boostup:
                invenManager.SetItem(new Inventory.Item_Booster(0, 1, 60 * 60));
                invenManager.SetItem(new Inventory.Item_MemorialPack(0, 2, 60 * 30));
                invenManager.SetItem(new Inventory.Item_MemorialPack(0, 3, 60 * 10));
                invenManager.SetItem(new Inventory.Item_MemorialPack(0, 5, 60 * 5));
                break;
            case ShopItemCash.ink_10:
                SaveManager.CurInk += 10;
                break;
            case ShopItemCash.ink_30:
                SaveManager.CurInk += 30;
                break;
            case ShopItemCash.ink_50:
                SaveManager.CurInk += 50;
                break;
            case ShopItemCash.ink_150:
                SaveManager.CurInk += 150;
                break;
            case ShopItemCash.ink_300:
                SaveManager.CurInk += 300;
                break;
            case ShopItemCash.ink_1000:
                SaveManager.CurInk += 1000;
                break;
            default:
                break;
        }
    }

    public void OnBuyCashWithButton(int idx)
    {
        OnBuyCash((ShopItemCash)idx);
    }
}

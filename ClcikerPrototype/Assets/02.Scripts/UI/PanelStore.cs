using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Purchasing;

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

    [SerializeField] Button btnClose;

    [SerializeField] Text[] textMemorials;

    InventoryManager invenManager;

    public void Start()
    {
        invenManager = FindObjectOfType<InventoryManager>(true);
        btnClose.onClick.AddListener(() =>
        {
            FindObjectOfType<SlideEffector>().SlideInFrom(Direction.Bottom, 1f, () =>
            {
                transform.parent.gameObject.SetActive(false);

                FindObjectOfType<SlideEffector>().SlideOutTo(Direction.Bottom, 1f);
            });
        });
    }

    public void OnEnable()
    {
        textMemorials[0].text = $"메모리얼 {DataManager.GetValueF((SaveManager.GetBookmarkValuePerSec() * 60 * 5))} 개";
        textMemorials[1].text = $"메모리얼 {DataManager.GetValueF((SaveManager.GetBookmarkValuePerSec() * 60 * 15))} 개";
        textMemorials[2].text = $"메모리얼 {DataManager.GetValueF((SaveManager.GetBookmarkValuePerSec() * 60 * 30))} 개";
        textMemorials[3].text = $"메모리얼 {DataManager.GetValueF((SaveManager.GetBookmarkValuePerSec() * 60 * 60))} 개";
        textMemorials[4].text = $"메모리얼 {DataManager.GetValueF((SaveManager.GetBookmarkValuePerSec() * 60 * 120))} 개";
        textMemorials[5].text = $"메모리얼 {DataManager.GetValueF((SaveManager.GetBookmarkValuePerSec() * 60 * 240))} 개";
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

        if(SaveManager.CurQuillPen >= price)
        {
            SaveManager.CurQuillPen -= price;

            new Inventory.Item_MemorialPack(0, 1, 60 * minutes);
        }
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
                invenManager.SetItem(new Inventory.Item_Ink(0, 30));
                break;
            case ShopItemCash.package_boostup:
                invenManager.SetItem(new Inventory.Item_Booster(0, 1, 60 * 60));
                invenManager.SetItem(new Inventory.Item_MemorialPack(0, 2, 60 * 30));
                invenManager.SetItem(new Inventory.Item_MemorialPack(0, 3, 60 * 10));
                invenManager.SetItem(new Inventory.Item_MemorialPack(0, 5, 60 * 5));
                break;
            case ShopItemCash.ink_10: 
                invenManager.SetItem(new Inventory.Item_Ink(0, 10));
                break;
            case ShopItemCash.ink_30:
                invenManager.SetItem(new Inventory.Item_Ink(0, 30));
                break;
            case ShopItemCash.ink_50:
                invenManager.SetItem(new Inventory.Item_Ink(0, 50));
                break;
            case ShopItemCash.ink_150:
                invenManager.SetItem(new Inventory.Item_Ink(0, 150));
                break;
            case ShopItemCash.ink_300:
                invenManager.SetItem(new Inventory.Item_Ink(0, 300));
                break;
            case ShopItemCash.ink_1000:
                invenManager.SetItem(new Inventory.Item_Ink(0, 1000));
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

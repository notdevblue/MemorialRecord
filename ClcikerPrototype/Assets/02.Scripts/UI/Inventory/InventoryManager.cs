using Inventory;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{

    public enum ItemType
    {
        Booster,
        NameChanger,
        ResearchPlus,
        GetAllMusic,
        RemoveAds,
        MemorialPack,
        Memorial,
        Ink,
        ResearchResource
    }

    [System.Serializable]
    public class ItemParent
    {
        public bool _needDelete = false;
        public ItemType _type = ItemType.Ink;
        public int _idx;
        public int _maxCount;
        public int _count;
        public bool _canUse;
        public float _boosterTime = 0f; // 부스터 지속시간 ( 초 )
        public event Action onUse;

        public virtual void OnUse()
        {
            if (_needDelete)
                return;

            _count--;
            if (_count <= 0)
            {
                Delete();
            }

            onUse();
        }

        public virtual void Delete()
        {
            _count = 0;
            _needDelete = true;
        }
        
        public virtual void PlusItem()
        {
            if(_maxCount < _count)
            {
                _count++;
                _needDelete = false;
            }
        }

        public bool CanOverlap(ItemParent target)
        {
            if (target._type == _type)
            {
                if (_maxCount < _count)
                {
                    if (ItemType.Booster == _type)
                    {
                        if (target._boosterTime != _boosterTime)
                            return false;
                    }
                    else if(ItemType.MemorialPack == _type)
                    {
                        if (target._boosterTime != _boosterTime)
                            return false;
                    }

                    return true;
                }
            }
            return false;
        }

        public int itemMaxCount(ItemType type)
        {
            switch (type)
            {
                case ItemType.Booster:
                    return 99;
                case ItemType.NameChanger:
                    return 1;
                case ItemType.ResearchPlus:
                    return 99;
                case ItemType.GetAllMusic:
                    return 1;
                case ItemType.RemoveAds:
                    return 1;
                case ItemType.MemorialPack:
                    return 20;
                case ItemType.Memorial:
                    return 1;
                case ItemType.Ink:
                    return 1;
                case ItemType.ResearchResource:
                    return 1;
                default:
                    return 99;
            }
        }
    }

    [System.Serializable]
    public class Item_Booster : ItemParent
    {
        public Item_Booster(int idx, int count, float time)
        {
            _idx = idx;
            _type = ItemType.Booster;
            _maxCount = itemMaxCount(_type);
            _count = count;

            _boosterTime = time;

            _canUse = true;
        }

        public override void OnUse()
        {
            base.OnUse();
            SaveManager.RemainBoostTime += _boosterTime;
        }
    }

    [System.Serializable]
    public class Item_NameChanger : ItemParent
    {
        public Item_NameChanger(int idx, int count)
        {
            _idx = idx;
            _type = ItemType.NameChanger;
            _maxCount = itemMaxCount(_type);
            _count = count;

            _canUse = true;
        }

        public override void OnUse()
        {
            base.OnUse();
            GameObject.FindObjectOfType<PanelName>(true).SetNamePanelInMain();
        }
    }

    [System.Serializable]
    public class Item_ResearchPlus : ItemParent
    {
        public Item_ResearchPlus(int idx, int count)
        {
            _idx = idx;
            _type = ItemType.ResearchPlus;
            _maxCount = itemMaxCount(_type);
            _count = count;

            _canUse = true;
        }

        public override void OnUse()
        {
            base.OnUse();
            SaveManager.ResearchSaveData.researchResourceMaxCount++;
        }
    }

    [System.Serializable]
    public class Item_GetAllMusic : ItemParent
    {
        public Item_GetAllMusic(int idx, int count)
        {
            _idx = idx;
            _type = ItemType.GetAllMusic;
            _maxCount = itemMaxCount(_type);
            _count = count;

            _canUse = true;
        }

        public override void OnUse()
        {
            base.OnUse();
            for (int i = 0; i < SaveManager.MusicBoughtArr.Length; i++)
            {
                SaveManager.MusicBoughtArr[i] = true;
            }
        }
    }

    public class Item_RemoveAds : ItemParent
    {
        public Item_RemoveAds(int idx, int count) 
        { 
            _idx = idx;
            _type = ItemType.RemoveAds;
            _maxCount = itemMaxCount(_type);
            _count = count; 

            _canUse = true; 
        }

        public override void OnUse()
        {
            base.OnUse();
            for (int i = 0; i < SaveManager.MusicBoughtArr.Length; i++)
            {
                SaveManager.MusicBoughtArr[i] = true;
            }
        }
    }
    public class Item_MemorialPack : ItemParent
    {
        public Item_MemorialPack(int idx, int count, float time)
        {
            _idx = idx;
            _type = ItemType.MemorialPack;
            _maxCount = itemMaxCount(_type);
            _count = count;

            _boosterTime = time;

            _canUse = true;
        }

        public override void OnUse()
        {
            base.OnUse();
            SaveManager.CurMemorial += _boosterTime * SaveManager.GetBookmarkValuePerSec();
        }
    }

    public class Item_Memorial : ItemParent
    {
        public Item_Memorial(int idx, int count)
        {
            _idx = idx;
            _type = ItemType.Memorial;
            _maxCount = itemMaxCount(_type);
            _count = count;

            _canUse = false;
        }
    }

    public class Item_Ink : ItemParent
    {
        public Item_Ink(int idx, int count)
        {
            _idx = idx;
            _type = ItemType.Ink;
            _maxCount = itemMaxCount(_type);
            _count = count;

            _canUse = false;
        }
    }

    public class Item_ResearchResource : ItemParent
    {
        public Item_ResearchResource(int idx, int count)
        {
            _idx = idx;
            _type = ItemType.ResearchResource;
            _maxCount = itemMaxCount(_type);
            _count = count;

            _canUse = false;
        }
    }
}

public class InventoryManager : MonoBehaviour
{
    [SerializeField] Transform _contentParent;
    [SerializeField] InventoryContent _contentPrefab;

    List<InventoryContent> _childs = new List<InventoryContent>();

    private void Start()
    {
        InitList();
    }

    public void InitList()
    {
        foreach (var item in SaveManager.InventoryItems)
        {
            InventoryContent content = Instantiate(_contentPrefab, _contentParent);
            _childs.Add(content);
            content.InventoryInit(item as ItemParent);
        }
        Refresh();
    }

    public void Refresh()
    {
        _childs.FindAll(x => x.Item._needDelete).ForEach(x => Destroy(x.gameObject));
        _childs.RemoveAll(x => x.Item._needDelete);

        foreach (var item in _childs)
        {
            item.RefreshInventoryItem();
        }

        _childs.Sort((x, y) => 
        {
            if (x.Item._type > y.Item._type)
            {
                return -1;
            }
            else if (x.Item._type == y.Item._type)
            {
                if (x.Item._count > y.Item._maxCount)
                {
                    return -1;
                }
                else if(x.Item._count == y.Item._maxCount)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                return 0;
            }
        });

        _childs.RemoveAll(x => x.gameObject == null);
    }

    public void SetItem(ItemParent item)
    {
        InventoryContent content = _childs.Find((x) => x.Item.CanOverlap(item));
        if(!content)
        {
            content = Instantiate(_contentPrefab, _contentParent);
            _childs.Add(content);
            content.InventoryInit(item);
        }
        else
        {
            content.Item._count++;
        }
    }
}


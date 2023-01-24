using Inventory;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemorialRecord.Data
{
    [Serializable]
    public class ResearchSaveData
    {
        public event Action<int> OnChangeResearchResources;

        public int ResearchResources
        {
            get
            {
                return researchResources;
            }

            set
            {
                researchResources = value;
                OnChangeResearchResources?.Invoke(researchResources);
            }
        }

        [SerializeField] int researchResources = 3;
        public int researchResourceMaxCount = 3;
        public int researchResourceRemainTime = 0;

        public double researchRemainTime = 0.0;
        public int curResearchIdx = -1;
    }

    [Serializable]
    public class PlayerProfile
    {
        public bool isNewUser = true;
        public bool isOnAllMusic = false;
        public bool isRemoveAds = false;
        public bool isPushAlarmOn = false;
        public bool isStoryAutoPlayOnUnlocked = false;

        public string characterName = "$InitalizedPlayerName$";

        [Header("MusicData")]
        public int curMusicIndex = 0;
        public bool[] MusicBoughtArr = new bool[] { true, false, false, false, false, false, false, false, false };

        [Header("Settings")]
        public float musicVolume = 0.3f;
        public float soundEffectVolume = 0.3f;

        public uint idxCurStory = 0;
    }

    [Serializable]
    public class SaveData : ISerializationCallbackReceiver
    {

        public string saveTimeString = DateTime.Now.ToString();

        public float remainBoostTime = 0f;

        public List<ItemParent> inventoryItems = new List<ItemParent>();

        public ResearchSaveData researchSaveData = new ResearchSaveData();

        [Header("ResourceManage")]
        public double currentMemorial;
        public long currentQuillPen;

        public Dictionary<int, bool> storyReadDict = new Dictionary<int, bool>() { {0, false} };
        // -1 잠김, 0 구매 가능, 1 구매함
        public Dictionary<int, int> bookLevelDict = new Dictionary<int, int>() { {0, 1} };
        public Dictionary<int, int> bookMarkLevelDict = new Dictionary<int, int>() { { 0, 0 } };
        public Dictionary<int, int> accessoryLevelDict = new Dictionary<int, int>() { { 0, 0 } };
        public Dictionary<int, int> roomLevelDict = new Dictionary<int, int>() { { 0, 1 } };
        public Dictionary<int, int> researchLevelDict = new Dictionary<int, int>() { };

        public int exapndLevel;

        #region metadata
        // 직렬화 / 역직렬화 과정에서 Dictonary Data를 보존하기 위해 만든 메타데이터입니다.

        [SerializeField] private ItemParent[] itemsMeta; 
        [SerializeField] private int[] researchRemainTimeKeys;
        [SerializeField] private int[] storyReadDictKeys;
        [SerializeField] private int[] bookLevelDictKeys;
        [SerializeField] private int[] bookMarkLevelDictKeys;
        [SerializeField] private int[] accessoryLevelDictKeys;
        [SerializeField] private int[] roomInfoLevelsDictKeys;
        [SerializeField] private int[] researchInfoLevelsDictKeys;

        [SerializeField] private double[] researchRemainTimeValues;
        [SerializeField] private bool[] storyReadDictValues;
        [SerializeField] private int[] bookLevelDictValues;
        [SerializeField] private int[] bookMarkLevelDictValues;
        [SerializeField] private int[] accessoryLevelDictValues;
        [SerializeField] private int[] roomInfoLevelsDictValues;
        [SerializeField] private int[] researchInfoLevelsDictValues;

        #endregion

        public void OnBeforeSerialize()
        {
            itemsMeta = new ItemParent[inventoryItems.Count];
            storyReadDictKeys = new int[storyReadDict.Keys.Count];
            bookLevelDictKeys = new int[bookLevelDict.Keys.Count];
            bookMarkLevelDictKeys = new int[bookMarkLevelDict.Keys.Count];
            accessoryLevelDictKeys = new int[accessoryLevelDict.Keys.Count];
            roomInfoLevelsDictKeys = new int[roomLevelDict.Keys.Count];
            researchInfoLevelsDictKeys = new int[researchLevelDict.Keys.Count];

            storyReadDictValues = new bool[storyReadDict.Values.Count];
            bookLevelDictValues = new int[bookLevelDict.Values.Count];
            bookMarkLevelDictValues = new int[bookMarkLevelDict.Values.Count];
            accessoryLevelDictValues = new int[accessoryLevelDict.Values.Count];
            roomInfoLevelsDictValues = new int[roomLevelDict.Values.Count];
            researchInfoLevelsDictValues = new int[researchLevelDict.Values.Count];

            storyReadDict.Keys.CopyTo(storyReadDictKeys, 0);
            bookLevelDict.Keys.CopyTo(bookLevelDictKeys, 0);
            bookMarkLevelDict.Keys.CopyTo(bookMarkLevelDictKeys, 0);
            accessoryLevelDict.Keys.CopyTo(accessoryLevelDictKeys, 0);
            roomLevelDict.Keys.CopyTo(roomInfoLevelsDictKeys, 0);
            researchLevelDict.Keys.CopyTo(researchInfoLevelsDictKeys, 0);

            storyReadDict.Values.CopyTo(storyReadDictValues, 0);
            bookLevelDict.Values.CopyTo(bookLevelDictValues, 0);
            bookMarkLevelDict.Values.CopyTo(bookMarkLevelDictValues, 0);
            accessoryLevelDict.Values.CopyTo(accessoryLevelDictValues, 0);
            roomLevelDict.Values.CopyTo(roomInfoLevelsDictValues, 0);
            researchLevelDict.Values.CopyTo(researchInfoLevelsDictValues, 0);
            inventoryItems.CopyTo(itemsMeta);
        }

        public void OnAfterDeserialize()
        {
            ArrayToDict(storyReadDict, storyReadDictKeys, storyReadDictValues);
            ArrayToDict(bookLevelDict, bookLevelDictKeys, bookLevelDictValues);
            ArrayToDict(bookMarkLevelDict, bookMarkLevelDictKeys, bookMarkLevelDictValues);
            ArrayToDict(accessoryLevelDict, accessoryLevelDictKeys, accessoryLevelDictValues);
            ArrayToDict(roomLevelDict, roomInfoLevelsDictKeys, roomInfoLevelsDictValues);
            ArrayToDict(researchLevelDict, researchInfoLevelsDictKeys, researchInfoLevelsDictValues);

            inventoryItems = new List<ItemParent>();
            ItemParent item = null;
            for (int i = 0; i < itemsMeta.Length; i++)
            {
                item = itemsMeta[i];
                if (item._count <= 0)
                    continue;

                switch (item._type)
                {
                    case ItemType.Booster:
                        inventoryItems.Add(new Item_Booster(0, item._count, item._boosterTime));
                        break;
                    case ItemType.NameChanger:
                        inventoryItems.Add(new Item_NameChanger(0, item._count));
                        break;
                    case ItemType.ResearchPlus:
                        inventoryItems.Add(new Item_ResearchPlus(0, item._count));
                        break;
                    case ItemType.GetAllMusic:
                        inventoryItems.Add(new Item_GetAllMusic(0, item._count));
                        break;
                    case ItemType.RemoveAds:
                        inventoryItems.Add(new Item_RemoveAds(0, item._count));
                        break;
                    case ItemType.MemorialPack:
                        inventoryItems.Add(new Item_MemorialPack(0, item._count, item._boosterTime));
                        break;
                    case ItemType.Memorial:
                        inventoryItems.Add(new Item_Memorial(0, item._count));
                        break;
                    case ItemType.Ink:
                        inventoryItems.Add(new Item_Ink(0, item._count));
                        break;
                    case ItemType.ResearchResource:
                        inventoryItems.Add(new Item_ResearchResource(0, item._count));
                        break;
                    default:
                        break;
                }
            }
        }

        private void ArrayToDict<TKey, TValue>(Dictionary<TKey, TValue> dst, TKey[] keySrc, TValue[] valueSrc)
        {
            for (int i = 0; i < keySrc.Length; i++)
            {
                if (!dst.ContainsKey(keySrc[i]))
                {
                    dst.Add(keySrc[i], valueSrc[i]);
                }
                else
                {
                    dst[keySrc[i]] = valueSrc[i];
                }
            }
        }
    }
}

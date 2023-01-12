using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemorialRecord.Data
{

    public enum AttendanceState { none, success, fail }

    [System.Serializable]
    public class SaveData : ISerializationCallbackReceiver
    {
        public string saveTimeString;
        public string characterName;

        #region AttendanceCheck

        public string firstStartDateString;
        public AttendanceState[] attendanceStateArray = new AttendanceState[12];

        #endregion

        #region PurchaseData

        [Header("PurchaseDatas")]
        public bool booster1;
        public bool booster2;
        public bool booster3;
        public bool booster4;
        public bool booster5;

        #endregion

        #region Settings

        [Header("Settings")]
        public float musicVolume = 0.3f;
        public float soundEffectVolume = 0.3f;
        public bool isPushAlarmOn = false;
        public bool isStoryAutoPlayOnUnlocked = false;

        #endregion

        #region MusicData

        [Header("MusicData")]
        public int curMusicIndex = 0;
        public bool[] MusicBoughtArr = new bool[] {true, false, false, false, false, false, false};

        #endregion

        #region ResourceManage

        [Header("ResourceManage")]
        public double currentMemorial;
        public long currentQuillPen;

        public uint idxCurStory = 0;

        public Dictionary<int, bool> storyReadDict = new Dictionary<int, bool>() { {0, false} };

        // -1 잠김, 0 구매 가능, 1 구매함
        public Dictionary<int, int> bookLevelDict = new Dictionary<int, int>() { {0, 1} };
        public Dictionary<int, int> bookMarkLevelDict = new Dictionary<int, int>() { { 0, 0 } };
        public Dictionary<int, int> accessoryLevelDict = new Dictionary<int, int>() { { 0, 0 } };
        public Dictionary<int, int> roomInfoLevelsDict = new Dictionary<int, int>() { { 0, 1 } };

        public int exapndLevel;

        [Header("research resource")]
        public int magnifierResource;
        public int magnifierResourceProduceCount;

        public int[] researchLevels;
        public int[] researchProgresses;
        #endregion

        #region metadata
        // 직렬화 / 역직렬화 과정에서 Dictonary Data를 보존하기 위해 만든 메타데이터입니다.

        [SerializeField] private int[] storyReadDictKeys;
        [SerializeField] private int[] bookLevelDictKeys;
        [SerializeField] private int[] bookMarkLevelDictKeys;
        [SerializeField] private int[] accessoryLevelDictKeys;
        [SerializeField] private int[] roomInfoLevelsDictKeys;

        [SerializeField] private bool[] storyReadDictValues;
        [SerializeField] private int[] bookLevelDictValues;
        [SerializeField] private int[] bookMarkLevelDictValues;
        [SerializeField] private int[] accessoryLevelDictValues;
        [SerializeField] private int[] roomInfoLevelsDictValues;

        #endregion

        public void OnBeforeSerialize()
        {
            storyReadDictKeys = new int[storyReadDict.Keys.Count];
            bookLevelDictKeys = new int[bookLevelDict.Keys.Count];
            bookMarkLevelDictKeys = new int[bookMarkLevelDict.Keys.Count];
            accessoryLevelDictKeys = new int[accessoryLevelDict.Keys.Count];
            roomInfoLevelsDictKeys = new int[roomInfoLevelsDict.Count];

            storyReadDictValues = new bool[storyReadDict.Values.Count];
            bookLevelDictValues = new int[bookLevelDict.Values.Count];
            bookMarkLevelDictValues = new int[bookMarkLevelDict.Values.Count];
            accessoryLevelDictValues = new int[accessoryLevelDict.Values.Count];
            roomInfoLevelsDictValues = new int[roomInfoLevelsDict.Values.Count];

            storyReadDict.Keys.CopyTo(storyReadDictKeys, 0);
            bookLevelDict.Keys.CopyTo(bookLevelDictKeys, 0);
            bookMarkLevelDict.Keys.CopyTo(bookMarkLevelDictKeys, 0);
            accessoryLevelDict.Keys.CopyTo(accessoryLevelDictKeys, 0);
            roomInfoLevelsDict.Keys.CopyTo(roomInfoLevelsDictKeys, 0);

            storyReadDict.Values.CopyTo(storyReadDictValues, 0);
            bookLevelDict.Values.CopyTo(bookLevelDictValues, 0);
            bookMarkLevelDict.Values.CopyTo(bookMarkLevelDictValues, 0);
            accessoryLevelDict.Values.CopyTo(accessoryLevelDictValues, 0);
            roomInfoLevelsDict.Values.CopyTo(roomInfoLevelsDictValues, 0);
        }

        public void OnAfterDeserialize()
        {
            ArrayToDict(storyReadDict, storyReadDictKeys, storyReadDictValues);
            ArrayToDict(bookLevelDict, bookLevelDictKeys, bookLevelDictValues);
            ArrayToDict(bookMarkLevelDict, bookMarkLevelDictKeys, bookMarkLevelDictValues);
            ArrayToDict(accessoryLevelDict, accessoryLevelDictKeys, accessoryLevelDictValues);
            ArrayToDict(roomInfoLevelsDict, roomInfoLevelsDictKeys, roomInfoLevelsDictValues);
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

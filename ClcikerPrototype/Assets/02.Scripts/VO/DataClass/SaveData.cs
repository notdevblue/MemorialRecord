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
        public float musicVolume;
        public float soundEffectVolume;
        public bool isPushAlarmOn;
        public bool isStoryAutoPlayOnUnlocked;

        #endregion

        #region MusicData

        [Header("MusicData")]
        public int curMusicIndex;
        public bool[] MusicBoughtArr;

        #endregion

        #region ResourceManage

        [Header("ResourceManage")]
        public double currentMemorial;
        public long currentQuillPen;

        // -1 잠김, 0 구매 가능, 1 구매함
        public Dictionary<int, int> bookLevelDict = new Dictionary<int, int>();
        public Dictionary<int, int> bookMarkLevelDict = new Dictionary<int, int>();
        public Dictionary<int, int> accessoryLevelDict = new Dictionary<int, int>();
        public Dictionary<int, int> roomInfoLevelsDict = new Dictionary<int, int>();

        public int exapndLevel;

        [Header("research resource")]
        public int magnifierResource;
        public int magnifierResourceProduceCount;

        public int[] researchLevels;
        public int[] researchProgresses;
        #endregion

        #region metadata
        // 직렬화 / 역직렬화 과정에서 Dictonary Data를 보존하기 위해 만든 메타데이터입니다.

        private int[] bookLevelDictKeys;
        private int[] bookMarkLevelDictKeys;
        private int[] accessoryLevelDictKeys;
        private int[] roomInfoLevelsDictKeys;

        private int[] bookLeveleDictValue;
        private int[] bookMarkLevelDictValue;
        private int[] accessoryLevelDictValue;
        private int[] roomInfoLevelsDictValue;

        #endregion

        public void OnBeforeSerialize()
        {
            bookLevelDict.Keys.CopyTo(bookLevelDictKeys, 0);
            bookMarkLevelDict.Keys.CopyTo(bookMarkLevelDictKeys, 0);
            accessoryLevelDict.Keys.CopyTo(accessoryLevelDictKeys, 0);
            roomInfoLevelsDict.Keys.CopyTo(roomInfoLevelsDictKeys, 0);

            bookLevelDict.Values.CopyTo(bookLeveleDictValue, 0);
            bookMarkLevelDict.Values.CopyTo(bookMarkLevelDictValue, 0);
            accessoryLevelDict.Values.CopyTo(accessoryLevelDictValue, 0);
            roomInfoLevelsDict.Values.CopyTo(roomInfoLevelsDictValue, 0);
        }

        public void OnAfterDeserialize()
        {
            ArrayToDict(bookLevelDict, bookLevelDictKeys, bookLeveleDictValue);
            ArrayToDict(bookMarkLevelDict, bookMarkLevelDictKeys, bookMarkLevelDictValue);
            ArrayToDict(accessoryLevelDict, accessoryLevelDictKeys, accessoryLevelDictValue);
            ArrayToDict(roomInfoLevelsDict, roomInfoLevelsDictKeys, roomInfoLevelsDictValue);
        }

        private void ArrayToDict<TKey, TValue>(Dictionary<TKey, TValue> dst, TKey[] keySrc, TValue[] valueSrc)
        {
            for (int i = 0; i < keySrc.Length; i++)
            {
                if (dst.ContainsKey(keySrc[i]))
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

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
        public bool[] MusicBoughtArr = new bool[] {true, false, false, false, false, false, false, false, false};

        #endregion

        #region ResourceManage

        [Header("ResourceManage")]
        public double currentMemorial;
        public long currentQuillPen;

        public uint idxCurStory = 0;

        public Dictionary<string, float> researchMultiplyDict = new Dictionary<string, float>() { { "PerSec", 1 }, { "Touch", 1 }, { "All", 1 } };

        public Dictionary<int, bool> storyReadDict = new Dictionary<int, bool>() { {0, false} };

        // -1 ���, 0 ���� ����, 1 ������
        public Dictionary<int, int> bookLevelDict = new Dictionary<int, int>() { {0, 1} };
        public Dictionary<int, int> bookMarkLevelDict = new Dictionary<int, int>() { { 0, 0 } };
        public Dictionary<int, int> accessoryLevelDict = new Dictionary<int, int>() { { 0, 0 } };
        public Dictionary<int, int> roomLevelDict = new Dictionary<int, int>() { { 0, 1 } };
        public Dictionary<int, int> researchLevelDict = new Dictionary<int, int>() { };
        public Dictionary<int, uint> researchRemainTimeDict = new Dictionary<int, uint>() { };

        public int exapndLevel;

        [Header("research resource")]
        public int magnifierResource;
        public int magnifierResourceProduceCount;

        public int[] researchLevels;
        public int[] researchProgresses;
        #endregion

        #region metadata
        // ����ȭ / ������ȭ �������� Dictonary Data�� �����ϱ� ���� ���� ��Ÿ�������Դϴ�.

        [SerializeField] private string[] researchMultiplyDictKeys = new string[] { "PerSec", "Touch", "All" };
        [SerializeField] private int[] researchRemainTimeKeys;
        [SerializeField] private int[] storyReadDictKeys;
        [SerializeField] private int[] bookLevelDictKeys;
        [SerializeField] private int[] bookMarkLevelDictKeys;
        [SerializeField] private int[] accessoryLevelDictKeys;
        [SerializeField] private int[] roomInfoLevelsDictKeys;
        [SerializeField] private int[] researchInfoLevelsDictKeys;

        [SerializeField] private float[] researchMultiplyDictValues;
        [SerializeField] private uint[] researchRemainTimeValues;
        [SerializeField] private bool[] storyReadDictValues;
        [SerializeField] private int[] bookLevelDictValues;
        [SerializeField] private int[] bookMarkLevelDictValues;
        [SerializeField] private int[] accessoryLevelDictValues;
        [SerializeField] private int[] roomInfoLevelsDictValues;
        [SerializeField] private int[] researchInfoLevelsDictValues;

        #endregion

        public void OnBeforeSerialize()
        {
            storyReadDictKeys = new int[storyReadDict.Keys.Count];
            researchRemainTimeKeys = new int[researchRemainTimeDict.Keys.Count];
            bookLevelDictKeys = new int[bookLevelDict.Keys.Count];
            bookMarkLevelDictKeys = new int[bookMarkLevelDict.Keys.Count];
            accessoryLevelDictKeys = new int[accessoryLevelDict.Keys.Count];
            roomInfoLevelsDictKeys = new int[roomLevelDict.Keys.Count];
            researchInfoLevelsDictKeys = new int[researchLevelDict.Keys.Count];

            researchMultiplyDictValues = new float[researchMultiplyDict.Values.Count];
            researchRemainTimeValues = new uint[researchRemainTimeDict.Values.Count];
            storyReadDictValues = new bool[storyReadDict.Values.Count];
            bookLevelDictValues = new int[bookLevelDict.Values.Count];
            bookMarkLevelDictValues = new int[bookMarkLevelDict.Values.Count];
            accessoryLevelDictValues = new int[accessoryLevelDict.Values.Count];
            roomInfoLevelsDictValues = new int[roomLevelDict.Values.Count];
            researchInfoLevelsDictValues = new int[researchLevelDict.Values.Count];

            storyReadDict.Keys.CopyTo(storyReadDictKeys, 0);
            researchRemainTimeDict.Keys.CopyTo(researchRemainTimeKeys, 0);
            bookLevelDict.Keys.CopyTo(bookLevelDictKeys, 0);
            bookMarkLevelDict.Keys.CopyTo(bookMarkLevelDictKeys, 0);
            accessoryLevelDict.Keys.CopyTo(accessoryLevelDictKeys, 0);
            roomLevelDict.Keys.CopyTo(roomInfoLevelsDictKeys, 0);
            researchLevelDict.Keys.CopyTo(researchInfoLevelsDictKeys, 0);

            researchMultiplyDict.Values.CopyTo(researchMultiplyDictValues, 0);
            researchRemainTimeDict.Values.CopyTo(researchRemainTimeValues, 0);
            storyReadDict.Values.CopyTo(storyReadDictValues, 0);
            bookLevelDict.Values.CopyTo(bookLevelDictValues, 0);
            bookMarkLevelDict.Values.CopyTo(bookMarkLevelDictValues, 0);
            accessoryLevelDict.Values.CopyTo(accessoryLevelDictValues, 0);
            roomLevelDict.Values.CopyTo(roomInfoLevelsDictValues, 0);
            researchLevelDict.Values.CopyTo(researchInfoLevelsDictValues, 0);
        }

        public void OnAfterDeserialize()
        {
            ArrayToDict(researchMultiplyDict, researchMultiplyDictKeys, researchMultiplyDictValues);
            ArrayToDict(storyReadDict, storyReadDictKeys, storyReadDictValues);
            ArrayToDict(bookLevelDict, bookLevelDictKeys, bookLevelDictValues);
            ArrayToDict(bookMarkLevelDict, bookMarkLevelDictKeys, bookMarkLevelDictValues);
            ArrayToDict(accessoryLevelDict, accessoryLevelDictKeys, accessoryLevelDictValues);
            ArrayToDict(roomLevelDict, roomInfoLevelsDictKeys, roomInfoLevelsDictValues);
            ArrayToDict(researchLevelDict, researchInfoLevelsDictKeys, researchInfoLevelsDictValues);
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

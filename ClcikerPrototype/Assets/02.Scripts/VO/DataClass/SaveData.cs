using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemorialRecord.Data
{
    public enum AttendanceState { none, success, fail }

    [System.Serializable]
    public class SaveData
    {
        public SaveData()
        {

        }

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

        public int[] bookLevels;
        public int[] bookMarkLevels;
        public int[] accessoryActivatedArr; // -1 잠김, 0 구매 가능, 1 구매함
        public int[] roomActivatedArr; // -1 잠김, 0 구매 가능, 1 구매함

        public int exapndLevel;


        [Header("research resource")]
        public int magnifierResource;
        public int magnifierResourceProduceCount;

        public int[] researchLevels;
        public int[] researchProgresses;

        #endregion
    }
}

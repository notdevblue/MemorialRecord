using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemorialRecord.Data
{
    public enum AttendanceState { none, success, fail }

    [System.Serializable]
    public class SaveData
    {
        public string SaveTimeString;
        public string characterName;

        #region AttendanceCheck

        public string FirstStartDateString;
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
        public float MusicVolume;
        public float SoundEffectVolume;
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
        public double CurrentMemorial;
        public long CurrentQuillPen;
        public int[] BookLevels;
        public int[] BookMarkLevels;
        public bool[] AccessoryActivatedArr;
        public int ExapndLevel;
        public bool[] SkinActivatedArr;

        [Header("research resource")]
        public int MagnifierResource;
        public int MagnifierResourceProduceCount;
        public int[] ResearchLevels;
        public int[] ResearchProgresses;

        #endregion
    }
}

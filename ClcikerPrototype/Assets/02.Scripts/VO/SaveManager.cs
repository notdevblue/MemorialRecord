using MemorialRecord.Data;
using System;
using System.Linq;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;
using System.Collections.Generic;
using Inventory;

public enum DataType
{
    Book,
    BookMark,
    Accessory,
    Room,
    Research,
}


public static class SaveManager
{
    public static event Action<int, DataType, int> OnUpdateLevel; // idx, Type, level
    public static event Action<double> OnChangeMemorial;
    public static event Action<long> OnChangeQuillPen;
    public static event Action<float> OnChangeMusicVolume;
    public static event Action<float> OnChangeSoundEffectVolume;
    public static event Action OnSetBooster;

    #region �ܺ� ���� ������ Getter, Setter
    public static double CurMemorial
    {
        get
        {
            return _savedata.currentMemorial;
        }
        set
        {
            _savedata.currentMemorial = value;
            OnChangeMemorial?.Invoke(value);
            SaveData();
        }
    }

    public static long CurQuillPen
    {
        get
        {
            return _savedata.currentQuillPen;
        }
        set
        {
            _savedata.currentQuillPen = value;
            OnChangeQuillPen?.Invoke(value);
            SaveData();
        }
    }

    public static float MusicVolume
    {
        get
        {
            return _profileData.musicVolume;
        }
        set
        {
            _profileData.musicVolume = value;
            OnChangeMusicVolume?.Invoke(value);
            SaveData();
        }
    }

    public static float SoundEffectVolume
    {
        get
        {
            return _profileData.soundEffectVolume;
        }
        set
        {
            _profileData.soundEffectVolume = value;
            OnChangeSoundEffectVolume?.Invoke(value);
            SaveData();
        }
    }

    public static string CharName
    {
        get
        {
            return _profileData.characterName;
        }
        set
        {
            _profileData.characterName = value;
            SaveData();
        }
    }

    public static int IdxCurMusic
    {
        get
        {
            return _profileData.curMusicIndex;
        }
        set
        {
            _profileData.curMusicIndex = value;
            GameObject.FindObjectOfType<BGMSelector>()?.SetBGM(value);
            SaveData();
        }
    }

    public static uint IdxCurStory
    {
        get
        {
            return _savedata.idxCurStory;
        }

        set
        {
            _savedata.idxCurStory = value;
            SaveData();
        }
    }

    public static bool[] MusicBoughtArr
    {
        get
        {
            return _profileData.MusicBoughtArr;
        }

        set
        {
            _profileData.MusicBoughtArr = value;
            SaveData();
        }
    }

    public static ResearchSaveData ResearchSaveData
    {
        get
        {
            return _savedata.researchSaveData;
        }

        set
        {
            _savedata.researchSaveData = value;
        }
    }

    public static float RemainBoostTime
    {
        get { return _savedata.remainBoostTime; }
        set
        {
            if(RemainBoostTime == 0f)
            {
                OnSetBooster?.Invoke();
            }

            _savedata.remainBoostTime = value;
        }
    }

    public static bool IsOnAllMusic
    {
        get { return _profileData.isOnAllMusic; }
        set { _profileData.isOnAllMusic = value; SaveData(); }
    }

    public static bool IsRemoveAds
    {
        get { return _profileData.isRemoveAds; }
        set { _profileData.isRemoveAds = value; SaveData(); }
    }

    public static ItemParent[] InventoryItems
    {
        get { return _savedata.inventoryItems; }
        set { _savedata.inventoryItems = value; SaveData(); }
    }
    #endregion

    #region readonly private fields

    public static readonly string _saveDir = Application.persistentDataPath;
    public static readonly string _profileSavePath = _saveDir + "/ProfileData.sav";
    public static readonly string _dataSavePath = _saveDir + "/SaveData.sav";
    public static readonly string _defaultSavePath = "Assets/Resources/DefaultSaveData.txt";
    private static readonly string _privateKey = "z$C&F)J@NcRfUjXn";//16byte �̻��� ���ڿ�
    #endregion


    private static SaveData _savedata = new MemorialRecord.Data.SaveData();
    private static PlayerProfile _profileData = new MemorialRecord.Data.PlayerProfile();
    private static Dictionary<string, float> _researchMultiplyDict = new Dictionary<string, float>() { { "PerSec", 1 }, { "Touch", 1 }, { "All", 1 } };

    public static Dictionary<string, float> ResearchMultiplyDict
    {
        get
        {
            return _researchMultiplyDict;
        }
    }

    #region ��Ÿ ����Ͻ� ����
    public static void SetStoryDict(int idx, bool value)
    {
        if (!_savedata.storyReadDict.ContainsKey(idx))
        {
            _savedata.storyReadDict.Add(idx, value);
        }
        else
        {
            _savedata.storyReadDict[idx] = value;
        }
        SaveData();
    }

    public static double GetBookmarkValuePerSec()
    {
        double value = 0.0d;
        double roomValue = SaveManager.GetRoomValue();

        foreach (var item in _savedata.bookMarkLevelDict)
        {
            if(item.Value > -1)
            {
                if (TryGetContentLevel(DataType.Accessory, item.Key, out int level) && level > 0)
                {
                    value += ValueCalculator.GetOutputValueOnLevel(item.Key, item.Value) / 3 * 2;
                }
                else
                {
                    value += ValueCalculator.GetOutputValueOnLevel(item.Key, item.Value) / 3;
                }
            }
        }

        value *= roomValue < 1 ? 1 : roomValue;
        value *= ResearchMultiplyDict["PerSec"];
        value *= ResearchMultiplyDict["All"];

        return value;
    }

    public static int GetRoomValue()
    {
        var roomlist = _savedata.roomLevelDict.ToList();
        roomlist.Sort((x, y) => 
        {
            if(y.Value.CompareTo(x.Value) == 0)
            {
                return y.Key.CompareTo(x.Key);
            }
            else
            {
                return y.Value.CompareTo(x.Value);
            }    
        });
        return roomlist[0].Key + 1;
    }

    public static bool TryGetContentLevel(DataType type, int idx, out int level)
    {
        switch (type)
        {
            case DataType.Book:
                if (!_savedata.bookLevelDict.ContainsKey(idx))
                {
                    break;
                }
                level = _savedata.bookLevelDict[idx];
                return true;
            case DataType.BookMark:
                if (!_savedata.bookMarkLevelDict.ContainsKey(idx))
                {
                    break;
                }
                level = _savedata.bookMarkLevelDict[idx];
                return true;
            case DataType.Accessory:
                if (!_savedata.accessoryLevelDict.ContainsKey(idx))
                {
                    break;
                }
                level = _savedata.accessoryLevelDict[idx];
                return true;
            case DataType.Room:
                if (!_savedata.roomLevelDict.ContainsKey(idx))
                {
                    break;
                }
                level = _savedata.roomLevelDict[idx];
                return true;
            case DataType.Research:
                if (!_savedata.researchLevelDict.ContainsKey(idx))
                {
                    break;
                }
                level = _savedata.researchLevelDict[idx];
                return true;
        }

        level = -2;
        return false;
    }

    public static int GetCountOfUnlockDatas(DataType type)
    {
        switch (type)
        {
            case DataType.Book:
                return _savedata.bookLevelDict.Values.ToList().FindAll(x => x >= 1).Count;
            case DataType.BookMark:
                return _savedata.bookMarkLevelDict.Values.ToList().FindAll(x => x >= 1).Count;
            case DataType.Accessory:
                return _savedata.accessoryLevelDict.Values.ToList().FindAll(x => x >= 1).Count;
            case DataType.Room:
                return _savedata.roomLevelDict.Values.ToList().FindAll(x => x >= 1).Count;
            case DataType.Research:
                return _savedata.researchLevelDict.Values.ToList().FindAll(x => x >= 1).Count;
            default:
                break;
        }
        return 0;
    }

    /// <summary>
    /// �ش� �������� ������ �����ɴϴ�.
    /// �ر� ���� ���� �������� -1 �� ��ȯ�մϴ�.
    /// ���� ������ �ش� ������ -1 �� �������� ����ϴ�.
    /// ���� ��Ȳ�� -2�� ��ȯ�մϴ�.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="idx"></param>
    /// <returns></returns>
    public static int GetContentLevel(DataType type, int idx)
    {
        switch (type)
        {
            case DataType.Book:
                if (!_savedata.bookLevelDict.ContainsKey(idx))
                {
                    _savedata.bookLevelDict.Add(idx, -1);
                }
                return _savedata.bookLevelDict[idx];
            case DataType.BookMark:
                if (!_savedata.bookMarkLevelDict.ContainsKey(idx))
                {
                    _savedata.bookMarkLevelDict.Add(idx, -1);
                }
                return _savedata.bookMarkLevelDict[idx];
            case DataType.Accessory:
                if (!_savedata.accessoryLevelDict.ContainsKey(idx))
                {
                    _savedata.accessoryLevelDict.Add(idx, -1);
                }
                return _savedata.accessoryLevelDict[idx];
            case DataType.Room:
                if (!_savedata.roomLevelDict.ContainsKey(idx))
                {
                    _savedata.roomLevelDict.Add(idx, -1);
                }
                return _savedata.roomLevelDict[idx];
            case DataType.Research:
                if (!_savedata.researchLevelDict.ContainsKey(idx))
                {
                    _savedata.researchLevelDict.Add(idx, 0);
                }
                return _savedata.researchLevelDict[idx];
        }

        return -2;
    }

    public static bool ContentLevelUp(int idx, DataType type)
    {
        bool isSuccessful = false;

        int updatedLevel = 0;
        switch (type)
        {
            case DataType.Book:
                if (_savedata.bookLevelDict.ContainsKey(idx))
                {
                    updatedLevel = ++_savedata.bookLevelDict[idx];
                    isSuccessful = true;
                }
                break;
            case DataType.BookMark:
                if (_savedata.bookMarkLevelDict.ContainsKey(idx))
                {
                    updatedLevel = ++_savedata.bookMarkLevelDict[idx];
                    isSuccessful = true;
                }
                break;
            case DataType.Accessory:
                if (_savedata.accessoryLevelDict.ContainsKey(idx))
                {
                    updatedLevel = ++_savedata.accessoryLevelDict[idx];
                    isSuccessful = true;
                }
                break;
            case DataType.Room:
                if (_savedata.roomLevelDict.ContainsKey(idx))
                {
                    updatedLevel = ++_savedata.roomLevelDict[idx];
                    isSuccessful = true;
                }
                break;
            case DataType.Research:
                if (_savedata.researchLevelDict.ContainsKey(idx))
                {
                    updatedLevel = ++_savedata.researchLevelDict[idx];
                    isSuccessful = true;
                    RefreshResearch();
                }
                break;
        }

        if (isSuccessful)
            OnUpdateLevel?.Invoke(idx, type, updatedLevel);

        return isSuccessful;
    }

    public static bool SetContentLevel(int idx, DataType type, int level)
    {
        bool isSuccessful = false;

        switch (type)
        {
            case DataType.Book:
                if (_savedata.bookLevelDict.ContainsKey(idx))
                {
                    _savedata.bookLevelDict[idx] = level;
                    isSuccessful = true;
                }
                break;
            case DataType.BookMark:
                if (_savedata.bookMarkLevelDict.ContainsKey(idx))
                {
                    _savedata.bookMarkLevelDict[idx] = level;
                    isSuccessful = true;
                }
                break;
            case DataType.Accessory:
                if (_savedata.accessoryLevelDict.ContainsKey(idx))
                {
                    _savedata.accessoryLevelDict[idx] = level;
                    isSuccessful = true;
                }
                break;
            case DataType.Room:
                if (_savedata.roomLevelDict.ContainsKey(idx))
                {
                    _savedata.roomLevelDict[idx] = level;
                    isSuccessful = true;
                }
                break;
            case DataType.Research:
                if (_savedata.researchLevelDict.ContainsKey(idx))
                {
                    _savedata.researchLevelDict[idx] = level;
                    isSuccessful = true;
                    RefreshResearch();
                }
                break;
        }

        if(isSuccessful) // �۾��� �������� ���� �̺�Ʈ �߻�
            OnUpdateLevel?.Invoke(idx, type, level);

        return isSuccessful;
    }
    #endregion

    #region ���̺� ���� ����
    public static void SaveData()
    {
        _savedata.saveTimeString = DateTime.Now.ToString();
        string encryptedProfile = Encrypt(JsonUtility.ToJson(_profileData));
        string encryptedData = Encrypt(JsonUtility.ToJson(_savedata));
        SaveFile(encryptedProfile, _profileSavePath);
        SaveFile(encryptedData, _dataSavePath);
    }

    public static SaveData LoadData()
    {
        //������ �����ϴ������� üũ.
        if (File.Exists(_dataSavePath))
        {
            _savedata = JsonUtility.FromJson<SaveData>(Decrypt(LoadFile(_dataSavePath)));
            AfterLoadData();
            return _savedata;
        }

        _savedata = new SaveData();
        if (!_savedata.roomLevelDict.ContainsKey(0))
        {
            _savedata.roomLevelDict.Add(0, 1);
        }
        else
        {
            _savedata.roomLevelDict[0] = 1;
        }

        AfterLoadData();
        return _savedata;
    }

    public static PlayerProfile LoadProfile()
    {
        //������ �����ϴ������� üũ.
        if (File.Exists(_profileSavePath))
        {
            _profileData = JsonUtility.FromJson<PlayerProfile>(Decrypt(LoadFile(_profileSavePath)));
            AfterLoadProfileData();
            return _profileData;
        }

        _savedata = new SaveData();
        if (!_savedata.roomLevelDict.ContainsKey(0))
        {
            _savedata.roomLevelDict.Add(0, 1);
        }
        else
        {
            _savedata.roomLevelDict[0] = 1;
        }

        AfterLoadProfileData();
        return _profileData;
    }

    private static void AfterLoadData()
    {
        ResearchCalc();
        RefreshResearch();
    }

    private static void AfterLoadProfileData()
    {

    }

    private static void ResearchCalc()
    {
        DateTime lastSaveTime = DateTime.Parse(_savedata.saveTimeString);
        TimeSpan spanedTime = DateTime.Now - lastSaveTime;

        int spanedMinutes = spanedTime.Minutes;

        _savedata.researchSaveData.researchRemainTime -= spanedTime.TotalSeconds;
        if(_savedata.researchSaveData.researchRemainTime <= 0)
        {
            _savedata.researchSaveData.researchRemainTime = 0;
            GameObject.FindObjectOfType<ResearchManager>().SetResearch(_savedata.researchSaveData.curResearchIdx, () => ContentLevelUp(_savedata.researchSaveData.curResearchIdx, DataType.Research));
        }

        while (spanedMinutes >= 15)
        {
            if(_savedata.researchSaveData.ResearchResources < _savedata.researchSaveData.researchResourceMaxCount)
                _savedata.researchSaveData.ResearchResources++;

            spanedMinutes -= 15;
        }
    }

    public static void RefreshResearch()
    {
        int level;
        if (TryGetContentLevel(DataType.Research, 0, out level))
        {
            _researchMultiplyDict["PerSec"] += 0.05f * level;
        }

        if (TryGetContentLevel(DataType.Research, 1, out level))
        {
            _researchMultiplyDict["PerSec"] += 0.10f * level;
        }

        if (TryGetContentLevel(DataType.Research, 2, out level))
        {
            _researchMultiplyDict["Touch"] += 0.05f * level;
        }

        if (TryGetContentLevel(DataType.Research, 3, out level))
        {
            for (int i = 0; i < level; i++)
            {
                if (i == 3)
                {
                    _researchMultiplyDict["Touch"] += 0.06f;
                    break;
                }
                _researchMultiplyDict["Touch"] += 0.07f;
            }
        }

        if (TryGetContentLevel(DataType.Research, 4, out level))
        {
            _researchMultiplyDict["All"] += 0.03f * level;
        }

        if (TryGetContentLevel(DataType.Research, 5, out level))
        {
            _researchMultiplyDict["All"] += 0.05f * level;
        }
    }

    private static void SaveFile(string jsonData, string path)
    {
        using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
        {
            //���Ϸ� ������ �� �ְ� ����Ʈȭ
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jsonData);

            //bytes�� ���빰�� 0 ~ max ���̱��� fs�� ����
            fs.Write(bytes, 0, bytes.Length);
        }
    }

    private static string LoadFile(string path)
    {
        using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
        {
            //������ ����Ʈȭ ���� �� ���� ������ ����
            byte[] bytes = new byte[(int)fs.Length];

            //���Ͻ�Ʈ������ ���� ����Ʈ ����
            fs.Read(bytes, 0, (int)fs.Length);

            //������ ����Ʈ�� json string���� ���ڵ�
            string jsonString = System.Text.Encoding.UTF8.GetString(bytes);
            return jsonString;
        }
    }
    #endregion

    #region ��ȣȭ
    private static string Encrypt(string data)
    {
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(data);
        RijndaelManaged rm = CreateRijndaelManaged();
        ICryptoTransform ct = rm.CreateEncryptor();
        byte[] results = ct.TransformFinalBlock(bytes, 0, bytes.Length);
        return System.Convert.ToBase64String(results, 0, results.Length);
    }

    private static string Decrypt(string data)
    {
        byte[] bytes = System.Convert.FromBase64String(data);
        RijndaelManaged rm = CreateRijndaelManaged();
        ICryptoTransform ct = rm.CreateDecryptor();
        byte[] resultArray = ct.TransformFinalBlock(bytes, 0, bytes.Length);
        return System.Text.Encoding.UTF8.GetString(resultArray);
    }

    private static RijndaelManaged CreateRijndaelManaged()
    {
        byte[] keyArray = System.Text.Encoding.UTF8.GetBytes(_privateKey);
        RijndaelManaged result = new RijndaelManaged();

        byte[] newKeysArray = new byte[16];
        System.Array.Copy(keyArray, 0, newKeysArray, 0, 16);

        result.Key = newKeysArray;
        result.Mode = CipherMode.ECB;
        result.Padding = PaddingMode.PKCS7;
        return result;
    }
    #endregion
}

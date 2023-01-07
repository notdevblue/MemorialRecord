using MemorialRecord.Data;
using System;
using System.Linq;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;

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

    public static double CurMemorial
    {
        get
        {
            return _data.currentMemorial;
        }
        set
        {
            _data.currentMemorial = value;
            SaveData();
            OnChangeMemorial?.Invoke(value);
        }
    }

    public static long CurQuillPen
    {
        get
        {
            return _data.currentQuillPen;
        }
        set
        {
            _data.currentQuillPen = value;
            OnChangeQuillPen?.Invoke(value);
        }
    }

    public static float MusicVolume
    {
        get
        {
            return _data.musicVolume;
        }
        set
        {
            _data.musicVolume = value;
            OnChangeMusicVolume?.Invoke(value);
        }
    }

    public static float SoundEffectVolume
    {
        get
        {
            return _data.soundEffectVolume;
        }
        set
        {
            _data.soundEffectVolume = value;
            OnChangeSoundEffectVolume?.Invoke(value);
        }
    }

    public static string Name
    {
        get
        {
            return _data.characterName;
        }
        set
        {
            _data.characterName = value;
            SaveData();
        }
    }

    #region readonly private fields
    public static readonly string _savePath = Application.persistentDataPath + "/SaveData.txt";
    public static readonly string _defaultSavePath = "Assets/Resources/DefaultSaveData.txt";
    private static readonly string _privateKey = "z$C&F)J@NcRfUjXn";//16byte 이상의 문자열
    #endregion

    private static SaveData _data = new MemorialRecord.Data.SaveData();

    #region 기타 비즈니스 로직

    public static double GetBookmarkValuePerSec()
    {
        double value = 0.0d;
        double roomValue = SaveManager.GetRoomValue();

        foreach (var item in _data.bookMarkLevelDict)
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
        return value;
    }

    public static int GetRoomValue()
    {
        var roomlist = _data.roomInfoLevelsDict.ToList();
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
                if (!_data.bookLevelDict.ContainsKey(idx))
                {
                    break;
                }
                level = _data.bookLevelDict[idx];
                return true;
            case DataType.BookMark:
                if (!_data.bookMarkLevelDict.ContainsKey(idx))
                {
                    break;
                }
                level = _data.bookMarkLevelDict[idx];
                return true;
            case DataType.Accessory:
                if (!_data.accessoryLevelDict.ContainsKey(idx))
                {
                    break;
                }
                level = _data.accessoryLevelDict[idx];
                return true;
            case DataType.Room:
                if (!_data.roomInfoLevelsDict.ContainsKey(idx))
                {
                    break;
                }
                level = _data.roomInfoLevelsDict[idx];
                return true;
            case DataType.Research:
                // TODO : 구현 필요
                break;
        }

        level = -2;
        return false;
    }

    /// <summary>
    /// 해당 콘텐츠의 레벨을 가져옵니다.
    /// 해금 되지 않은 콘텐츠는 -1 을 반환합니다.
    /// 값이 없으면 해당 레벨에 -1 의 콘텐츠를 만듭니다.
    /// 예외 상황엔 -2를 반환합니다.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="idx"></param>
    /// <returns></returns>
    public static int GetContentLevel(DataType type, int idx)
    {
        switch (type)
        {
            case DataType.Book:
                if (!_data.bookLevelDict.ContainsKey(idx))
                {
                    _data.bookLevelDict.Add(idx, -1);
                }
                return _data.bookLevelDict[idx];
            case DataType.BookMark:
                if (!_data.bookMarkLevelDict.ContainsKey(idx))
                {
                    _data.bookMarkLevelDict.Add(idx, -1);
                }
                return _data.bookMarkLevelDict[idx];
            case DataType.Accessory:
                if (!_data.accessoryLevelDict.ContainsKey(idx))
                {
                    _data.accessoryLevelDict.Add(idx, -1);
                }
                return _data.accessoryLevelDict[idx];
            case DataType.Room:
                if (!_data.roomInfoLevelsDict.ContainsKey(idx))
                {
                    _data.roomInfoLevelsDict.Add(idx, -1);
                }
                return _data.roomInfoLevelsDict[idx];
            case DataType.Research:
                // TODO : 구현 필요
                break;
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
                if (_data.bookLevelDict.ContainsKey(idx))
                {
                    updatedLevel = ++_data.bookLevelDict[idx];
                    isSuccessful = true;
                }
                break;
            case DataType.BookMark:
                if (_data.bookMarkLevelDict.ContainsKey(idx))
                {
                    updatedLevel = ++_data.bookMarkLevelDict[idx];
                    isSuccessful = true;
                }
                break;
            case DataType.Accessory:
                if (_data.accessoryLevelDict.ContainsKey(idx))
                {
                    updatedLevel = ++_data.accessoryLevelDict[idx];
                    isSuccessful = true;
                }
                break;
            case DataType.Room:
                if (_data.roomInfoLevelsDict.ContainsKey(idx))
                {
                    updatedLevel = ++_data.roomInfoLevelsDict[idx];
                    isSuccessful = true;
                }
                break;
            case DataType.Research:
                // TODO : 구현 필요
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
                if (_data.bookLevelDict.ContainsKey(idx))
                {
                    _data.bookLevelDict[idx] = level;
                    isSuccessful = true;
                }
                break;
            case DataType.BookMark:
                if (_data.bookMarkLevelDict.ContainsKey(idx))
                {
                    _data.bookMarkLevelDict[idx] = level;
                    isSuccessful = true;
                }
                break;
            case DataType.Accessory:
                if (_data.accessoryLevelDict.ContainsKey(idx))
                {
                    _data.accessoryLevelDict[idx] = level;
                    isSuccessful = true;
                }
                break;
            case DataType.Room:
                if (_data.roomInfoLevelsDict.ContainsKey(idx))
                {
                    _data.roomInfoLevelsDict[idx] = level;
                    isSuccessful = true;
                }
                break;
            case DataType.Research:
                // TODO : 구현 필요
                break;
        }

        if(isSuccessful) // 작업이 성공했을 때만 이벤트 발생
            OnUpdateLevel?.Invoke(idx, type, level);

        return isSuccessful;
    }
    #endregion

    #region 세이브 관련 로직
    public static void SaveData()
    {
        SaveFile(Encrypt(JsonUtility.ToJson(_data)));
    }

    public static SaveData LoadData()
    {
        //파일이 존재하는지부터 체크.
        if (File.Exists(_savePath))
        {
            _data = JsonUtility.FromJson<SaveData>(Decrypt(LoadFile(_savePath)));
            return _data;
        }

        _data = new SaveData();
        if (!_data.roomInfoLevelsDict.ContainsKey(0))
        {
            _data.roomInfoLevelsDict.Add(0, 1);
        }
        else
        {
            _data.roomInfoLevelsDict[0] = 1;
        }
            
        return _data;
    }

    private static void SaveFile(string jsonData)
    {
        using (FileStream fs = new FileStream(_savePath, FileMode.Create, FileAccess.Write))
        {
            //파일로 저장할 수 있게 바이트화
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jsonData);

            //bytes의 내용물을 0 ~ max 길이까지 fs에 복사
            fs.Write(bytes, 0, bytes.Length);
        }
    }

    private static string LoadFile(string path)
    {
        using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
        {
            //파일을 바이트화 했을 때 담을 변수를 제작
            byte[] bytes = new byte[(int)fs.Length];

            //파일스트림으로 부터 바이트 추출
            fs.Read(bytes, 0, (int)fs.Length);

            //추출한 바이트를 json string으로 인코딩
            string jsonString = System.Text.Encoding.UTF8.GetString(bytes);
            return jsonString;
        }
    }
    #endregion

    #region 암호화
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

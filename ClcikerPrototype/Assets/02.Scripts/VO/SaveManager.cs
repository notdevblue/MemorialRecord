using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MemorialRecord.Data;
using System;
using System.Security.Cryptography;
using System.IO;

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
    #region readonly private fields
    public static readonly string _savePath = Application.persistentDataPath + "/SaveData.txt";
    public static readonly string _defaultSavePath = "Assets/Resources/DefaultSaveData.txt";
    private static readonly string _privateKey = "z$C&F)J@NcRfUjXn";//16byte 이상의 문자열
    #endregion

    private static SaveData _data = new MemorialRecord.Data.SaveData();

    // Getter 
    public static SaveData Data 
    { 
        get 
        { 
            if(_data == null)
            {
                _data = LoadData();
            }
            return _data; 
        } 
    }

    public static string _name = "";

    #region 기타 비즈니스 로직
    public static void SaveName(string name)
    {
        _data.characterName = name;
    }

    /// <summary>
    /// 해당 콘텐츠의 레벨을 가져옵니다.
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
    #endregion

    #region 세이브 관련 로직
    public static void SaveData()
    {
        Debug.Log("LocalSave" + DateTime.Now + " " + _savePath);
        SaveFile(Encrypt(JsonUtility.ToJson(_data)));
    }

    public static SaveData LoadData()
    {
        Debug.Log("LocalLoad" + DateTime.Now + " " + _savePath);
        //파일이 존재하는지부터 체크.
        if (!File.Exists(_savePath))
        {
            _data = JsonUtility.FromJson<SaveData>(Decrypt(LoadFile(_savePath)));
            return _data;
        }

        _data = JsonUtility.FromJson<SaveData>(Decrypt(LoadFile(_savePath)));
        return _data;
        // TODO : 다른 시스템에 이름 넘겨주기
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

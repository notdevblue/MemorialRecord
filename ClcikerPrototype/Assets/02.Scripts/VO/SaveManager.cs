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
    private static readonly string _privateKey = "z$C&F)J@NcRfUjXn";//16byte �̻��� ���ڿ�
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

    #region ��Ÿ ����Ͻ� ����
    public static void SaveName(string name)
    {
        _data.characterName = name;
    }

    /// <summary>
    /// �ش� �������� ������ �����ɴϴ�.
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
                // TODO : ���� �ʿ�
                break;
        }

        return -2;
    }
    #endregion

    #region ���̺� ���� ����
    public static void SaveData()
    {
        Debug.Log("LocalSave" + DateTime.Now + " " + _savePath);
        SaveFile(Encrypt(JsonUtility.ToJson(_data)));
    }

    public static SaveData LoadData()
    {
        Debug.Log("LocalLoad" + DateTime.Now + " " + _savePath);
        //������ �����ϴ������� üũ.
        if (!File.Exists(_savePath))
        {
            _data = JsonUtility.FromJson<SaveData>(Decrypt(LoadFile(_savePath)));
            return _data;
        }

        _data = JsonUtility.FromJson<SaveData>(Decrypt(LoadFile(_savePath)));
        return _data;
        // TODO : �ٸ� �ý��ۿ� �̸� �Ѱ��ֱ�
    }

    private static void SaveFile(string jsonData)
    {
        using (FileStream fs = new FileStream(_savePath, FileMode.Create, FileAccess.Write))
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

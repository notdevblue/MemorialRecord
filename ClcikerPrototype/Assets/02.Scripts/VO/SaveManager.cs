using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MemorialRecord.Data;
using System;
using System.Security.Cryptography;

public static class SaveManager
{
    #region readonly private fields
    public static readonly string savePath = Application.persistentDataPath + "/SaveData.txt";
    public static readonly string defaultSavePath = "Assets/Resources/DefaultSaveData.txt";
    private static readonly string privateKey = "z$C&F)J@NcRfUjXn";//16byte 이상의 문자열
    #endregion

    private static SaveData data;

    // Getter 
    public static SaveData Data => data;

    public static void SaveData()
    {
        Debug.Log("LocalSave" + DateTime.Now + " " + savePath);

        SaveManager.data.characterName = ""; // GameManager.Instance.characterName;

        
    }

    private static void SaveToLocalFile(string json)
    {

    }

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
        byte[] keyArray = System.Text.Encoding.UTF8.GetBytes(privateKey);
        RijndaelManaged result = new RijndaelManaged();

        byte[] newKeysArray = new byte[16];
        System.Array.Copy(keyArray, 0, newKeysArray, 0, 16);

        result.Key = newKeysArray;
        result.Mode = CipherMode.ECB;
        result.Padding = PaddingMode.PKCS7;
        return result;
    }

}

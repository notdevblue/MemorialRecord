using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using MemorialRecord.Data;

public class CSVDataReader : Editor
{
    [MenuItem(itemName: "Data/Read/ReadBookDataFromCSV")]
    public static void GetBookDataFromCSV()
    {
        string path = EditorUtility.OpenFilePanel("CSV파일 에서 책 데이터 가져오기", "", "csv");

        if(File.Exists(path))
        {
            using StreamReader sr = new StreamReader(path);

            DataSO dataSO = AssetDatabase.LoadAssetAtPath<DataSO>("Assets/Data/DataListSO.asset");
            BookListSO bookListSO = AssetDatabase.LoadAssetAtPath<BookListSO>("Assets/Data/BookDataListSO.asset");

            if (!bookListSO)
            {
                bookListSO = CreateInstance<BookListSO>();
            }

            string str = sr.ReadLine();
            str = sr.ReadLine(); // 첫줄 지우기위해서 한번 더

            bookListSO.bookDatas.Clear();
            while (str != null)
            {
                string[] strArr = str.Split(',');

                bookListSO.bookDatas.Add(
                    new BookData(
                        int.Parse(strArr[0]),
                        strArr[1],
                        strArr[2])
                    );

                str = sr.ReadLine();
            }

            if (dataSO == null)
            {
                dataSO = CreateInstance<DataSO>();
                dataSO._bookListSO = bookListSO;
            }

            if (AssetDatabase.LoadAssetAtPath<DataSO>("Assets/Data/DataListSO.asset") == null)
            {
                AssetDatabase.CreateFolder("Assets", "Data");
                AssetDatabase.CreateAsset(dataSO, "Assets/Data/DataListSO.asset");
            }

            if (!AssetDatabase.LoadAssetAtPath<BookListSO>("Assets/Data/BookDataListSO.asset"))
            {
                AssetDatabase.CreateAsset(bookListSO, "Assets/Data/BookDataListSO.asset");
            }

            AssetDatabase.SaveAssets();
        }
    }
}

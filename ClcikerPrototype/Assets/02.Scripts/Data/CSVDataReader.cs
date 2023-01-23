using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using MemorialRecord.Data;

#if UNITY_EDITOR
public class CSVDataReader : Editor
{
    [MenuItem(itemName: "Data/Read/ReadBookDataFromCSV")]
    public static void GetBookDataFromCSV()
    {
        string path = EditorUtility.OpenFilePanel("CSV파일에서 책 데이터 가져오기", "", "csv");

        if(File.Exists(path))
        {
            using StreamReader sr = new StreamReader(path);

            InitDataSO dataSO = AssetDatabase.LoadAssetAtPath<InitDataSO>("Assets/Data/DataListSO.asset");
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
                        int.Parse(strArr[0]), // idx
                        AssetDatabase.LoadAssetAtPath<Sprite>("Assets/03.Sprites/UI/Icons/책.png"), // sprite
                        strArr[1], // Title
                        strArr[2]) // Writer
                    );

                str = sr.ReadLine();
            }

            if (dataSO == null)
            {
                dataSO = CreateInstance<InitDataSO>();
                dataSO._bookListSO = bookListSO;
            }
            Debug.Log("데이터 에셋 생성");

            if (AssetDatabase.LoadAssetAtPath<InitDataSO>("Assets/Data/DataListSO.asset") == null)
            {
                AssetDatabase.CreateFolder("Assets", "Data");
                AssetDatabase.CreateAsset(dataSO, "Assets/Data/DataListSO.asset");
                Debug.Log("데이터 에셋 생성");
            }

            if (!AssetDatabase.LoadAssetAtPath<BookListSO>("Assets/Data/BookDataListSO.asset"))
            {
                AssetDatabase.CreateAsset(bookListSO, "Assets/Data/BookDataListSO.asset");
                Debug.Log("에셋 생성");
            }

            AssetDatabase.SaveAssets();
        }
    }

    [MenuItem(itemName: "Data/Read/ReadBookMarkDataFromCSV")]
    public static void GetBookmarkDataFromCSV()
    {
        string path = EditorUtility.OpenFilePanel("CSV파일에서 책갈피 데이터 가져오기", "", "csv");

        if (File.Exists(path))
        {
            using StreamReader sr = new StreamReader(path);

            InitDataSO dataSO = AssetDatabase.LoadAssetAtPath<InitDataSO>("Assets/Data/DataListSO.asset");
            BookmarkListSO bookmarkListSO = AssetDatabase.LoadAssetAtPath<BookmarkListSO>("Assets/Data/BookMarkDataListSO.asset");

            if (!bookmarkListSO)
            {
                bookmarkListSO = CreateInstance<BookmarkListSO>();
            }

            string str = sr.ReadLine();
            str = sr.ReadLine(); // 첫줄 지우기위해서 한번 더

            bookmarkListSO.bookmarkDatas.Clear();
            while (str != null)
            {
                string[] strArr = str.Split(',');

                bookmarkListSO.bookmarkDatas.Add(
                    new BookmarkData(
                        int.Parse(strArr[0]), // idx
                        AssetDatabase.LoadAssetAtPath<Sprite>("Assets/03.Sprites/UI/Icons/책.png"), // sprite    
                        strArr[1] // Name
                        )
                    );

                str = sr.ReadLine();
            }

            if (dataSO == null)
            {
                dataSO = CreateInstance<InitDataSO>();
                dataSO._bookmarkListSO = bookmarkListSO;
            }

            if (!AssetDatabase.LoadAssetAtPath<InitDataSO>("Assets/Data/DataListSO.asset"))
            {
                AssetDatabase.CreateFolder("Assets", "Data");
                AssetDatabase.CreateAsset(dataSO, "Assets/Data/DataListSO.asset");
            }

            if (!AssetDatabase.LoadAssetAtPath<BookmarkListSO>("Assets/Data/BookMarkDataListSO.asset"))
            {
                AssetDatabase.CreateAsset(bookmarkListSO, "Assets/Data/BookMarkDataListSO.asset");
            }

            AssetDatabase.SaveAssets();
        }
    }

    [MenuItem(itemName: "Data/Read/ReadAccDataFromCSV")]
    public static void ReadAccDataFromCSV()
    {
        string path = EditorUtility.OpenFilePanel("CSV파일에서 악세서리 데이터 가져오기", "", "csv");

        if (File.Exists(path))
        {
            using StreamReader sr = new StreamReader(path);

            InitDataSO dataSO = AssetDatabase.LoadAssetAtPath<InitDataSO>("Assets/Data/DataListSO.asset");
            AccessoryListSO accDataListSO = AssetDatabase.LoadAssetAtPath<AccessoryListSO>("Assets/Data/AccessoryDataListSO.asset");

            if (!accDataListSO)
            {
                accDataListSO = CreateInstance<AccessoryListSO>();
            }

            string str = sr.ReadLine();
            str = sr.ReadLine(); // 첫줄 지우기위해서 한번 더

            accDataListSO.accessoryDatas.Clear();
            while (str != null)
            {
                string[] strArr = str.Split(',');

                accDataListSO.accessoryDatas.Add(
                    new AccessoryData(
                        int.Parse(strArr[0]), // idx
                        AssetDatabase.LoadAssetAtPath<Sprite>("Assets/03.Sprites/UI/Icons/책.png"), // sprite    
                        strArr[1] // Name
                        )
                    );

                str = sr.ReadLine();
            }

            if (dataSO == null)
            {
                dataSO = CreateInstance<InitDataSO>();
                dataSO._accessoryListSO = accDataListSO;
            }

            if (!AssetDatabase.LoadAssetAtPath<InitDataSO>("Assets/Data/DataListSO.asset"))
            {
                AssetDatabase.CreateFolder("Assets", "Data");
                AssetDatabase.CreateAsset(dataSO, "Assets/Data/DataListSO.asset");
            }

            if (!AssetDatabase.LoadAssetAtPath<AccessoryListSO>("Assets/Data/AccessoryDataListSO.asset"))
            {
                AssetDatabase.CreateAsset(accDataListSO, "Assets/Data/AccessoryDataListSO.asset");
            }

            AssetDatabase.SaveAssets();
        }
    }

    [MenuItem(itemName: "Data/Read/ReadRoomDataFromCSV")]
    public static void ReadRoomDataFromCSV()
    {
        string path = EditorUtility.OpenFilePanel("CSV파일에서 방 데이터 가져오기", "", "csv");

        if (File.Exists(path))
        {
            using StreamReader sr = new StreamReader(path);

            InitDataSO dataSO = AssetDatabase.LoadAssetAtPath<InitDataSO>("Assets/Data/DataListSO.asset");
            RoomListSO roomDataListSO = AssetDatabase.LoadAssetAtPath<RoomListSO>("Assets/Data/RoomDataListSO.asset");

            if (!roomDataListSO)
            {
                roomDataListSO = CreateInstance<RoomListSO>();
            }

            string str = sr.ReadLine();
            str = sr.ReadLine(); // 첫줄 지우기위해서 한번 더

            roomDataListSO.roomDatas.Clear();
            while (str != null)
            {
                string[] strArr = str.Split(',');

                roomDataListSO.roomDatas.Add(
                    new RoomData(
                        int.Parse(strArr[0]), // idx
                        AssetDatabase.LoadAssetAtPath<Sprite>("Assets/03.Sprites/UI/Icons/책.png"), // sprite   
                        strArr[1] // Name
                        )
                    );

                str = sr.ReadLine();
            }

            if (dataSO == null)
            {
                dataSO = CreateInstance<InitDataSO>();
                dataSO._roomListSO = roomDataListSO;
            }

            if (!AssetDatabase.LoadAssetAtPath<InitDataSO>("Assets/Data/DataListSO.asset"))
            {
                AssetDatabase.CreateFolder("Assets", "Data");
                AssetDatabase.CreateAsset(dataSO, "Assets/Data/DataListSO.asset");
            }

            if (!AssetDatabase.LoadAssetAtPath<RoomListSO>("Assets/Data/RoomDataListSO.asset"))
            {
                AssetDatabase.CreateAsset(roomDataListSO, "Assets/Data/RoomDataListSO.asset");
            }

            AssetDatabase.SaveAssets();
        }
    }
}
#endif
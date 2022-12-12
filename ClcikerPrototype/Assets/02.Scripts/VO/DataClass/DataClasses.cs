using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemorialRecord.Data
{
    [System.Serializable]
    public class DataParent
    {
        public int _idx;

        public string _title;
        public string _writer;
        public string _name;

        public Sprite _image;
    }


    [System.Serializable]
    public class BookData : DataParent
    {
        public BookData(int idx, Sprite sprite, string title, string writer)
        {
            _idx = idx;
            _image = sprite;
            _title = title;
            _writer = writer;
        }
    }

    [System.Serializable]
    public class BookmarkData : DataParent
    {
        public BookmarkData(int idx, Sprite sprite, string name)
        {
            _idx = idx;
            _image = sprite;
            _name = name;
        }
    }

    [System.Serializable]
    public class AccessoryData : DataParent
    {
        public AccessoryData(int idx, Sprite sprite, string name)
        {
            _idx = idx;
            _image = sprite;
            _name = name;
        }
    }

    public class RoomData : DataParent
    {

    }

    [System.Serializable]
    public class researchData : DataParent
    {

    }

}

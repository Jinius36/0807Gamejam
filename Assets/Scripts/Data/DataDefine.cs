using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataDefine
{
    [System.Serializable]
    public class SaveData
    {
        public int _currentStage;
        public SaveData()
        {
            _currentStage = 1;
        }
    }
    [System.Serializable]
    public class Item
    {
        public List<ItemData> itemData;
    }
    [System.Serializable]
    public class ItemData
    {
        public bool isGet;
        public ItemData()
        {
            isGet = false;
        }
    }

}

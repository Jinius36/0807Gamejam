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
        public float _bgmVolume;
        public float _sfxVolume;
        public SaveData()
        {
            _currentStage = 1;
            _bgmVolume = 50.0f;
            _sfxVolume = 50.0f;
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

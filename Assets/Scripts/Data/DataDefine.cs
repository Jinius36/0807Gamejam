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

}

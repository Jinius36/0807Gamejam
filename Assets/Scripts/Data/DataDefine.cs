using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataDefine
{
    [System.Serializable]
    public class MusicData
    {
        public List<Music> music;
    }
    [System.Serializable]
    public class Music
    {
        public List<BeatData> beatData;
        public List<Data> data;
    }

    [System.Serializable]
    public class BeatData
    {
        public float touchTime;
    }
    [System.Serializable]
    public class Data
    {
        public int index;
        public float bpm;
        public int count;
    }
    [System.Serializable]
    public class UserCharacterData
    {
        public List<Characters> characters;
    }
    [System.Serializable]
    public class Characters
    {
        public int _index;
        public string _rarity;
        public bool _isHave;
        public int _shape;
        public int _weight;

        public Characters(int index, bool isHave, string rarity, int shape, int weight)
        {
            this._index = index;
            this._isHave = isHave;
            this._rarity = rarity;
            this._shape = shape;
            this._weight = weight;
        }
        public Characters()
        {
            _index = 0;
            _isHave = true;
            _rarity = "defualt";
            _shape = 0;
            _weight = 0;
        }
    }

    [System.Serializable]
    public class WholeGameData
    {
        public bool _tutorialClear;
        public int _coin;
        public int _currentSong;
        public float _masterVolume;
        public float _bgmVolume;
        public float _sfxVolume;

        public WholeGameData(bool tutorialClear, int coin, float masterVolume, float bgmVolume, float sfxVolume, int currentSong)
        {
            this._tutorialClear = tutorialClear;
            this._coin = coin;
            this._masterVolume = masterVolume;
            this._bgmVolume = bgmVolume;
            this._sfxVolume = sfxVolume;
            this._currentSong = currentSong;
        }
        public WholeGameData()
        {
            _tutorialClear = false;
            _coin = 0;
            _masterVolume = 0.5f;
            _bgmVolume = 0.5f;
            _sfxVolume = 0.5f;
            _currentSong = 5;
        }
    }
    [System.Serializable]
    public class SaveData
    {
        public string _currentCharacter;
        public string _rarity;
        public Sprite _changeSprite;
        public SaveData()
        {
            _currentCharacter = "default1";
            _rarity = "default";
        }
    }

}

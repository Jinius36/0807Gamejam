using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_MainSetting : UI_Popup
{
    public enum Buttons
    {
        Main
    }
    public enum GameObjects
    {
        BgmSlider,
        SfxSlider
    }
    private Slider _bgmSlider;
    private Slider _sfxSlider;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));
        GetButton((int)Buttons.Main).gameObject.AddUIEvent(BackToMainClick);
        _bgmSlider = Get<GameObject>((int)GameObjects.BgmSlider).GetComponent<Slider>();
        _sfxSlider = Get<GameObject>((int)GameObjects.SfxSlider).GetComponent<Slider>();
        _bgmSlider.value = DataManager.singleTon.saveData._bgmVolume;
        _sfxSlider.value = DataManager.singleTon.saveData._sfxVolume;
        _bgmSlider.gameObject.AddUIEvent(BGMVolume, Define.UIEvent.Drag);
        _sfxSlider.gameObject.AddUIEvent(SFXVolume, Define.UIEvent.Drag);
        if (DataManager.singleTon.saveData._bgmVolume <= -40f)
        {
            Managers.Sound.audioMixer.SetFloat("BGM", -80);
        }
        Managers.Sound.audioMixer.SetFloat("BGM", Mathf.Log10(_bgmSlider.value) * 20);
        if (DataManager.singleTon.saveData._bgmVolume <= -40f)
        {
            Managers.Sound.audioMixer.SetFloat("SFX", -80);
        }
        Managers.Sound.audioMixer.SetFloat("SFX", Mathf.Log10(_sfxSlider.value) * 20);
    }
    public void BackToMainClick(PointerEventData data)
    {
        ClosePopUPUI();
    }
    public void CreditClick(PointerEventData data)
    {
        Managers.UI.ShowPopUpUI<UI_Credit_1>();
    }
    public void BGMVolume(PointerEventData data)
    {
        Debug.Log(_bgmSlider.value);
        DataManager.singleTon.saveData._bgmVolume = _bgmSlider.value;
        DataManager.singleTon.jsonManager.Save<DataDefine.SaveData>(DataManager.singleTon.saveData);
        if (DataManager.singleTon.saveData._bgmVolume <= -40f)
        {
            Managers.Sound.audioMixer.SetFloat("BGM", -80);
        }
        Managers.Sound.audioMixer.SetFloat("BGM", Mathf.Log10(_bgmSlider.value) * 20);
    }
    public void SFXVolume(PointerEventData data)
    {
        Debug.Log(_sfxSlider.value);
        DataManager.singleTon.saveData._sfxVolume = _sfxSlider.value;
        DataManager.singleTon.jsonManager.Save<DataDefine.SaveData>(DataManager.singleTon.saveData);
        if (DataManager.singleTon.saveData._bgmVolume <= -40f)
        {
            Managers.Sound.audioMixer.SetFloat("SFX", -80);
        }
        Managers.Sound.audioMixer.SetFloat("SFX", Mathf.Log10(_sfxSlider.value) * 20);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Audio;

public class UI_Setting_1 : UI_Popup
{
    public enum Buttons
    {
        Main,
        ReStart
    }
    public enum GameObjects
    {
        BgmSlider,
        SfxSlider
    }
    [SerializeField] AudioMixer audioMixer;
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
        GetButton((int)Buttons.ReStart).gameObject.AddUIEvent(RestartStageClick);
        _bgmSlider = Get<GameObject>((int)GameObjects.BgmSlider).GetComponent<Slider>();
        _sfxSlider = Get<GameObject>((int)GameObjects.SfxSlider).GetComponent<Slider>();
        _bgmSlider.value = DataManager.singleTon.saveData._bgmVolume;
        _sfxSlider.value = DataManager.singleTon.saveData._sfxVolume;
        _bgmSlider.gameObject.AddUIEvent(BGMVolume, Define.UIEvent.Drag);
        _sfxSlider.gameObject.AddUIEvent(SFXVolume, Define.UIEvent.Drag);
    }
    public void BackToMainClick(PointerEventData data)
    {
        Managers.Scene.LoadScene(Define.Scene.StartScene);
        Time.timeScale = 1;
    }
    public void RestartStageClick(PointerEventData data)
    {
        switch (DataManager.singleTon.saveData._currentStage)
        {
            case 1:
                Managers.Scene.LoadScene(Define.Scene.GameScene1);
                break;
            case 2:
                Managers.Scene.LoadScene(Define.Scene.GameScene2);
                break;
            case 3:
                Managers.Scene.LoadScene(Define.Scene.GameScene3);
                break;
        }
        Time.timeScale = 1f;
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

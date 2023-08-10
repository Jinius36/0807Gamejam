using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_StartScene : UI_Scene
{
    public enum Buttons
    {
        GameStart,
        Setting
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        GetButton((int)Buttons.GameStart).gameObject.AddUIEvent(StartClick);
        GetButton((int)Buttons.Setting).gameObject.AddUIEvent(SettingClick);
    }
    // Update is called once per frame
    public void StartClick(PointerEventData data)
    {
        Managers.Scene.LoadScene(Define.Scene.GameScene1);
    }
    public void SettingClick(PointerEventData data)
    {
        Managers.UI.ShowPopUpUI<UI_MainSetting>();
    }
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android && Managers.UI.Root.transform.childCount == 1)
        {
            if (Input.GetKey(KeyCode.Escape))

            {
                Application.Quit();
            }
        }
    }
}

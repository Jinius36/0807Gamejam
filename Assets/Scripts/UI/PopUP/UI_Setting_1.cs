using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Setting_1 : UI_Popup
{
    public enum Buttons
    {
        Main,
        ReStart,
        Credit
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
        GetButton((int)Buttons.Main).gameObject.AddUIEvent(BackToMainClick);
        GetButton((int)Buttons.ReStart).gameObject.AddUIEvent(RestartStageClick);
        GetButton((int)Buttons.Credit).gameObject.AddUIEvent(CreditClick);
    }
    public void BackToMainClick(PointerEventData data)
    {
        Managers.Scene.LoadScene(Define.Scene.StartScene);
    }
    public void RestartStageClick(PointerEventData data)
    {
        ClosePopUPUI();
        Time.timeScale = 1f;
    }
    public void CreditClick(PointerEventData data)
    {
        Managers.UI.ShowPopUpUI<UI_Credit>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

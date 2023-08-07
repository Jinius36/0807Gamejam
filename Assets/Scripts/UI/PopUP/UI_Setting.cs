using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Setting : UI_Popup
{
    public enum Buttons
    {
        BackToMain,
        RestartStage,
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
        GetButton((int)Buttons.BackToMain).gameObject.AddUIEvent(BackToMainClick);
        GetButton((int)Buttons.RestartStage).gameObject.AddUIEvent(RestartStageClick);
        GetButton((int)Buttons.Credit).gameObject.AddUIEvent(CreditClick);
    }

    public void BackToMainClick(PointerEventData data)
    {
        Managers.UI.ClosePopUpUI();
    }
    public void RestartStageClick(PointerEventData data)
    {
        //스테이지에 따른 구현 필요
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

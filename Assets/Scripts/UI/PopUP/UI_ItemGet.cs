using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Windows;

public class UI_ItemGet : UI_Popup
{
    public enum GameObjects
    {
        Panel
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(GameObjects));
        Get<GameObject>((int)GameObjects.Panel).AddUIEvent(Click) ;
    }
    void Click(PointerEventData eventData)
    {
        ClosePopUPUI();
        Time.timeScale = 1;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

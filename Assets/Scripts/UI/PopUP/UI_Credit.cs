using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Credit : UI_Popup
{
    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Managers.UI.ClosePopUpUI();
        }
    }
}
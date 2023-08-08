using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : BaseScene
{
    public override void Clear() { }
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.StartScene;
        Managers.UI.ShowSceneUI<UI_StartScene>();
        Managers.Sound.Play("Sounds/BGM/BGM");
    }

    // Update is called once per frame
    void Update()
    {

    }
}

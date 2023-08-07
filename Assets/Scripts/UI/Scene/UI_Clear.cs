using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Clear : UI_Scene
{
    public Image image;
    public enum Images
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
        Bind<Image>(typeof(Images));
        image = GetImage((int)Images.Panel);
        StartCoroutine(FadeCoroutine());
    }
    IEnumerator FadeCoroutine()
    {
        float fadeCount = 1.0f;
        while(fadeCount > 0)
        {
            fadeCount -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            image.color = new Color(0, 0, 0, fadeCount);
        }
        if(DataManager.singleTon.saveData._currentStage == 1)
        {
            DataManager.singleTon.saveData._currentStage = 2;
            Managers.Scene.LoadScene(Define.Scene.GameScene2);
        }
        if(DataManager.singleTon.saveData._currentStage == 2)
        {
            DataManager.singleTon.saveData._currentStage = 3;
            Managers.Scene.LoadScene(Define.Scene.GameScene3);
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

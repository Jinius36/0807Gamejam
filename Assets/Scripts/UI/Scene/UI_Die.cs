using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Die : UI_Scene
{
    public Image image;
    public GameScene1 gameScene1;
    public GameScene2 gameScene2;
    public GameScene3 gameScene3;
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
        TryGetComponent(out gameScene1);
        TryGetComponent(out gameScene2);
        TryGetComponent(out gameScene3);
        Bind<Image>(typeof(Images));
        image = GetImage((int)Images.Panel);
        StartCoroutine(FadeCoroutine());
    }
    IEnumerator FadeCoroutine()
    {
        float fadeCount = 1.0f;
        while (fadeCount > 0)
        {
            fadeCount -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            image.color = new Color(0, 0, 0, fadeCount);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

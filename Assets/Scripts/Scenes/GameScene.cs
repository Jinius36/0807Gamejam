using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    [SerializeField] Vector3 characterSpawn = Vector3.zero;
    [SerializeField] List<Vector3> EnemySpawnList;   
    public override void Clear() { }
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.GameScene;
        Managers.UI.ShowSceneUI<UI_GameScene>();
//        Managers.Resource.Instantiate("Character").transform.position = characterSpawn;
        // switch (DataManager.singleTon.saveData._currentStage)
        // {
        //     case 1:
        //         Managers.Resource.Instantiate("Maps/stage1");
        //         break;
        //     case 2:
        //         Managers.Resource.Instantiate("Maps/stage2");
        //         break;
        //     case 3:
        //         Managers.Resource.Instantiate("Maps/stage3");
        //         break;
        // }
 //       Managers.Resource.Instantiate("Enemy");
    }

    // Update is called once per frame
    void Update()
    {

    }
}

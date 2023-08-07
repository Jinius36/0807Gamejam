using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScene1 : BaseScene
{
    [SerializeField] Vector3 characterSpawn = Vector3.zero;
    [SerializeField] List<Vector3> EnemySpawnList;
    public override void Clear() { }
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.GameScene1;
        Managers.UI.ShowSceneUI<UI_GameScene>();
        Managers.Resource.Instantiate("Character").transform.position = characterSpawn;
        Managers.Resource.Instantiate("Maps/stage1");
        Managers.Resource.Instantiate("Enemy");
    }
    public void ReStart()
    {
        Managers.UI.ShowSceneUI<UI_GameScene>();
        Managers.Resource.Instantiate("Character").transform.position = characterSpawn;
        Managers.Resource.Instantiate("Maps/stage1");
        Managers.Resource.Instantiate("Enemy");
    }

    // Update is called once per frame
    void Update()
    {

    }
}

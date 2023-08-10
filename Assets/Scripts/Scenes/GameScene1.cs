using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScene1 : BaseScene
{
    [SerializeField] Vector3 characterSpawn;
    [SerializeField] List<Vector3> EnemySpawnList;
    [SerializeField] GameObject Player;
    public override void Clear() { }
    protected override void Init()
    {
        base.Init();
        characterSpawn = new Vector3(52.0f, 2.0f, 0.0f);
        SceneType = Define.Scene.GameScene1;
        Managers.UI.ShowSceneUI<UI_GameScene>();
        Managers.Resource.Instantiate("Maps/stage1");
        Player = Managers.Resource.Instantiate("Character");
        Player.transform.position = characterSpawn;
        Managers.Sound.Play("Sounds/BGM/BGM", Define.Sound.BGM);
    }
    public void ReStart()
    {
        Managers.UI.ShowSceneUI<UI_GameScene>();
        Managers.Resource.Instantiate("Character", characterSpawn);
        Managers.Resource.Instantiate("Maps/stage1");
    }

    // Update is called once per frame
    void Update()
    {

    }
}

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
        EnemySpawnList.Add(new Vector3(41.7f, 7.1f, 0.0f));
        EnemySpawnList.Add(new Vector3(-13.3f, 22.7f, 0.0f));
        EnemySpawnList.Add(new Vector3(25.3f, 22.7f, 0.0f));
        EnemySpawnList.Add(new Vector3(-31.0f, 5.1f, 0.0f));
        EnemySpawnList.Add(new Vector3(4.9f, 5.1f, 0.0f));
        EnemySpawnList.Add(new Vector3(1, -9.1f, 0.0f));
        SceneType = Define.Scene.GameScene1;
        Managers.UI.ShowSceneUI<UI_GameScene>();
        Managers.Resource.Instantiate("Maps/stage1");
        Player = Managers.Resource.Instantiate("Character");
        Player.transform.position = characterSpawn;
        Managers.Resource.Instantiate("Enemy");
        for(int i=0; i<EnemySpawnList.Count;i++)
        {
            Managers.Resource.Instantiate("Enemy").transform.position = EnemySpawnList[i];
        }
        Managers.Sound.Play("Sounds/BGM/BGM", Define.Sound.BGM);
    }
    public void ReStart()
    {
        Managers.UI.ShowSceneUI<UI_GameScene>();
        Managers.Resource.Instantiate("Character", characterSpawn);
        Managers.Resource.Instantiate("Maps/stage1");
        Managers.Resource.Instantiate("Enemy");
    }

    // Update is called once per frame
    void Update()
    {

    }
}

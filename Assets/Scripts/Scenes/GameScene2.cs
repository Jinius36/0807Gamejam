using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene2: BaseScene
{
    [SerializeField] Vector3 characterSpawn;
    [SerializeField] List<Vector3> EnemySpawnList;   
    public override void Clear() { }
    protected override void Init()
    {
        base.Init();
        characterSpawn = new Vector3(52.0f, 2.0f, 0.0f);
        EnemySpawnList.Add(new Vector3(32.1f,5.3f,0.0f));
        EnemySpawnList.Add(new Vector3(32.1f,14.7f,0.0f));
        EnemySpawnList.Add(new Vector3(-15.9f,14.7f,0.0f)); 
        EnemySpawnList.Add(new Vector3(-40.7f,6.4f,0.0f));
        EnemySpawnList.Add(new Vector3(-18.9f,-11.0f,0.0f));
        EnemySpawnList.Add(new Vector3(-5.1f,9.9f,0.0f));
        SceneType = Define.Scene.GameScene2;
        Managers.UI.ShowSceneUI<UI_GameScene>();
        Managers.Resource.Instantiate("Maps/stage2");
        Managers.Resource.Instantiate("Character").transform.position = characterSpawn;
        Managers.Sound.Play("Sounds/BGM/BGM", Define.Sound.BGM);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene3 : BaseScene
{
    [SerializeField] Vector3 characterSpawn;
    [SerializeField] List<Vector3> EnemySpawnList;   
    public override void Clear() { }
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.GameScene3;
        characterSpawn = new Vector3(52.0f, 2.0f, 0.0f);
        EnemySpawnList.Add(new Vector3(-2.3f, 6.3f, 0.0f));
        EnemySpawnList.Add(new Vector3(-36.4f, 6.3f, 0.0f));
        EnemySpawnList.Add(new Vector3(-27.9f, 15.0f, 0.0f));
        EnemySpawnList.Add(new Vector3(-19.2f, -14.6f, 0.0f));
        EnemySpawnList.Add(new Vector3(23.9f, -22.1f, 0.0f));
        Managers.UI.ShowSceneUI<UI_GameScene>();
        Managers.Resource.Instantiate("Character").transform.position = characterSpawn;
        Managers.Resource.Instantiate("Maps/stage3");
        for(int i=0;i<EnemySpawnList.Count;i++){
            Managers.Resource.Instantiate("Enemy").transform.position = EnemySpawnList[i];
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

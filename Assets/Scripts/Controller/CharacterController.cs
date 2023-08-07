using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : BaseController
{
    [SerializeField] float speed;
    //[SerializeField] UI_GameScene gameScene;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    public override void Init()
    {
        WorldObjectType = Define.WorldObject.Player;
        //gameScene = GameObject.FindObjectOfType<UI_GameScene>();
    }
    protected override void UpdateIdle()
    {
        base.UpdateIdle();
    }
    protected override void UpdateRun()
    {
        base.UpdateRun();
    }
    protected override void UpdateWalk()
    {
        base.UpdateWalk();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        //if(gameScene.Horizontal !=0||gameScene.Vertical !=0)
        //{
        //    MoveControl();
        //}
    }
    //private void MoveControl()
    //{
    //    Vector3 upMovement = Vector3.up * speed * Time.deltaTime * gameScene.Vertical;
    //    Vector3 rightMovement = Vector3.right * speed * Time.deltaTime * gameScene.Horizontal;
    //    transform.position += upMovement;
    //    transform.position += rightMovement;
    //}

}

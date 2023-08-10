using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Sockets;
using UnityEngine;
using static EnemyController;

public class EnemyController : BaseController
{
    public enum Rotations
    {
        Right,
        Left,
        Up,
        Down
    }
    Vector3 prePosition;
    Rotations rotations;
    Transform light;
    void Start()
    {
        Init();
        light = transform.GetChild(0);
        prePosition = transform.position;
        rotations = Rotations.Left;
    }
    public override void Init()
    {
        WorldObjectType = Define.WorldObject.Enemy;
        State = Define.State.Walk;
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
    public void moveRight()
    {
        transform.localEulerAngles = new Vector3(0, -180, 0);
        light.SetLocalPositionAndRotation(new Vector3(-1.85f, -0.62f, 0), Quaternion.Euler(new Vector3(0, 0, -90)));
        transform.position += Vector3.right * Time.deltaTime;
    }
    public void moveLeft()
    {
        transform.localEulerAngles = new Vector3(0, 0, 0);
        light.SetLocalPositionAndRotation(new Vector3(-1.85f, -0.62f ,0), Quaternion.Euler(new Vector3(0, 0, -90)));
        transform.position += Vector3.left * Time.deltaTime;
    }
    // public void moveUp()
    // {
    //     light.SetLocalPositionAndRotation(new Vector3(0, -2.73f ,0), Quaternion.Euler(new Vector3(0, 0, 0)));
    //     transform.position += Vector3.up * Time.deltaTime;
    // }
    // public void moveDown()
    // {
    //     light.SetLocalPositionAndRotation(new Vector3(0, 2.73f ,0), Quaternion.Euler(new Vector3(0, 0, 0)));
    //     transform.position += Vector3.down * Time.deltaTime;
    //}
    // Update is called once per frame
    void Update()
    {
        switch (rotations)
        {
            case Rotations.Left:
                moveLeft(); break;
            case Rotations.Right:
                moveRight(); break;
            // case Rotations.Up:
            //     moveUp(); break;
            // case Rotations.Down:
            //     moveDown();break;
        }
        if((transform.position - prePosition).magnitude > 5)
        {
            switch (rotations)
            {
                case Rotations.Left:
                    prePosition = transform.position;
                    //rotations = Rotations.Down; break;
                    rotations = Rotations.Right; break;
                case Rotations.Right:
                    prePosition = transform.position;  
                    rotations = Rotations.Left; break;
                    //rotations = Rotations.Up; break;
                // case Rotations.Up:
                //     prePosition = transform.position;
                //     rotations = Rotations.Left; break;
                // case Rotations.Down:
                //     prePosition = transform.position;
                //     rotations = Rotations.Right; break;
            }
        }
    }
}

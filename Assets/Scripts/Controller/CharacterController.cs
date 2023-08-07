using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CharacterController : BaseController
{
    [SerializeField] public float speed;
    [SerializeField] UI_GameScene gameScene;
    [SerializeField] float time = 3f;
    [SerializeField] int life = 3;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    public override void Init()
    {
        WorldObjectType = Define.WorldObject.Player;
        gameScene = GameObject.FindObjectOfType<UI_GameScene>();
        life = 3;
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
        if (life <= 0)
        {
            Debug.Log("Die");
        }
    }
    private void FixedUpdate()
    {
        if (gameScene.Horizontal != 0 || gameScene.Vertical != 0)
        {
            MoveControl();
        }

    }
    private void MoveControl()
    {
        if(gameScene.Horizontal > 0)
        {
            transform.localEulerAngles = new Vector3(0, 0, -90);
        }
        else if(gameScene.Horizontal < 0)
        {
            transform.localEulerAngles = new Vector3(0, 0, 90);
        }
        Vector3 upmovement = Vector3.up * speed * Time.deltaTime * gameScene.Vertical;
        Vector3 rightmovement = Vector3.right * speed * Time.deltaTime * gameScene.Horizontal;
        transform.position += upmovement;
        transform.position += rightmovement;
    }
    IEnumerator ReturnSpeed()
    {
        yield return new WaitForSeconds(time);
        speed = 5;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Item0")
        {
            speed = 10;
            StartCoroutine(ReturnSpeed());
        }
        if(other.gameObject.tag == "Item1")
        {
            speed = 2;
            StartCoroutine(ReturnSpeed());
        }
        if(other.gameObject.tag == "Laser")
        {
            life--;
        }
        if(other.gameObject.tag == "Light")
        {
            life = 0;
        }
    }
}

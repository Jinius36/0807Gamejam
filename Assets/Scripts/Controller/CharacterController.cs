using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : BaseController
{
    [SerializeField] UI_Die die;
    public enum Items
    {
        Key1,
        Key2,
        Pen,
        Picture1,
        Picture2,
        USB,
        Paper1_1,
        Paper1_2,
        Paper2_1,
        Paper2_2,
    }
    [SerializeField] public float speed = 10;
    [SerializeField] UI_GameScene gameScene;
    [SerializeField] float time = 3f;
    [SerializeField] int life = 3;
    [SerializeField] Rigidbody2D rigidbody;
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
        rigidbody = GetComponent<Rigidbody2D>();
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
            if(die == null)
            {
                switch (DataManager.singleTon.saveData._currentStage)
                {
                    case 1:
                        Managers.Scene.LoadScene(Define.Scene.GameScene1);
                        break;
                    case 2:
                        Managers.Scene.LoadScene(Define.Scene.GameScene2);
                        break;
                    case 3:
                        Managers.Scene.LoadScene(Define.Scene.GameScene3);
                        break;
                }
            }
        }
    }
    private void FixedUpdate()
    {
        if (gameScene.Horizontal != 0 || gameScene.Vertical != 0)
        {
            MoveControl();
        }
        else
        {
            rigidbody.velocity = Vector2.zero;
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
            Managers.Resource.Destroy(other.gameObject);
            StartCoroutine(ReturnSpeed());
        }
        if(other.gameObject.tag == "Item1")
        {
            speed = 2;
            Managers.Resource.Destroy(other.gameObject);
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
        if(other.gameObject.tag == "Stairs")
        {
            if(DataManager.singleTon.saveData._currentStage == 1)
            {
                if (DataManager.singleTon.item.itemData[0].isGet == true
                    && DataManager.singleTon.item.itemData[1].isGet == true)
                {
                    DataManager.singleTon.saveData._currentStage = 2;
                    Managers.Scene.LoadScene(Define.Scene.GameScene2);
                }
            }
            else if(DataManager.singleTon.saveData._currentStage == 2)
            {
                if (DataManager.singleTon.item.itemData[2].isGet == true
                    && DataManager.singleTon.item.itemData[3].isGet == true
                    && DataManager.singleTon.item.itemData[4].isGet == true)
                {
                    DataManager.singleTon.saveData._currentStage = 3;
                    Managers.Scene.LoadScene(Define.Scene.GameScene3);
                }
            }
            else if (DataManager.singleTon.saveData._currentStage == 3)
            {
                if (DataManager.singleTon.item.itemData[5].isGet == true
                    && DataManager.singleTon.item.itemData[6].isGet == true
                    && DataManager.singleTon.item.itemData[7].isGet == true
                    && DataManager.singleTon.item.itemData[8].isGet == true
                    && DataManager.singleTon.item.itemData[9].isGet == true)
                {
                    Managers.Scene.LoadScene(Define.Scene.StartScene);
                }
            }
        }
        if(other.gameObject.tag == "Clue")
        {
            switch(other.gameObject.name) 
            {
                case "Key1":
                    DataManager.singleTon.item.itemData[(int)Items.Key1].isGet = true;
                    Managers.UI.ShowPopUpUI<UI_ItemGet>();
                    Time.timeScale = 0;
                    break;
                case "Key2":
                    DataManager.singleTon.item.itemData[(int)Items.Key2].isGet = true;
                    Managers.UI.ShowPopUpUI<UI_ItemGet>();
                    Time.timeScale = 0;
                    break;
                case "Pen":
                    DataManager.singleTon.item.itemData[(int)Items.Pen].isGet = true;
                    Managers.UI.ShowPopUpUI<UI_ItemGet>();
                    Time.timeScale = 0;
                    break;
                case "Picture1":
                    DataManager.singleTon.item.itemData[(int)Items.Picture1].isGet = true;
                    Managers.UI.ShowPopUpUI<UI_ItemGet>();
                    Time.timeScale = 0;
                    break;
                case "Picture2":
                    DataManager.singleTon.item.itemData[(int)Items.Picture2].isGet = true;
                    Managers.UI.ShowPopUpUI<UI_ItemGet>();
                    break;
                case "USB":
                    DataManager.singleTon.item.itemData[(int)Items.USB].isGet = true;
                    Managers.UI.ShowPopUpUI<UI_ItemGet>();
                    Time.timeScale = 0;
                    break;
                case "Paper1_1":
                    DataManager.singleTon.item.itemData[(int)Items.Paper1_1].isGet = true;
                    Managers.UI.ShowPopUpUI<UI_ItemGet>();
                    Time.timeScale = 0;
                    break;
                case "Paper1_2":
                    DataManager.singleTon.item.itemData[(int)Items.Paper1_2].isGet = true;
                    Managers.UI.ShowPopUpUI<UI_ItemGet>();
                    Time.timeScale = 0;
                    break;
                case "Paper2_1":
                    DataManager.singleTon.item.itemData[(int)Items.Paper2_1].isGet = true;
                    Managers.UI.ShowPopUpUI<UI_ItemGet>();
                    Time.timeScale = 0;
                    break;
                case "Paper2_2":
                    DataManager.singleTon.item.itemData[(int)Items.Paper2_2].isGet = true;
                    Managers.UI.ShowPopUpUI<UI_ItemGet>();
                    Time.timeScale = 0;
                    break;
            }
            Managers.Resource.Destroy(other.gameObject);
        }
    }
}

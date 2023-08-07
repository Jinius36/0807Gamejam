using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : BaseController
{
    public enum Items
    {
        Cctv1,
        Cctv2,
        Cctv3,
        Key,
        Pen,
        Picture1,
        Picture2,
        USB,
        Paper1,
        Paper2,
        Sausage,
        Milk
    }
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
                    && DataManager.singleTon.item.itemData[1].isGet == true
                    && DataManager.singleTon.item.itemData[2].isGet == true
                    && DataManager.singleTon.item.itemData[3].isGet == true)
                {
                    Managers.UI.ShowSceneUI<UI_Clear>();
                }
            }
            else if(DataManager.singleTon.saveData._currentStage == 2)
            {
                if (DataManager.singleTon.item.itemData[4].isGet == true
                    && DataManager.singleTon.item.itemData[5].isGet == true
                    && DataManager.singleTon.item.itemData[6].isGet == true)
                {
                    Managers.UI.ShowSceneUI<UI_Clear>();
                }
            }
            else if (DataManager.singleTon.saveData._currentStage == 3)
            {
                if (DataManager.singleTon.item.itemData[7].isGet == true
                    && DataManager.singleTon.item.itemData[8].isGet == true
                    && DataManager.singleTon.item.itemData[9].isGet == true)
                {
                    Managers.UI.ShowSceneUI<UI_Clear>();
                }
            }
        }
        if(other.gameObject.tag == "Clue")
        {
            switch(other.gameObject.name) 
            {
                case "Cctv1":
                    DataManager.singleTon.item.itemData[(int)Items.Cctv1].isGet = true;
                    break;
                case "Cctv2":
                    DataManager.singleTon.item.itemData[(int)Items.Cctv2].isGet = true;
                    break;
                case "Cctv3":
                    DataManager.singleTon.item.itemData[(int)Items.Cctv3].isGet = true;
                    break;
                case "Key":
                    DataManager.singleTon.item.itemData[(int)Items.Key].isGet = true;
                    break;
                case "Pen":
                    DataManager.singleTon.item.itemData[(int)Items.Pen].isGet = true;
                    break;
                case "Picture1":
                    DataManager.singleTon.item.itemData[(int)Items.Picture1].isGet = true;
                    break;
                case "Picture2":
                    DataManager.singleTon.item.itemData[(int)Items.Picture2].isGet = true;
                    break;
                case "USB":
                    DataManager.singleTon.item.itemData[(int)Items.USB].isGet = true;
                    break;
                case "Paper1":
                    DataManager.singleTon.item.itemData[(int)Items.Paper1].isGet = true;
                    break;
                case "Paper2":
                    DataManager.singleTon.item.itemData[(int)Items.Paper2].isGet = true;
                    break;
                case "Sausage":
                    DataManager.singleTon.item.itemData[(int)Items.Sausage].isGet = true;
                    break;
                case "Milk":
                    DataManager.singleTon.item.itemData[(int)Items.Milk].isGet = true;
                    break;
            }
            Managers.Resource.Destroy(other.gameObject);
        }
    }
}

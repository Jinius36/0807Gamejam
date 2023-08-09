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
    [SerializeField] public float normalSpeed = 10;
    [SerializeField] float time = 3f;
    [SerializeField] int life = 3;
    [SerializeField] new Rigidbody2D rigidbody;
    [SerializeField] VariableJoystick joystick;
    [SerializeField] Vector2 moveVec;
    float x;
    float y;

    public Rigidbody2D Rigidbody { get => rigidbody; set => rigidbody = value; }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    public override void Init()
    {
        WorldObjectType = Define.WorldObject.Player;
        life = 3;
        Rigidbody = GetComponent<Rigidbody2D>();
        joystick = FindAnyObjectByType<VariableJoystick>();
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
        Rigidbody.MovePosition(Rigidbody.position + moveVec);

        // #. No input = No Rotation
        if (x < 0)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.localEulerAngles = new Vector3(0, -180, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        x = joystick.Horizontal;
        y = joystick.Vertical;
        moveVec = new Vector2(x, y) * speed * Time.deltaTime;
        if (moveVec.sqrMagnitude != 0)
        {
            State = Define.State.Walk;
        }
        if (moveVec.sqrMagnitude == 0)
        {
            State = Define.State.Idle;
        }
        switch (State)
        {
            case Define.State.Idle:
                UpdateIdle();
                break;
            case Define.State.Walk:
                UpdateWalk();
                break;
            case Define.State.Run:
                UpdateRun();
                break;
        }
        if (life <= 0)
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
    private void FixedUpdate()
    {
        //State = Define.State.Walk;
        //// 1. Input Value
        //float x = joystick.Horizontal;
        //float y = joystick.Vertical;

        //// 2. Move Position 
        //moveVec = new Vector2(x, y) * speed * Time.fixedDeltaTime;
        //Rigidbody.MovePosition(Rigidbody.position + moveVec);
        //State = Define.State.Walk;
        //if (moveVec.sqrMagnitude == 0)
        //{
        //    State = Define.State.Idle;
        //    return;
        //}
        //// #. No input = No Rotation
        //if (x < 0)
        //{
        //    transform.localEulerAngles = new Vector3(0, 0, 0);
        //}
        //else
        //{
        //    transform.localEulerAngles = new Vector3(0, -180, 0);
        //}
    }
    IEnumerator ReturnSpeed()
    {
        yield return new WaitForSeconds(time);
        State = Define.State.Walk;
        speed = normalSpeed;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Item0")
        {
            Managers.Sound.Play("Sounds/SFX/5_yumyum", Define.Sound.SFX);
            Managers.UI.ShowPopUpUI<UI_ItemGet>().transform.GetChild(0).GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("UI/SoddeokUI");
            Time.timeScale = 0.0f;
            speed = speed * 2;
            State = Define.State.Run;
            Managers.Resource.Destroy(other.gameObject);
            StartCoroutine(ReturnSpeed());
        }
        if(other.gameObject.tag == "Item1")
        {
            Managers.Sound.Play("Sounds/SFX/3_drinkMilk", Define.Sound.SFX);
            Managers.UI.ShowPopUpUI<UI_ItemGet>().transform.GetChild(0).GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("UI/Milk");
            Time.timeScale = 0.0f;
            speed = speed / 2;
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
                    Managers.Sound.Play("Sounds/SFX/6_gain", Define.Sound.SFX);
                    DataManager.singleTon.item.itemData[(int)Items.Key1].isGet = true;
                    Managers.UI.ShowPopUpUI<UI_ItemGet>().transform.GetChild(1).GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("UI/Key1");
                    Time.timeScale = 0;
                    break;
                case "Key2":
                    Managers.Sound.Play("Sounds/SFX/6_gain", Define.Sound.SFX); 
                    DataManager.singleTon.item.itemData[(int)Items.Key2].isGet = true;
                    Managers.UI.ShowPopUpUI<UI_ItemGet>().transform.GetChild(1).GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("UI/Key2");
                    Time.timeScale = 0;
                    break;
                case "Pen":
                    Managers.Sound.Play("Sounds/SFX/6_gain", Define.Sound.SFX);
                    DataManager.singleTon.item.itemData[(int)Items.Pen].isGet = true;
                    Managers.UI.ShowPopUpUI<UI_ItemGet>().transform.GetChild(1).GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("UI/Pen");
                    Time.timeScale = 0;
                    break;
                case "Picture1":
                    Managers.Sound.Play("Sounds/SFX/6_gain", Define.Sound.SFX);
                    DataManager.singleTon.item.itemData[(int)Items.Picture1].isGet = true;
                    Managers.UI.ShowPopUpUI<UI_ItemGet>().transform.GetChild(1).GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("UI/Picture1");
                    Time.timeScale = 0;
                    break;
                case "Picture2":
                    Managers.Sound.Play("Sounds/SFX/6_gain", Define.Sound.SFX);
                    DataManager.singleTon.item.itemData[(int)Items.Picture2].isGet = true;
                    Managers.UI.ShowPopUpUI<UI_ItemGet>().transform.GetChild(1).GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("UI/Picture2");
                    break;
                case "USB":
                    Managers.Sound.Play("Sounds/SFX/6_gain", Define.Sound.SFX);
                    DataManager.singleTon.item.itemData[(int)Items.USB].isGet = true;
                    Managers.UI.ShowPopUpUI<UI_ItemGet>().transform.GetChild(1).GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("UI/USB");
                    Time.timeScale = 0;
                    break;
                case "Paper1_1":
                    Managers.Sound.Play("Sounds/SFX/6_gain", Define.Sound.SFX);
                    DataManager.singleTon.item.itemData[(int)Items.Paper1_1].isGet = true;
                    Managers.UI.ShowPopUpUI<UI_ItemGet>().transform.GetChild(1).GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("UI/Paper1_1");
                    Time.timeScale = 0;
                    break;
                case "Paper1_2":
                    Managers.Sound.Play("Sounds/SFX/6_gain", Define.Sound.SFX);
                    DataManager.singleTon.item.itemData[(int)Items.Paper1_2].isGet = true;
                    Managers.UI.ShowPopUpUI<UI_ItemGet>().transform.GetChild(1).GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("UI/Paper1_2");
                    Time.timeScale = 0;
                    break;
                case "Paper2_1":
                    Managers.Sound.Play("Sounds/SFX/6_gain", Define.Sound.SFX);                
                    DataManager.singleTon.item.itemData[(int)Items.Paper2_1].isGet = true;
                    Managers.UI.ShowPopUpUI<UI_ItemGet>().transform.GetChild(1).GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("UI/Paper2_1");
                    Time.timeScale = 0;
                    break;
                case "Paper2_2":
                    Managers.Sound.Play("Sounds/SFX/6_gain", Define.Sound.SFX);
                    DataManager.singleTon.item.itemData[(int)Items.Paper2_2].isGet = true;
                    Managers.UI.ShowPopUpUI<UI_ItemGet>().transform.GetChild(1).GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("UI/Paper2_2");
                    Time.timeScale = 0;
                    break;
            }
            Managers.Resource.Destroy(other.gameObject);
        }
    }
}

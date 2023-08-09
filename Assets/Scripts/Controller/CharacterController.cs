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
    [SerializeField] public Animator anim;
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
        anim = this.gameObject.GetComponent<Animator>();
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
    //private void FixedUpdate()
    //{
    //    if (gameScene.Horizontal != 0 || gameScene.Vertical != 0)
    //    {
    //        MoveControl();
    //    }
    //    else
    //    {
    //        rigidbody.velocity = Vector2.zero;
    //        anim.SetBool("isWalk", false);
    //    }
    //}
    public void Move(Vector2 inputDirection)
    {
        Vector2 moveInput = inputDirection;
        bool isMove = moveInput.magnitude != 0;
        anim.SetBool("isWalk", isMove);
        if (isMove)
        {
            Vector3 movDir = Vector3.up * moveInput.y + Vector3.right * moveInput.x ;
            transform.position += movDir * Time.deltaTime * speed;
        }
    }
    public void Look(Vector2 inputDirection)
    {
        Vector2 lookInput = inputDirection;
        if(lookInput.x < 0)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.localEulerAngles = new Vector3(0, -180, 0);
        }
    }
    //private void MoveControl()
    //{
    //    anim.SetBool("isWalk", true);
    //    Managers.Sound.Play("Sounds/SFX/4_walking", Define.Sound.SFX);
    //    if(gameScene.Horizontal > 0)
    //    {
    //        transform.localEulerAngles = new Vector3(0, -180, 0);
    //    }
    //    else if(gameScene.Horizontal < 0)
    //    {
    //        transform.localEulerAngles = new Vector3(0, 0, 0);
    //    }
    //    Vector3 upmovement = Vector3.up * speed * Time.deltaTime * gameScene.Vertical;
    //    Vector3 rightmovement = Vector3.right * speed * Time.deltaTime * gameScene.Horizontal;
    //    transform.position += upmovement;
    //    transform.position += rightmovement;
    //}
    IEnumerator ReturnSpeed()
    {
        yield return new WaitForSeconds(time);
        anim.SetBool("isRun", false);
        speed = 10;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Item0")
        {
            Managers.Sound.Play("Sounds/SFX/5_yumyum", Define.Sound.SFX);
            Managers.UI.ShowPopUpUI<UI_ItemGet>().transform.GetChild(0).GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("UI/SoddeokUI");
            Time.timeScale = 0.0f;
            speed = 20;
            anim.SetBool("isRun", true);
            Managers.Resource.Destroy(other.gameObject);
            StartCoroutine(ReturnSpeed());
        }
        if(other.gameObject.tag == "Item1")
        {
            Managers.Sound.Play("Sounds/SFX/3_drinkMilk", Define.Sound.SFX);
            Managers.UI.ShowPopUpUI<UI_ItemGet>().transform.GetChild(0).GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("UI/Milk");
            Time.timeScale = 0.0f;
            speed = 5;
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

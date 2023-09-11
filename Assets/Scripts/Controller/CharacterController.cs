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
        Paper2_2
    }
    [SerializeField] public float speed = 5;
    [SerializeField] public float normalSpeed = 5;
    [SerializeField] public float runSpeed;
    [SerializeField] float time = 3f;
    [SerializeField] int life = 3;
    [SerializeField] new Rigidbody2D rigidbody;
    [SerializeField] VariableJoystick joystick;
    [SerializeField] Vector2 moveVec;
    [SerializeField] Stage1 stage1;
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
        stage1 = FindObjectOfType<Stage1>();
        runSpeed = normalSpeed * 1.75f;
    }
    protected override void UpdateIdle()
    {
        base.UpdateIdle();
    }
    protected override void UpdateRun()
    {
        base.UpdateRun();
        Rigidbody.MovePosition(Rigidbody.position + moveVec);
        if (x < 0)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.localEulerAngles = new Vector3(0, -180, 0);
        }
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
    public void WalkSound()
    {
        Managers.Sound.Play("Sounds/SFX/4_walking", Define.Sound.SFX);
    }
    public void RunSound()
    {
        Managers.Sound.Play("Sounds/SFX/2_dash", Define.Sound.SFX);
    }
    // Update is called once per frame
    void Update()
    {
        x = joystick.Horizontal;
        y = joystick.Vertical;
        moveVec = new Vector2(x, y) * speed * Time.deltaTime;
        if (moveVec.sqrMagnitude != 0)
        {
            if(State != Define.State.Run)
            {
                State = Define.State.Walk;
            }
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
            StartCoroutine(Die());
            for(int i=0; i< DataManager.singleTon.item.itemData.Count; i++)
            {
                DataManager.singleTon.item.itemData[i].isGet = false;
            }
        }
    }
    private void FixedUpdate()
    {
    }
    IEnumerator ReturnSpeed()
    {
        yield return new WaitForSeconds(time);
        State = Define.State.Walk;
        speed = normalSpeed;
    }
    IEnumerator Die()
    {
        switch (DataManager.singleTon.saveData._currentStage)
        {
            case 1:
                Managers.UI.ShowSceneUI<UI_Die>();
                yield return new WaitForSeconds(time);
                Managers.Scene.LoadScene(Define.Scene.GameScene1);
                break;
            case 2:
                Managers.UI.ShowSceneUI<UI_Die>();
                yield return new WaitForSeconds(time);
                Managers.Scene.LoadScene(Define.Scene.GameScene2);
                break;
            case 3:
                Managers.UI.ShowSceneUI<UI_Die>();
                yield return new WaitForSeconds(time);
                Managers.Scene.LoadScene(Define.Scene.GameScene3);
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "PC":
                for (int i = 0; i < stage1.PC.Length; i++)
                {
                    stage1.PC[i].GetComponent<SpriteRenderer>().sprite = Managers.Resource.Load<Sprite>("ItemIcon/PC_Off");
                }
                break;
            case "Item0":
                Managers.Sound.Play("Sounds/SFX/5_yumyum", Define.Sound.SFX);
                Managers.UI.ShowPopUpUI<UI_ItemGet>().transform.GetChild(1).GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("UI/SoddeokUI");
                Time.timeScale = 0.0f;
                speed = runSpeed;
                State = Define.State.Run;
                Managers.Resource.Destroy(other.gameObject);
                StartCoroutine(ReturnSpeed());
                break;
            case "Item1":
                Managers.Sound.Play("Sounds/SFX/3_drinkMilk", Define.Sound.SFX);
                Managers.UI.ShowPopUpUI<UI_ItemGet>().transform.GetChild(1).GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("UI/Milk");
                Time.timeScale = 0.0f;
                speed = normalSpeed / 1.75f;
                Managers.Resource.Destroy(other.gameObject);
                StartCoroutine(ReturnSpeed());
                break;
            case "Laser":
                life--;
                Managers.Sound.Play("Sounds/SFX/8_warning");
                break;
            case "Light":
                life = 0;
                break;
            case "Stairs":
                if (DataManager.singleTon.saveData._currentStage == 1)
                {
                    if (DataManager.singleTon.item.itemData[(int)Items.Key1].isGet
                        && DataManager.singleTon.item.itemData[(int)Items.Key2].isGet)
                    {
                        DataManager.singleTon.saveData._currentStage = 2;
                        Managers.Scene.LoadScene(Define.Scene.GameScene2);
                    }
                }
                else if (DataManager.singleTon.saveData._currentStage == 2)
                {
                    if (DataManager.singleTon.item.itemData[(int)Items.Pen].isGet
                        && DataManager.singleTon.item.itemData[(int)Items.Picture1].isGet
                        && DataManager.singleTon.item.itemData[(int)Items.Picture1].isGet)
                    {
                        DataManager.singleTon.saveData._currentStage = 3;
                        Managers.Scene.LoadScene(Define.Scene.GameScene3);
                    }
                }
                else if (DataManager.singleTon.saveData._currentStage == 3)
                {
                    if (DataManager.singleTon.item.itemData[(int)Items.USB].isGet
                        && DataManager.singleTon.item.itemData[(int)Items.Paper1_1].isGet
                        && DataManager.singleTon.item.itemData[(int)Items.Paper1_2].isGet
                        && DataManager.singleTon.item.itemData[(int)Items.Paper2_1].isGet
                        && DataManager.singleTon.item.itemData[(int)Items.Paper2_2].isGet)
                    {
                        Managers.UI.ShowPopUpUI<UI_Ending>();
                    }
                }
                break;
            case "Clue":
                switch (other.gameObject.name)
                {
                    case "Key1":
                        if (DataManager.singleTon.item.itemData[(int)Items.Key2].isGet)
                        {
                            Managers.Sound.Play("Sounds/SFX/7_allGain", Define.Sound.SFX);
                        }
                        Managers.Sound.Play("Sounds/SFX/6_gain", Define.Sound.SFX);
                        DataManager.singleTon.item.itemData[(int)Items.Key1].isGet = true;
                        Managers.UI.ShowPopUpUI<UI_ItemGet>().transform.GetChild(1).GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("UI/Key1");
                        Time.timeScale = 0;
                        break;
                    case "Key2":
                        if (DataManager.singleTon.item.itemData[(int)Items.Key1].isGet)
                        {
                            Managers.Sound.Play("Sounds/SFX/7_allGain", Define.Sound.SFX);
                        }
                        Managers.Sound.Play("Sounds/SFX/6_gain", Define.Sound.SFX);
                        DataManager.singleTon.item.itemData[(int)Items.Key2].isGet = true;
                        Managers.UI.ShowPopUpUI<UI_ItemGet>().transform.GetChild(1).GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("UI/Key2");
                        Time.timeScale = 0;
                        break;
                    case "Pen":
                        if (DataManager.singleTon.item.itemData[(int)Items.Picture1].isGet && DataManager.singleTon.item.itemData[(int)Items.Picture2].isGet)
                        {
                            Managers.Sound.Play("Sounds/SFX/7_allGain", Define.Sound.SFX);
                        }
                        Managers.Sound.Play("Sounds/SFX/6_gain", Define.Sound.SFX);
                        DataManager.singleTon.item.itemData[(int)Items.Pen].isGet = true;
                        Managers.UI.ShowPopUpUI<UI_ItemGet>().transform.GetChild(1).GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("UI/Pen");
                        Time.timeScale = 0;
                        break;
                    case "Picture1":
                        if (DataManager.singleTon.item.itemData[(int)Items.Pen].isGet && DataManager.singleTon.item.itemData[(int)Items.Picture2].isGet)
                        {
                            Managers.Sound.Play("Sounds/SFX/7_allGain", Define.Sound.SFX);
                        }
                        Managers.Sound.Play("Sounds/SFX/6_gain", Define.Sound.SFX);
                        DataManager.singleTon.item.itemData[(int)Items.Picture1].isGet = true;
                        Managers.UI.ShowPopUpUI<UI_ItemGet>().transform.GetChild(1).GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("UI/Picture1");
                        Time.timeScale = 0;
                        break;
                    case "Picture2":
                        if (DataManager.singleTon.item.itemData[(int)Items.Pen].isGet && DataManager.singleTon.item.itemData[(int)Items.Picture1].isGet)
                        {
                            Managers.Sound.Play("Sounds/SFX/7_allGain", Define.Sound.SFX);
                        }
                        Managers.Sound.Play("Sounds/SFX/6_gain", Define.Sound.SFX);
                        DataManager.singleTon.item.itemData[(int)Items.Picture2].isGet = true;
                        Managers.UI.ShowPopUpUI<UI_ItemGet>().transform.GetChild(1).GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("UI/Picture2");
                        Time.timeScale = 0;
                        break;
                    case "USB":
                        if (DataManager.singleTon.item.itemData[(int)Items.Paper1_1].isGet && DataManager.singleTon.item.itemData[(int)Items.Paper1_2].isGet
                            && DataManager.singleTon.item.itemData[(int)Items.Paper2_1].isGet && DataManager.singleTon.item.itemData[(int)Items.Paper2_2].isGet)
                        {
                            Managers.Sound.Play("Sounds/SFX/7_allGain", Define.Sound.SFX);
                        }
                        Managers.Sound.Play("Sounds/SFX/6_gain", Define.Sound.SFX);
                        DataManager.singleTon.item.itemData[(int)Items.USB].isGet = true;
                        Managers.UI.ShowPopUpUI<UI_ItemGet>().transform.GetChild(1).GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("UI/USB");
                        Time.timeScale = 0;
                        break;
                    case "Paper1_1":
                        if (DataManager.singleTon.item.itemData[(int)Items.USB].isGet && DataManager.singleTon.item.itemData[(int)Items.Paper1_2].isGet
                            && DataManager.singleTon.item.itemData[(int)Items.Paper2_1].isGet && DataManager.singleTon.item.itemData[(int)Items.Paper2_2].isGet)
                        {
                            Managers.Sound.Play("Sounds/SFX/7_allGain", Define.Sound.SFX);
                        }
                        Managers.Sound.Play("Sounds/SFX/6_gain", Define.Sound.SFX);
                        DataManager.singleTon.item.itemData[(int)Items.Paper1_1].isGet = true;
                        Managers.UI.ShowPopUpUI<UI_ItemGet>().transform.GetChild(1).GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("UI/Paper1_1");
                        Time.timeScale = 0;
                        break;
                    case "Paper1_2":
                        if (DataManager.singleTon.item.itemData[(int)Items.Paper1_1].isGet && DataManager.singleTon.item.itemData[(int)Items.USB].isGet
                            && DataManager.singleTon.item.itemData[(int)Items.Paper2_1].isGet && DataManager.singleTon.item.itemData[(int)Items.Paper2_2].isGet)
                        {
                            Managers.Sound.Play("Sounds/SFX/7_allGain", Define.Sound.SFX);
                        }
                        Managers.Sound.Play("Sounds/SFX/6_gain", Define.Sound.SFX);
                        DataManager.singleTon.item.itemData[(int)Items.Paper1_2].isGet = true;
                        Managers.UI.ShowPopUpUI<UI_ItemGet>().transform.GetChild(1).GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("UI/Paper1_2");
                        Time.timeScale = 0;
                        break;
                    case "Paper2_1":
                        if (DataManager.singleTon.item.itemData[(int)Items.Paper1_1].isGet && DataManager.singleTon.item.itemData[(int)Items.Paper1_2].isGet
                            && DataManager.singleTon.item.itemData[(int)Items.USB].isGet && DataManager.singleTon.item.itemData[(int)Items.Paper2_2].isGet)
                        {
                            Managers.Sound.Play("Sounds/SFX/7_allGain", Define.Sound.SFX);
                        }
                        Managers.Sound.Play("Sounds/SFX/6_gain", Define.Sound.SFX);
                        DataManager.singleTon.item.itemData[(int)Items.Paper2_1].isGet = true;
                        Managers.UI.ShowPopUpUI<UI_ItemGet>().transform.GetChild(1).GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("UI/Paper2_1");
                        Time.timeScale = 0;
                        break;
                    case "Paper2_2":
                        if (DataManager.singleTon.item.itemData[(int)Items.Paper1_1].isGet && DataManager.singleTon.item.itemData[(int)Items.Paper1_2].isGet
                            && DataManager.singleTon.item.itemData[(int)Items.Paper2_1].isGet && DataManager.singleTon.item.itemData[(int)Items.USB].isGet)
                        {
                            Managers.Sound.Play("Sounds/SFX/7_allGain", Define.Sound.SFX);
                        }
                        Managers.Sound.Play("Sounds/SFX/6_gain", Define.Sound.SFX);
                        DataManager.singleTon.item.itemData[(int)Items.Paper2_2].isGet = true;
                        Managers.UI.ShowPopUpUI<UI_ItemGet>().transform.GetChild(1).GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("UI/Paper2_2");
                        Time.timeScale = 0;
                        break;
                }
                Managers.Resource.Destroy(other.gameObject);
                break;
        }
    }
}

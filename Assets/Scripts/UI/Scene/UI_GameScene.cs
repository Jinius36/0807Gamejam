using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UI_StartScene;

public class UI_GameScene : UI_Scene
{
    public enum Images
    {
        Panel
    }
    public enum Buttons
    {
        Run,
        Setting
    }
    private bool canRun;
    public Image image;
    [SerializeField] GameObject character;
    [SerializeField] CharacterController characterController;
    Vector3 input = Vector3.zero;
    public float Horizontal { get { return input.x; } }
    public float Vertical { get { return input.y; } }
    public Vector3 Input {  get { return input; } }
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();
        Bind<Image>(typeof(Images));
        Bind<Button>(typeof(Buttons));
        image = GetImage((int)Images.Panel);
        GetButton((int)Buttons.Run).gameObject.AddUIEvent(Run);
        GetButton((int)Buttons.Setting).gameObject.AddUIEvent(SettingClick);
        character = FindObjectOfType<CharacterController>().gameObject;
        characterController = character.GetComponent<CharacterController>();
        StartCoroutine(FadeCoroutine());
        canRun = true;
    }
    IEnumerator FadeCoroutine()
    {
        float fadeCount = 1.0f;
        while (fadeCount > 0)
        {
            fadeCount -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            image.color = new Color(0, 0, 0, fadeCount);
        }
        Managers.Resource.Destroy(image.gameObject);
    }
    IEnumerator ReturnSpeed()
    {
        yield return new WaitForSeconds(5f);
        characterController.State = Define.State.Walk;
        characterController.speed = characterController.normalSpeed;
        yield return new WaitForSeconds(10f);
        canRun = true;
    }
    void Run(PointerEventData eventData)
    {
        if (!canRun)
        {
            return;
        }
        Managers.Sound.Play("Sounds/SFX/2_dash");
        characterController.speed *= 2;
        characterController.State = Define.State.Run;
        canRun = false;
        StartCoroutine(ReturnSpeed());
    }
    void SettingClick(PointerEventData eventData)
    {
        Managers.UI.ShowPopUpUI<UI_Setting_1>();
        Time.timeScale = 0;
    }
    void Update()
    {
    }
}

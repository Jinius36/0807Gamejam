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
        JoyStickBack,
        JoyStick
    }
    public enum Buttons
    {
        Run
    }
    RectTransform m_rectBack;
    RectTransform m_rectJoystick;
    Image joyStick;
    [SerializeField] GameObject character;
    [SerializeField] CharacterController characterController;
    float m_fRadius;
    float deadZone = 0f;
    float handlerRange = 1;
    Vector3 input = Vector3.zero;
    Canvas canvas;
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
        m_rectBack = GetImage((int)Images.JoyStickBack).transform.GetComponent<RectTransform>();
        joyStick = GetImage((int)Images.JoyStick);
        m_rectJoystick = joyStick.transform.GetComponent<RectTransform>();
        GetButton((int)Buttons.Run).gameObject.AddUIEvent(Run);
        character = FindObjectOfType<CharacterController>().gameObject;
        characterController = character.GetComponent<CharacterController>();
        joyStick.gameObject.AddUIEvent(OnBeginDrag, Define.UIEvent.BeginDrag);
        joyStick.gameObject.AddUIEvent(OnDrag, Define.UIEvent.Drag);
        joyStick.gameObject.AddUIEvent(OnPointerUP, Define.UIEvent.DragEnd);
        m_fRadius = m_rectBack.rect.width * 0.5f;
        canvas = this.GetComponent<Canvas>();
    }
    void Run(PointerEventData eventData)
    {
        characterController.speed = 10;
    }
    void OnBeginDrag(PointerEventData eventData)
    {
        input = (eventData.position - m_rectBack.anchoredPosition) / (m_fRadius * canvas.scaleFactor);
        HandleInput(input.magnitude, input.normalized);
        m_rectJoystick.anchoredPosition = input * m_fRadius * handlerRange;
        //character.transform.eulerAngles = new Vector3(0f, 0f, Mathf.Atan2(-input.normalized.x, input.normalized.y) * Mathf.Rad2Deg);
    }
    void OnDrag(PointerEventData eventData)
    {
        input = (eventData.position - m_rectBack.anchoredPosition) / (m_fRadius * canvas.scaleFactor);
        HandleInput(input.magnitude, input.normalized);
        m_rectJoystick.anchoredPosition = input * m_fRadius * handlerRange;
        //character.transform.eulerAngles = new Vector3(0f, 0f, Mathf.Atan2(-input.normalized.x, input.normalized.y) * Mathf.Rad2Deg);
    }
    void HandleInput(float magnitude, Vector2 normalized)
    {
        if (magnitude > deadZone)
        {
            if (magnitude > 1)
            {
                input = normalized;
            }
            else
            {
                input = Vector2.zero;
            }
        }
    }
    void OnPointerUP(PointerEventData eventData)
    {
        input = Vector2.zero;
        m_rectJoystick.anchoredPosition = Vector2.zero;
    }
    void Update()
    {
    }
}

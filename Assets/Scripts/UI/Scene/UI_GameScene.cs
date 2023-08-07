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
    float m_fRadius;
    bool m_bTouch = false;
    float m_fSpeed = 5.0f;
    float m_fSqr = 0f;
    Vector3 m_vecMove;
    [SerializeField, Range(10f, 150f)]
    private float leverRange;   // 추가
    Vector2 m_vecNormal;
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
        //GetButton((int)Buttons.Run).gameObject.AddUIEvent(Run);
        character = FindObjectOfType<CharacterController>().gameObject;
        joyStick.gameObject.AddUIEvent(OnBeginDrag, Define.UIEvent.BeginDrag);
        joyStick.gameObject.AddUIEvent(OnDrag, Define.UIEvent.Drag);
        joyStick.gameObject.AddUIEvent(OnPointerUP, Define.UIEvent.DragEnd);
        m_fRadius = m_rectBack.rect.width * 0.5f;
        canvas = this.GetComponent<Canvas>();
    }
    void OnBeginDrag(PointerEventData eventData)
    {
        Vector2 radius = m_rectBack.sizeDelta;
        input = (eventData.position - m_rectBack.anchoredPosition) / (m_fRadius * canvas.scaleFactor);
        HandleInput(input.magnitude, input.normalized);
        m_rectJoystick.anchoredPosition = input * m_fRadius * handlerRange;
        character.transform.eulerAngles = new Vector3(0f, 0f, Mathf.Atan2(input.normalized.x, -input.normalized.y) * Mathf.Rad2Deg);
    }
    void OnDrag(PointerEventData eventData)
    {
        Vector2 radius = m_rectBack.sizeDelta;
        input = (eventData.position - m_rectBack.anchoredPosition) / (m_fRadius * canvas.scaleFactor);
        HandleInput(input.magnitude, input.normalized);
        m_rectJoystick.anchoredPosition = input * m_fRadius * handlerRange;
        character.transform.eulerAngles = new Vector3(0f, 0f, Mathf.Atan2(-input.normalized.x, input.normalized.y) * Mathf.Rad2Deg);
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
    //void OnTouch(Vector3 vecTouch)
    //{
    //    Vector3 vec = new Vector3(vecTouch.x - m_rectBack.position.x, vecTouch.y - m_rectBack.position.y, 0f);

    //    // vec값을 m_fRadius 이상이 되지 않도록 합니다.
    //    vec = Vector3.ClampMagnitude(vec, m_fRadius);
    //    m_rectJoystick.localPosition = vec;

    //    // 조이스틱 배경과 조이스틱과의 거리 비율로 이동합니다.
    //    float fSqr = (m_rectBack.position - m_rectJoystick.position).sqrMagnitude / (m_fRadius * m_fRadius);

    //    // 터치위치 정규화
    //    Vector3 vecNormal = vec.normalized;

    //    m_vecMove = new Vector3(vecNormal.x * m_fSpeed * Time.deltaTime * fSqr * 10, vecNormal.y * m_fSpeed * Time.deltaTime * fSqr * 10, 0f);
    //    character.transform.eulerAngles = new Vector3(0f, 0f, Mathf.Atan2(vecNormal.x, vecNormal.y) * Mathf.Rad2Deg);
    //}
    //public void Run(PointerEventData eventData)
    //{
    //    OnTouch(eventData.position);
    //    m_bTouch = true;
    //}
    //public void OnDrag(PointerEventData eventData)
    //{
    //    OnTouch(eventData.position);
    //    m_bTouch = true;
    //}

    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    m_bTouch = true;
    //}

    //public void OnPointerUp(PointerEventData eventData)
    //{
    //    // 원래 위치로 되돌립니다.
    //    m_rectJoystick.localPosition = Vector3.zero;
    //    m_bTouch = false;
    //}
    // Update is called once per frame
    void Update()
    {
        if (m_bTouch)
        {
            character.transform.position += m_vecMove;
        }

    }
}

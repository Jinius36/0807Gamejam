using System.Collections;
using System.Collections.Generic;
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
    float m_fSpeed = 5.0f;
    float m_fsqr = 0f;
    bool m_bTouch = false;
    Vector3 m_vecMove;

    Vector2 m_vecNormal;
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
        m_rectBack = GetImage((int)Images.JoyStickBack).gameObject.GetComponent<RectTransform>();
        joyStick = GetImage((int)Images.JoyStick);
        m_rectJoystick = joyStick.gameObject.GetComponent<RectTransform>();
        GetButton((int)Buttons.Run).gameObject.AddUIEvent(Run);
        character = FindObjectOfType<CharacterController>().gameObject;
        joyStick.gameObject.AddUIEvent(OnDrag, Define.UIEvent.Drag);
        joyStick.gameObject.AddUIEvent(OnPointerDown, Define.UIEvent.BeginDrag);
        joyStick.gameObject.AddUIEvent(OnPointerUp, Define.UIEvent.DragEnd);

    }
    void OnTouch(Vector2 vecTouch)
    {
        Vector2 vec = new Vector2(vecTouch.x - m_rectBack.position.x, vecTouch.y - m_rectBack.position.y);

        // vec값을 m_fRadius 이상이 되지 않도록 합니다.
        vec = Vector2.ClampMagnitude(vec, m_fRadius);
        m_rectJoystick.localPosition = vec;

        // 조이스틱 배경과 조이스틱과의 거리 비율로 이동합니다.
        float fSqr = (m_rectBack.position - m_rectJoystick.position).sqrMagnitude / (m_fRadius * m_fRadius);

        // 터치위치 정규화
        Vector2 vecNormal = vec.normalized;

        m_vecMove = new Vector3(vecNormal.x * m_fSpeed * Time.deltaTime * fSqr, 0f, vecNormal.y * m_fSpeed * Time.deltaTime * fSqr);
        character.transform.eulerAngles = new Vector3(0f, Mathf.Atan2(vecNormal.x, vecNormal.y) * Mathf.Rad2Deg, 0f);
    }
    public void Run(PointerEventData eventData)
    {
        OnTouch(eventData.position);
        m_bTouch = true;
    }
    public void OnDrag(PointerEventData eventData)
    {
        OnTouch(eventData.position);
        m_bTouch = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnTouch(eventData.position);
        m_bTouch = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // 원래 위치로 되돌립니다.
        m_rectJoystick.localPosition = Vector2.zero;
        m_bTouch = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (m_bTouch)
        {
            character.transform.position += m_vecMove;
        }
    }
}

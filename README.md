# 착하게 살자
![BeKind_Icon (1)](https://github.com/user-attachments/assets/81707f70-fab6-4ea7-bad3-bfaee1da2d2d)


# 홍익대학교 ExP Make 2023 1학기 게임잼 착하게 살자
Unity / 2D / 아케이드

## 역할 분담 🧑‍💻
### 개발 인원 : 7명 [ExP Make 2023 1학기 게임잼 팀프로젝트]
| 이름 | 개인 역할 | 담당 역할 및 기능 |
| ------ | ---------- | ------ |
| 백승민 | 기획 | 메인 기획자 |
| 박민지 | 기획 | 서브 기획자 |
| 문서정 | 아트 | 디자이너 |
| 유수진 | 아트 | 디자이너 |
| 한효빈 | Developer | 개발 |
| 이관진 | Developer | 개발 |
| 문서정 | 사운드 | 사운드 |

## 개발 기간 📅
2023.08 ~ 2024. 08

## 시연영상 
#### ⬇ Link Here ⬇
[https://youtu.be/4PYj3PyUdYQ](https://youtu.be/8BB4Xyhk8IA)
 
## 기술 스택 💻
<img src="https://img.shields.io/badge/Unity-FFFFFF?style=for-the-badge&logo=Unity&logoColor=black">
<img src="https://img.shields.io/badge/csharp-512BD4?style=for-the-badge&logo=csharp&logoColor=white">

### Singleton 패턴을 이용한 Manager Class 관리
```{cpp}
public class Managers : MonoBehaviour
{
    // 싱글톤 인스턴스
    static Managers s_instance;
    static Managers Instance { get { Init();  return s_instance; } }

    // 각종 매니저들 선언
    UI_Manager _ui = new UI_Manager();
    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();
    InputManager _input = new InputManager();
    SoundManager _sound = new SoundManager();
    MapManager _map = new MapManager();
    PoolManager _pool = new PoolManager();
    
    // 외부에서 접근 가능한 정적 프로퍼티들
    public static UI_Manager UI { get { return Instance._ui; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static InputManager Input { get { return Instance._input; } }
    public static SoundManager Sound { get { return Instance._sound;  } }
    public static MapManager Map { get { return Instance._map; } }
    public static PoolManager Pool { get { return Instance._pool; } }

    void Start() 
    {
        Init();
    }
    
    // Update is called once per frame
    void Update()
    {

    }
    
    // Managers 초기화 메서드
    static void Init()
    {
		    // "@Manager"라는 이름의 GameObject를 찾아서 사용, 없다면 새로 생성
        GameObject go = GameObject.Find("@Manager");
        if (go == null)
        {
            go = new GameObject { name = "@Manager" };
            go.AddComponent<Managers>();
        }
        
        // 씬 옮겨도 파괴되지 않도록 설정
        DontDestroyOnLoad(go);
        s_instance = go.GetComponent<Managers>();

				// PoolManager와 SoundManager 초기화
        s_instance._pool.Init();
        s_instance._sound.Init();
    }
    
    // 모든 매니저 초기화 및 데이터 정리
    public static void Clear()
    {
        Input.Clear();
        Sound.Clear();
        Scene.Clear();
        UI.Clear();
        Pool.Clear();
    }
}
```
### 추상 클래스와 상속을 활용한 Controller Class 관리
```{cpp}
public abstract class BaseController : MonoBehaviour
{
    [SerializeField]
    protected GameObject _lockTarget;// 목표 게임오브젝트
    
    [SerializeField]
    protected Define.State _state = Define.State.Idle;// 현재 상태 (기본값은 Idle)
    
    public Define.WorldObject WorldObjectType 
    { get; protected set; } = Define.WorldObject.Unknown;// 월드 오브젝트 타입 설정 (기본값은 Unknown)
    
    
    private void Start()
    {
        Init();
    }
    
    // 추상 메서드 Init - 상속받은 클래스에서 반드시 구현해야 함
    public abstract void Init();
    
    // 상태를 설정하는 프로퍼티
    public virtual Define.State State
    {
        get { return _state; }
        set
        {
            _state = value;

            Animator anim = GetComponent<Animator>();
            // 상태에 따라 애니메이션 설정
            switch (_state)
            {
                case Define.State.Idle:
                    anim.SetBool("isWalk", false);
                    anim.SetBool("isRun", false);
                    break;
                case Define.State.Walk:
                    anim.SetBool("isWalk", true);
                    anim.SetBool("isRun", false);
                    break;
                case Define.State.Run:
                    anim.SetBool("isRun", true);
                    break;
            }
        }
    }

		// Idle 상태 업데이트 메서드 (상속 클래스에서 오버라이드 가능)
    protected virtual void UpdateIdle() {}

		// Walk 상태 업데이트 메서드 (상속 클래스에서 오버라이드 가능)
    protected virtual void UpdateWalk() {}

		// Run 상태 업데이트 메서드 (상속 클래스에서 오버라이드 가능)
    protected virtual void UpdateRun() {}
}
```

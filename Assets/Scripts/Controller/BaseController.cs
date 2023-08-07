using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public abstract class BaseController : MonoBehaviour
{
    [SerializeField]
    protected GameObject _lockTarget;
    [SerializeField]
    protected Define.State _state = Define.State.Idle;
    public Define.WorldObject WorldObjectType { get; protected set; } = Define.WorldObject.Unknown;
    // Start is called before the first frame update
    private void Start()
    {
        Init();
    }
    public abstract void Init();
    public virtual Define.State State
    {
        get { return _state; }
        set
        {
            _state = value;

            Animator anim = GetComponent<Animator>();
            switch (_state)
            {
                case Define.State.Idle:
                    anim.CrossFade("WAIT", 0.1f);
                    break;
                case Define.State.Walk:
                    anim.CrossFade("WALK", 0.1f);
                    break;
                case Define.State.Run:
                    anim.CrossFade("RUN", 0.1f, -1, 0);
                    break;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        switch (_state)
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
    }
    protected virtual void UpdateIdle()
    {
    }

    protected virtual void UpdateWalk()
    {
    }

    protected virtual void UpdateRun()
    {
    }
}

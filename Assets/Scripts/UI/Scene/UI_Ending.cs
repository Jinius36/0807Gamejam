using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Ending : UI_Popup
{
    // Start is called before the first frame update
    public enum GameObjects
    {
        BackGround,
        EndingCredit
    }
    public GameObject Final;
    public Animator anim;
    void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(GameObjects));
        Get<GameObject>((int)GameObjects.BackGround);
        anim = Get<GameObject>((int)GameObjects.EndingCredit).GetComponent<Animator>();
        StartCoroutine(EndingCreditStart());
    }
    IEnumerator EndingCreditStart()
    {
        yield return new WaitForSeconds(3.0f);
        anim.Play("EndingCredit");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // init ¼¼ÆÃ
    public override void Initialize(int Hp, int ATK, int AttakRate, int AttackRange)
    {
        base.Initialize(Hp, ATK, AttakRate, AttackRange);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(SM.CurrentState.ToString());
        SM.CurrentState.Update();
    }

    public override void OnAttack1Trigger()
    {
        base.OnAttack1Trigger();
    }
}

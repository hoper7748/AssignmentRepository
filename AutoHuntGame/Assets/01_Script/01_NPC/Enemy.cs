using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{

    public virtual void Initialize(int Hp, int ATK, int AttakRate, int AttackRange)
    {
        SM = new StateMachine();
        Idle = new IdleState(SM, this);
        Attack = new AttackState(SM, this);
        Dead = new DeadState(SM, this);
        Move = new MoveState(SM, this);

        SM.Initialize(Idle);

        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();


        this.HP = Hp;
        this.Atk = ATK;
        this.AttackRate = AttakRate;
        this.curAttackTimer = AttackRate;
        this.AttackRange = AttackRange;

        

    }

    public override void OnAttack1Trigger()
    {
        base.OnAttack1Trigger();
    }

    private void Update()
    {
        if (!isAttack())
            AttackTimer();
    }

}

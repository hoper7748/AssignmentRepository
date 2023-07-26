using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : Character
{
    protected EnemySpawner Spawner;
    public virtual void Initialize(EnemySpawner Spawner,int Hp, int ATK, int AttakRate, int AttackRange)
    {
        SM = new StateMachine();
        Idle = new IdleState(SM, this);

        InitializeEAtkType();

        Attack = new AttackState(SM, this);
        Dead = new DeadState(SM, this);
        Move = new MoveState(SM, this);

        SM.Initialize(Idle);

        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        this.HP = Hp;
        this.curHp = this.HP;
        this.Atk = ATK;
        this.AttackRate = AttakRate;
        this.curAttackTimer = this.AttackRate;
        this.AttackRange = AttackRange;
        this.AttackEnd = true;
        this.Spawner = Spawner;

    }

    public override void OnAttack1Trigger()
    {
        switch (EAtkType[curEAtk])
        {
            case ATKTYPE.NORMAL_ATK:
                base.OnAttack1Trigger();
                break;
            case ATKTYPE.SKILL1:
                break;
            case ATKTYPE.SKILL2:
                break;
        }
    }

    private void Update()
    {

    }

}

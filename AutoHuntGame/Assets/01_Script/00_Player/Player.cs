using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Player : Character
{

    static Player instance;
    public static Player Instance { get { return instance; } }

    public float SkillRange;
    public float EXP;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        SM = new StateMachine();

        Idle = new IdleState(SM,this);
        Attack = new AttackState(SM, this);
        Dead = new DeadState(SM, this);
        Move = new MoveState(SM, this);

        SM.Initialize(Idle);

        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttack())
            AttackTimer();
    }

    public override void OnAttack1Trigger()
    {
        base.OnAttack1Trigger();
    }
}

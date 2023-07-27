using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Player : Character
{

    static Player instance;
    
    public static Player Instance { get { return instance; } }
    
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {

        SM = new StateMachine();

        Idle = new IdleState(SM, this);
        //Attacks = new AttackState[EAtkType.Length];
        InitializeEAtkType();
        Dead = new DeadState(SM, this);
        Move = new MoveState(SM, this);

        SM.Initialize(Idle);

        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        if (rigidbody == null)
        {
            gameObject.AddComponent<Rigidbody>();
            rigidbody = GetComponent<Rigidbody>();
        }
        Level = 1;
        this.HP = GameManager.Instance.GetLoad ? LoadManager.Instance.LoadPlayerHp() : this.HP;
        this.curHp = HP;
        this.Level = GameManager.Instance.GetLoad ? LoadManager.Instance.LoadPlayerLv() : this.Level;
        this.EXP = GameManager.Instance.GetLoad ? LoadManager.Instance.LoadPlayerEXP() : this.EXP;
        AttackEnd = true;

        UIManager.Instance.UpdateLvGage();
        UIManager.Instance.GetExp();
        UIManager.Instance.UpdateHpGage();
    }

    public override void KillTarget()
    {
        base.KillTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isGame)
        {
            if (!isAttack() && AttackEnd)
                AttackTimer();
            SM.CurrentState.Update();
        }
    }

    public override void OnAttack1Trigger()
    {
        switch (EAtkType[curEAtk])
        {
            case ATKTYPE.NORMAL_ATK:
            case ATKTYPE.SKILL2:
                base.OnAttack1Trigger();
                break;
            case ATKTYPE.SKILL1:
                break;
        }
    }
}
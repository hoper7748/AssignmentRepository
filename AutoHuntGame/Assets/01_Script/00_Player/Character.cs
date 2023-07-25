using UnityEngine;

public class Character : MonoBehaviour
{
    public enum ATKTYPE
    {
        NORMAL_ATK,
        SKILL1,
        SKILL2,
    }

    public StateMachine SM;
    public IdleState Idle;
    public AttackState[] Attacks;
    public AttackState Attack;
    public SkillState Skill;
    public DeadState Dead;
    public MoveState Move;

    public Rigidbody rigidbody;
    public Animator animator;

    [Header("Controls")]
    protected int Level;
    public int HP;
    protected int curHp;
    public int Atk;
    public int AttackRate;
    public int AttackRange;
    public int SkillRange;

    public ATKTYPE[] EAtkType;
    public int curEAtk = 0;

    private int CurAtkCount;
    private int AttackCount;

    // Player일 경우에만 증가.
    public float MaxEXP;
    protected float EXP;

    public bool AttackEnd;
    public bool isDead = false;

    public GameObject Target;

    public float curAttackTimer;

    public string stateName;
    private void Start()
    {

    }

    public bool CheckAttackDistance()
    {
        try
        {
            if (Vector3.Distance(Target.transform.position, transform.position) < AttackRange)
            {
                return true;
            }
            return false;
        }
        catch
        {
            return false;
        }
    }

    public void ResetAtkTimer()
    {
        curAttackTimer = 0;
    }

    public virtual void KillTarget()
    {
        // 경험치를 얻고 타겟을 초기화 한다.
        Target = null;
        EXP += 10;
        if (EXP >= MaxEXP)
        {
            Level++;
            HP += 10;
            Atk += 5;
            curHp = HP;   // 체력 재생
            EXP = 0;
            UIManager.Instance.UpdateLv();
        }
        UIManager.Instance.GetExp();
    }

    public virtual void OnAttack1Trigger()
    {
        bool AtkTypeCheck = EAtkType.Length > 1 
            && EAtkType[curEAtk] == ATKTYPE.SKILL2;
        AttackEnd = true;
        Target.GetComponent<Character>().GetDamaged(this.Atk);
        if (AtkTypeCheck)
        {
            this.curHp += Atk;  // 회복
            if (curHp > HP)
                curHp = HP;
            Debug.Log($"{this.gameObject.name} Healing: {curHp - Atk} -> {curHp}");
        }
        curAttackTimer = 0;
    }

    public void GetDamaged(int damage)
    {
        curHp -= damage;
    }

    public void InitializeEAtkType()
    {
        if (EAtkType.Length <= 0)
        {
            // 공격 패턴이 등록되어 있지 않은 경우.
            EAtkType = new ATKTYPE[1];
            EAtkType[0] = ATKTYPE.NORMAL_ATK;
        }
        Attacks = new AttackState[EAtkType.Length];

        for (int atk = 0; atk < this.EAtkType.Length; atk++)
        {
            switch (this.EAtkType[atk])
            {
                case ATKTYPE.NORMAL_ATK:
                    Attacks[atk] = new AttackState(SM, this);
                    break;
                case ATKTYPE.SKILL1:
                    Attacks[atk] = new SkillState(SM, this);
                    break;
                case ATKTYPE.SKILL2:
                    Attacks[atk] = new SpecialAttackState(SM, this);
                    break;
            }
        }
    }

    public bool isAttack()
    {
        return curAttackTimer > 1f / (float)AttackRate ? true : false;
    }

    public void AttackTimer()
    {
        curAttackTimer += Time.deltaTime;
    }

    public void EAtkUpdate()
    {
        AttackEnd = false;
        ResetAtkTimer();
    }

    public int GetLv
    {
        get
        {
            return Level;
        }
    }

    public int GetHP
    {
        get
        {
            return curHp;
        }
    }
    public float GetEXP
    {
        get
        {
            return EXP;
        }
    }
}

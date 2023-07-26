using UnityEngine;

public class SkillState : AttackState
{

    float timer = 0;
    float skillDelay = 0.7f;
    float waitTime = 1.0f;
    bool bSkill = false;
    public SkillState(StateMachine _stateMachine, Character charBase) : base(_stateMachine, charBase)
    {

    }

    public override void Enter()
    {
        //base.Enter();
        timer = 0;
        bSkill = false;

        //Debug.Log($"Skill");
        charBase.stateName = "Skill";
        Rotate();
        if (DeadCheck() || charBase.Target == null)
            return;
        // 공격 대기 시간이 남았다면 공격하지 않음.
        if (!charBase.isAttack())
            return;
        charBase.animator.SetTrigger("isSkill");
        charBase.EAtkUpdate();
    }
    private void HitDamAll()
    {
        Collider[] cols = Physics.OverlapSphere(charBase.transform.position, charBase.SkillRange, 1 << 6);
        if (cols.Length > 0)
        {
            for (int i = 0; i < cols.Length; i++)
            {
                bool EnemyCheck = cols[i].gameObject.layer == LayerMask.NameToLayer("Enemy") && !cols[i].GetComponent<Character>().isDead;
                if (EnemyCheck)
                {
                    cols[i].GetComponent<Character>().GetDamaged(charBase.Atk);
                }
            }
        }
        bSkill = true;
    }

    private void useSkill()
    {
        if (timer < skillDelay)
        {
            timer += Time.deltaTime;
            return;
        }
        else if (!bSkill)
        {
            HitDamAll();
        }
    }

    private void StateChange()
    {
        bool bAttackCheck = (!charBase.isAttack() && charBase.AttackEnd);
        bool bTarget = charBase.Target == null;
        if (timer < waitTime)
        {
            timer += Time.deltaTime;
            return;
        }
        else
        {
            Debug.Log($"bAttackCheck = {bAttackCheck}");
            Debug.Log($"bTarget = {bTarget}");
            charBase.AttackEnd = true;
            if (DeadCheck())
            {
                //Debug.Log($"DeadCheck Hit");
                return;
            }
            if (bAttackCheck || bTarget)
            {
                //Debug.Log($"StateChange Hit");
                stateMachine.ChangeState(charBase.Idle);
            }
        }
    }

    public override void Update()
    {

        useSkill();
        StateChange();
    }

    public override void Exit()
    {
        base.Exit();
        //charBase.curEAtk++;
        //if (charBase.EAtkType.Length <= charBase.curEAtk)
        //    charBase.curEAtk = 0;

    }

}

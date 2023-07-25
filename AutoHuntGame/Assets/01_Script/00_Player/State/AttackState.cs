using UnityEngine;

public class AttackState : State
{
    public AttackState(StateMachine _stateMachine, Character charBase) : base(_stateMachine, charBase)
    {

    }
    protected void Rotate()
    {
        Vector3 rotVec = charBase.Target.transform.position - charBase.transform.position;
        charBase.transform.rotation = Quaternion.LookRotation(rotVec).normalized;
    }

    public override void Enter()
    {
        //base.Enter();
        Rotate();
        charBase.stateName = "Attck";
        if (DeadCheck() || charBase.Target == null)
            return;
        // 공격 대기 시간이 남았다면 공격하지 않음.
        if (!charBase.isAttack())
            return;
        charBase.animator.SetTrigger("isAttack");
        charBase.EAtkUpdate();
    }


    public override void Update()       
    {
        base.Update();
        if (DeadCheck())
            return;
        if (!charBase.isAttack() && charBase.AttackEnd)
            stateMachine.ChangeState(charBase.Idle);
    }

    public override void Exit()
    {
        base.Exit();
        charBase.curEAtk++;
        if (charBase.EAtkType.Length <= charBase.curEAtk)
            charBase.curEAtk = 0;
    }
}

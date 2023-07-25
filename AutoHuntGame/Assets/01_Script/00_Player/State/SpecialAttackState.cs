using UnityEngine;

public class SpecialAttackState : AttackState
{
    public SpecialAttackState(StateMachine _stateMachine, Character charBase) : base(_stateMachine, charBase)
    {

    }

    public override void Enter()
    {
        //base.Enter();
        Debug.Log($"Special Attack");

        charBase.stateName = "Special";
        charBase.animator.SetTrigger("isAttack");
        charBase.EAtkUpdate();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
        //charBase.curEAtk++;
        //if (charBase.EAtkType.Length <= charBase.curEAtk)
        //    charBase.curEAtk = 0;
    }
}

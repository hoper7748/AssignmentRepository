
using UnityEngine;

public class MoveState : State
{

    float speed = 2f;
    // 타겟을 향해 전진하는 기능을 가짐.
    // 타겟과 가까워지면 타겟을 공격하는 Attack State로 변경됨.
    public MoveState(StateMachine _stateMachine, Character _charBase) : base(_stateMachine, _charBase)
    {

    }

    public override void Enter()
    {
        base.Enter();
        charBase.animator.SetBool("isWalk", true);
        charBase.stateName = "Move";
    }

    private Vector3 Caculation()
    {
        Vector3 directionToMovement = charBase.Target.transform.position - charBase.transform.position;
        return directionToMovement.normalized;
    }
    private bool StateChange()
    {
        if (charBase.Target == null)
        {
            stateMachine.ChangeState(charBase.Idle);
            return true;
        }
        else if (charBase.CheckAttackDistance())
        {
            stateMachine.ChangeState(charBase.Attacks[charBase.curEAtk]);
            return true;
        }
        return false;
    }

    private void Rotate()
    {
        Vector3 rotVec = charBase.Target.transform.position - charBase.transform.position;
        charBase.transform.rotation = Quaternion.LookRotation(rotVec).normalized;
    }

    public override void Update()
    {
        base.Update();
        try
        {
            if (DeadCheck())
                return;
            if (StateChange())
            {
                return;
            }
            else
            {
                // Player를 향해 움직임.
                Rotate();
                charBase.rigidbody.velocity = Caculation();
                //Debug.Log($"{charBase.gameObject.name} - calc: {Caculation()} , velocity: {charBase.rigidbody.velocity}");
            }
        }
        catch
        {
            Debug.LogWarning($"Move State Error");
        }
    }

    public override void Exit()
    {
        base.Exit();
        charBase.rigidbody.velocity = Vector3.zero;
        charBase.animator.SetBool("isWalk", false);
    }
}

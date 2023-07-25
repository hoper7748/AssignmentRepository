
using UnityEngine;

public class MoveState : State
{

    float speed = 2f;
    // Ÿ���� ���� �����ϴ� ����� ����.
    // Ÿ�ٰ� ��������� Ÿ���� �����ϴ� Attack State�� �����.
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
                // Player�� ���� ������.
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

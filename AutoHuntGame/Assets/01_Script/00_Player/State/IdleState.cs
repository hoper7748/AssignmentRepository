
using UnityEngine;

public class IdleState : State
{
    
    private bool bSearch = false;
    private float waitTime = 1f;
    private float timer = 0;
    // ���� ��ġ ���� �ֺ��� ���� �����Ǹ� �ش� ���� ���� ���ư��� MoveState�� �����.
    public IdleState(StateMachine _stateMachine, Character _charBase) : base(_stateMachine, _charBase)
    {

    }

    public override void Enter()
    {
        base.Enter();
        if(charBase.Target == null)
            bSearch = false;
        charBase.stateName = "Idle";
    }

    private bool RangeCheck(Vector3 A, Vector3 B)
    {
        return Vector3.Distance(charBase.transform.position, A) < Vector3.Distance(charBase.transform.position, B);

    }

    private void Search()
    {
        
        if (bSearch) return;
        if (charBase.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            // ���� ��� �ڵ����� Player�� ��������.
            charBase.Target = Player.Instance.gameObject;
            bSearch = true;
        }
        else if(charBase.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // Player�� ��� �� ������ üũ�Ͽ� ���� ����� ���� Ž�� �� ����.
            Collider[] cols = Physics.OverlapSphere(charBase.transform.position, 10f, 1 << 6);
            if(cols.Length > 0)
            {
                for(int i =0; i < cols.Length; i++)
                {
                    bool EnemyCheck = cols[i].gameObject.layer == LayerMask.NameToLayer("Enemy") && !cols[i].GetComponent<Character>().isDead;
                    if (EnemyCheck)
                    {
                        if(charBase.Target == null) 
                            charBase.Target = cols[i].gameObject;
                        else
                        {
                            charBase.Target = RangeCheck(charBase.Target.transform.position, cols[i].transform.position) ? charBase.Target: cols[i].gameObject;
                        }
                    }
                }
            }
            bSearch = charBase.Target == null ? false : true;
        }
    }

    private void StateChange()
    {
        if (bSearch)
        {
            if (charBase.CheckAttackDistance() && charBase.isAttack())
            {
                stateMachine.ChangeState(charBase.Attacks[charBase.curEAtk]);
            }
            else if (!charBase.CheckAttackDistance())
            {
                // ���� �Ÿ��� �� ���
                stateMachine.ChangeState(charBase.Move);
            }
        }
    }

    public override void Update()
    {
        base.Update();
        if (DeadCheck())
            return;
        Search();
        // Ž�� 
        if (timer < waitTime)
        {
            timer += Time.deltaTime;
            return;
        }
        StateChange();
    }

    public override void Exit()
    {
        base.Exit();
    }
}


using UnityEngine;

public class IdleState : State
{
    
    private bool bSearch = false;
    private float waitTime = 1f;
    private float timer = 0;
    // 현재 서치 상태 주변에 적이 생성되면 해당 적을 향해 나아가는 MoveState로 변경됨.
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
            // 적일 경우 자동으로 Player를 세팅해줌.
            charBase.Target = Player.Instance.gameObject;
            bSearch = true;
        }
        else if(charBase.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // Player일 경우 원 범위를 체크하여 가장 가까운 적을 탐색 후 따라감.
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
                // 아직 거리가 먼 경우
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
        // 탐색 
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

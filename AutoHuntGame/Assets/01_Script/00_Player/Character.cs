using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public StateMachine SM;
    public IdleState Idle;
    public AttackState Attack;
    public DeadState Dead;
    public MoveState Move;

    public Rigidbody rigidbody;
    public Animator animator;

    [Header("Controls")]
    public int HP;
    public int Atk;
    public int AttackRate;
    public int AttackRange;

    public GameObject Target;

    protected float curAttackTimer;

    private void Start()
    {
        //if (rigidbody == null)
        //    Debug.Log("Not have Rigdibody");
        //Debug.Log("AA");   
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

    public virtual void OnAttack1Trigger()
    {
        Target.GetComponent<Character>().HP -= this.Atk;
        curAttackTimer = 0;
    }

    public bool isAttack()
    {
        if(curAttackTimer > 1 / AttackRate )
        {
            return true;
        }
        return false;
    }

    public void AttackTimer()
    {
        curAttackTimer += Time.deltaTime;
    }
}

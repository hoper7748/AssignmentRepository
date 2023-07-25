using UnityEngine;

public class DeadState : State
{

    public DeadState(StateMachine _stateMachine, Character _charBase) : base(_stateMachine, _charBase)
    {

    }

    public override void Enter()
    {
        base.Enter();
        charBase.isDead = true;
        charBase.gameObject.layer = LayerMask.NameToLayer("DeadCharacter");
        charBase.Target.GetComponent<Character>().KillTarget();
        charBase.animator.SetTrigger("isDead");
        charBase.stateName = "Dead";
        GameObject.Destroy(charBase.gameObject, 2f);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }

}

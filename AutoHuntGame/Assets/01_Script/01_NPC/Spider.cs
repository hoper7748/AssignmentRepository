using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // init ¼¼ÆÃ
    public override void Initialize(EnemySpawner Spawner, int Hp, int ATK, int AttakRate, int AttackRange)
    {
        base.Initialize(Spawner, Hp, ATK, AttakRate, AttackRange);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.Instance.isGame)
        {
            //Debug.Log(SM.CurrentState.ToString());
            if (!isAttack() && AttackEnd)
                AttackTimer();

            SM.CurrentState.Update();
        }
    }

    public override void OnAttack1Trigger()
    {
        base.OnAttack1Trigger();
    }

    private void OnDestroy()
    {
        Spawner.DeadEnemy();
        //if (Player.Instance.Target == this)
        //    Player.Instance.KillTarget();

    }
}

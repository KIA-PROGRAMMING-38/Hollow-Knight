using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : StateMachineBehaviour
{
    private float idleMinSec = 1f;
    private float idleMaxSec = 3f;
    private float totalElapsedTime;
    private float idleTime;

    private MonsterController monster;
    private Transform monsterTransform;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        monster = animator.GetComponent<MonsterController>();
        monsterTransform = animator.GetComponent<Transform>();
        
        Init();
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float deltaTime = Time.deltaTime;
        
        totalElapsedTime += deltaTime;
        Attack(animator);
        if(totalElapsedTime >= idleTime)
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Walk", true);
        }

    }
   
    private void Init()
    {
        idleTime = Random.Range(idleMinSec, idleMaxSec);
        totalElapsedTime = 0f;
    }

    private void Attack(Animator anim)
    {
        Vector2 direction = monster.target.position - monsterTransform.position;
        float distance = direction.magnitude;
        if (distance <= monster.attackRange)
        {
            anim.SetBool("Idle", false);
            anim.SetBool("Walk", false);
            anim.SetTrigger("Hit");
        }
        return;
    }
}

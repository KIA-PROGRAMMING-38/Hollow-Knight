using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : StateMachineBehaviour
{
    private BossController boss;
    private float idleMinSec = 1f;
    private float idleMaxSec = 5f;
    private float totalElapsedTime;
    private float idleTime;
    private int randomPattern;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<BossController>();
        Init();
        boss.Flip(boss.target.transform.position.x, boss.transform.position.x);
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float deltaTime = Time.deltaTime;

        totalElapsedTime += deltaTime;
        if(totalElapsedTime >= idleTime)
        {
            animator.SetBool("Idle", false);

            switch (randomPattern)
            {
                case 1:
                    animator.SetTrigger("Attack");
                    break;
                case 2:
                    animator.SetBool("JumpAttackReady", true);
                    break;
                case 3:
                    animator.SetBool("JumpUp", true);
                    break;
                case 4:
                    animator.SetBool("Run", true);
                    break;
                case 5:
                    if(boss.bossHealth <= 500)
                    {
                        animator.SetTrigger("AttackRe");
                        break;
                    }
                    break;
            }
        }
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        randomPattern = 0;
    }

    private void Init()
    {
        idleTime = Random.Range(idleMinSec, idleMaxSec);
        randomPattern = Random.Range(1, 5);
        totalElapsedTime = 0f;
        boss.rigid.constraints = RigidbodyConstraints2D.None;
        boss.rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    
}

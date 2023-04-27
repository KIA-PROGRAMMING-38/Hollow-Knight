using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MonsterIdle : StateMachineBehaviour
{
    internal float idleMinSec = 1f;
    internal float idleMaxSec = 3f;
    internal float totalElapsedTime;
    internal float idleTime;

    internal MonsterController monster;
    internal Transform monsterTransform;

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
}

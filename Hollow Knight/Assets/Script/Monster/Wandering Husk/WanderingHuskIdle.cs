using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WanderingHuskIdle : MonsterIdle
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        monster = animator.GetComponent<WanderingHusk>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
    }

    
}

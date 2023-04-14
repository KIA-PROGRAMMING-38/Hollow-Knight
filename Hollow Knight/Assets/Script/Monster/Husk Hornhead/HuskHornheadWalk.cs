using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuskHornheadWalk : MonsterWalk
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        monster = animator.GetComponent<HuskHornhead>();
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
    }

    
}

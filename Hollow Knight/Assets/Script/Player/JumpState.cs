using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : StateMachineBehaviour
{
    Transform playerTransform;
    PlayerController player;
    LayerMask layerMask;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = animator.GetComponent<PlayerController>();
        playerTransform = animator.GetComponent<Transform>();
        layerMask = animator.GetComponent<LayerMask>();
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }


}

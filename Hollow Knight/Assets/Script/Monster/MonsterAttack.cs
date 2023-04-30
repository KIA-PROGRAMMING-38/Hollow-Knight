using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class MonsterAttack : StateMachineBehaviour
{
    internal MonsterController monster;
    internal Transform monsterTransform;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        monster = animator.GetComponent<MonsterController>();
        monsterTransform = animator.GetComponent<Transform>();
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        monster.Flip(monster.target.position.x, monsterTransform.position.x);
        //monsterTransform.position = Vector2.MoveTowards(monsterTransform.position, monster.target.position, monster.attackSpeed * Time.deltaTime);
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
    
}

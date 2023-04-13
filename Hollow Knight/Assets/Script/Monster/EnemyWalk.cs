using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyWalk : StateMachineBehaviour
{
    private float patrolMinSec = 3f;
    private float patrolMaxSec = 5f;
    private float patrolSec;

    private MonsterController monster;
    private Transform monsterTransform;

    private Vector3 nextPoint;
    private int currentPointIndex = 0;
    private float elapsedTime;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        monster = animator.GetComponent<MonsterController>();
        monsterTransform = animator.GetComponent<Transform>();
        Init();
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float delta = Time.deltaTime;
        float step = monster.moveSpeed * delta;
        elapsedTime += delta;

        Attack(animator);

        if (elapsedTime >= patrolSec)
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Idle", true);
            return;
        }

        monster.transform.position = Vector3.MoveTowards(monster.transform.position, nextPoint, step);
        if(Vector3.Distance(monster.transform.position, nextPoint) < 0.1f)
        {
            SetNextPoint();
        }

    }

    private void SetNextPoint()
    {
        nextPoint = monster.patrolPoints[currentPointIndex].position;
        currentPointIndex = (currentPointIndex + 1) % 2;
        if (currentPointIndex == 0)
            monster.spriteRenderer.flipX = true;
        else
            monster.spriteRenderer.flipX = false;
    }
    private void Init()
    {
        patrolSec = Random.Range(patrolMinSec, patrolMaxSec);
        SetNextPoint();
        elapsedTime = 0f;
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

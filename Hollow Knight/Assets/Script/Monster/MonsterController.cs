using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MonsterController : MonoBehaviour
{
    internal float moveSpeed = 3f;
    internal float attackSpeed = 7f;
    internal float attackCoolTime = 4f;
    internal int monsterHealth = 60;
    internal int hitDamage = 20;
    internal float attackRange = 7f;
    internal float attackDelay;
    

    public Transform target;
    public Transform[] patrolPoints;

    internal Animator anim;
    internal Collider2D col;
    internal SpriteRenderer spriteRenderer;
    internal Rigidbody2D rigid;

    public void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }
    
    public void Flip(float target, float obj)
    {
        if (target < obj)
            spriteRenderer.flipX = false;
        else
            spriteRenderer.flipX = true;
    }

    public void Damage(int damage)
    {
        int dir = transform.position.x - target.position.x > 0 ? 1 : -1;
        monsterHealth -= damage;
        Flip(target.position.x, transform.position.x);
        //피격시 넉백
        rigid.AddForce(new Vector2(dir * 5, 0), ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            // 데미지를 입었을 때
            anim.SetTrigger("Hit");
            Damage(hitDamage);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            // 처치되었을 때
            if(monsterHealth == 0)
            {
                anim.SetTrigger("Die");
                col.enabled = false;
                rigid.simulated = false;
                spriteRenderer.sortingOrder = -1;
            }

        }
    }



}

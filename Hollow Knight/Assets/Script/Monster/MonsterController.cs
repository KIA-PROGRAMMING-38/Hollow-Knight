using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MonsterController : MonoBehaviour
{
    internal float attackSpeed;
    internal float moveSpeed;
    internal float attackCoolTime;
    internal int monsterHealth;
    internal int hitDamage;
    internal float attackRange;

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
        //ÇÇ°Ý½Ã ³Ë¹é
        rigid.AddForce(new Vector2(dir * 4, 0), ForceMode2D.Impulse);
    }
}

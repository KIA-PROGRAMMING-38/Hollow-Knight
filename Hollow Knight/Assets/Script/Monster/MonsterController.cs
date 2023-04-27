using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MonsterController : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    internal float attackSpeed;
    internal float moveSpeed;
    internal float attackCoolTime;
    internal float attackRange;
    internal int monsterHealth;
    internal int hitDamage;

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
    private void Damage(int damage)
    {
        int dir = transform.position.x - target.position.x > 0 ? 1 : -1;
        monsterHealth -= damage;
        Flip(target.position.x, transform.position.x);
        //피격시 넉백
        if (spriteRenderer.flipX == false)
            rigid.velocity = Vector2.right * 10f;
        else
            rigid.velocity = Vector2.left * 10f;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            //데미지를 입었을 때
            Damage(hitDamage);
        }
        
    }
    public void AttackSpeed()
    {
        if(spriteRenderer.flipX == false)
            rigid.velocity = Vector2.left * attackSpeed;
        else
            rigid.velocity = Vector2.right* attackSpeed;
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            //처치 되었을 때
            if (monsterHealth == 0)
            {
                anim.SetTrigger("Die");
                col.enabled = false;
                rigid.simulated = false;
                spriteRenderer.sortingOrder = -1;
                uiManager.AcquisitionMpIcon(0.3f);
            }
        }
    }

    public void AttackDistance()
    {
        Vector2 direction = target.position - transform.position;
        float distance = direction.magnitude;
        
        if(distance <= attackRange)
        {
            anim.SetTrigger("Attack");
            Flip(target.position.x, transform.position.x);
        }
    }
}

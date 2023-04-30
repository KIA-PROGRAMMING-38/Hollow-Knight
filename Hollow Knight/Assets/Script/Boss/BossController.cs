using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class BossController : MonoBehaviour
{
    public int bossHealth;
    public int jumpForce;
    public int moveSpeed;
    private int hitDamage;
    private int skillDamage;
   
    public Transform target;
    private BossAttack _bossAttack;
    public UIManager ui;

    public AudioClip aui;
    internal Animator anim;
    internal Rigidbody2D rigid;
    internal SpriteRenderer spriteRenderer;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        _bossAttack = GetComponentInChildren<BossAttack>();

        hitDamage = 20;
        skillDamage = 40;
    }

    public void Flip(float target, float boss)
    {
        if (target < boss)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);
    }

    //보스 이펙트 생성
    private void ShowAttackEffect() => _bossAttack.Show();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            bossHealth -= hitDamage;
            spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f, 1);
            ui.AcquisitionMpIcon(0.3f);
        }
        if (collision.CompareTag("SkillBullet"))
        {
            bossHealth -= skillDamage;
            spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f, 1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon") || collision.CompareTag("SkillBullet"))
        {
            spriteRenderer.color = new Color(1, 1, 1, 1);
            if (bossHealth == 600 || bossHealth == 500 || bossHealth == 200)
            {
                anim.SetBool("Idle", false);
                anim.SetBool("JumpUp", false);
                anim.SetBool("Run", false);
                anim.SetTrigger("Stun");
            }
            if(bossHealth == 0)
            {
                anim.SetBool("Idle", false);
                anim.SetBool("JumpUp", false);
                anim.SetBool("Run", false);
                anim.SetTrigger("Die");
                gameObject.GetComponent<Collider2D>().enabled = false;
                rigid.simulated = false;
                spriteRenderer.sortingOrder = -1;
            }
        }
    }

}

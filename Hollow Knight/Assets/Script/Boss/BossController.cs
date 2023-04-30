using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BossController : MonoBehaviour
{
    // 보스가 체력 50이 되면 광폭화 진행
    // 체력 절반 이 되기 전에는 패턴이 정형화 절반 이하에는 랜덤
    // 체력 500 이라면 100 마다 스턴 효과
    public int bossHealth;
    public int jumpForce;
    public int moveSpeed;
    private int playerDamage;
   
    public Transform target;
    private BossAttack _bossAttack;
    private UIManager ui;
    

    internal Animator anim;
    internal Rigidbody2D rigid;
    private Material originmaterial;
    public Material material;
    internal SpriteRenderer spriteRenderer;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        material = GetComponent<Material>();
        ui = GetComponent<UIManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        _bossAttack = GetComponentInChildren<BossAttack>();

        playerDamage = 20;
        originmaterial = spriteRenderer.material;
        
    }

    public void Flip(float target, float boss)
    {
        if (target < boss)
            spriteRenderer.flipX = false;
        else
            spriteRenderer.flipX = true;
    }

    //보스 이펙트 생성
    private void ShowAttackEffect() => _bossAttack.Show();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            bossHealth -= playerDamage;
            spriteRenderer.material = material;
            ui.AcquisitionMpIcon(0.3f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            spriteRenderer.material = originmaterial;
            if(bossHealth == 600 || bossHealth == 500 || bossHealth == 200)
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BossController : MonoBehaviour
{
    // ������ ü�� 50�� �Ǹ� ����ȭ ����
    // ü�� ���� �� �Ǳ� ������ ������ ����ȭ ���� ���Ͽ��� ����
    // ü�� 500 �̶�� 100 ���� ���� ȿ��
    public int bossHealth;
    private bool isJump;
    public int jumpForce;
    public int moveSpeed;
    private float stunElapsedTime;
    private int playerDamage;
    private bool isDie;
    [SerializeField] private Transform pos;
    [SerializeField] private float checkRadious;
    [SerializeField] private LayerMask islayer;
    //public Transform target;

    private BossAttack _bossAttack;
    private UIManager ui;
    

    internal Animator anim;
    internal Rigidbody2D rigid;
    private Material originmaterial;
    public Material material;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        material = GetComponent<Material>();
        ui = GetComponent<UIManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        _bossAttack = GetComponentInChildren<BossAttack>();

        playerDamage = 20;
        stunElapsedTime = 0f;
        originmaterial = spriteRenderer.material;
        isDie = true;
    }

    private void Update()
    {
        
    }

    public void Flip(float target, float boss)
    {
        if (target < boss)
            spriteRenderer.flipX = false;
        else
            spriteRenderer.flipX = true;
    }

    //���� ����Ʈ ����
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

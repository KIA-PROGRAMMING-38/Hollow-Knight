using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private Transform pos;
    [SerializeField] private float checkRadious;
    [SerializeField] private LayerMask islayer;
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private UIManager uiManager;
    
    private int moveDirection;

    internal bool isGround;
    internal bool isJumping;
    internal int dashCount;
    internal float jumpTime;
    internal float jumpTimeCounter;
    internal int jumpCount;
    
    internal bool isSkill;
    internal bool isDash;
    internal float dashSpeed;
    internal float dashTime;

    private int life;

    private SlashEffect _slashEffect;
    private UpSlashEffect _upSlashEffect;
    private DownSlashEffect _downSlashEffect;
    private FireBallEffect _fireballEffect;
    private DashEffect _dashEffect;

    private Animator anim;
    internal Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        _slashEffect = GetComponentInChildren<SlashEffect>();
        _upSlashEffect = GetComponentInChildren<UpSlashEffect>();
        _downSlashEffect = GetComponentInChildren<DownSlashEffect>();
        _fireballEffect = GetComponentInChildren<FireBallEffect>();
        _dashEffect = GetComponentInChildren<DashEffect>();


        jumpTime = 0.5f;
        isSkill = true;
        dashSpeed = 30f;
        dashTime = 0.5f;
        life = 5;
        
    }

    void FixedUpdate()
    {
        Move();
    }
    void Update()
    {
        Flip();
        Jump();
        SlashActive();
        SkillActive();
    }

    // ��ų Ȱ��ȭ
    private void SkillActive()
    {
        if (Input.GetKeyDown(KeyCode.C) && dashCount == 1)
        {
            StartCoroutine(Dash());
        }

        if (Input.GetKeyDown(KeyCode.A) && isSkill)
        {
            // ���� ���� �� ��ų ��� �Ұ�
            if (uiManager.mpImage.fillAmount < 0.1f)
                return;

            anim.SetTrigger("isFireBall");
            StartCoroutine(SkillBullet());
            // ��ų ��� �� ���� ����
            uiManager.ConsumptionMpIcon(0.3f);
        }
        
    }
    private void ShowFireBallEffect() => _fireballEffect.Show();
    private void ShowDashEffect() => _dashEffect.Show();

    // ���� Ȱ��ȭ
    private void SlashActive()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                anim.SetTrigger("isUpSlash");
                return;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                anim.SetTrigger("isDownSlash");
                return;
            }
            anim.SetTrigger("isSlash");
        }
    }
    private void ShowSlashEffect() => _slashEffect.Show();
    private void ShowUpSlashEffect() => _upSlashEffect.Show();
    private void ShowDownSlashEffect() => _downSlashEffect.Show();
    
        
    
    void Move()
    {
        // �뽬 �� �̵��� ���� �ʴ´�.
        if (isDash)
            return;

        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            // �������� �ʴ´�.
            moveDirection = 0;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rigid.velocity = new Vector2(speed * -1, rigid.velocity.y);
            moveDirection = -1;
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rigid.velocity = new Vector2(speed, rigid.velocity.y);
            moveDirection = 1;
        }
    }
    void Flip()
    {
        if (isDash)
            return;
        if (Input.GetKey(KeyCode.LeftArrow))
                transform.localScale = new Vector3(1f, 1f, 1f);
        else if (Input.GetKey(KeyCode.RightArrow))
                transform.localScale = new Vector3(-1f, 1f, 1f);
    }
    
    void Jump()
    {
        // �뽬 �� ������ ���� �ʴ´�.
        if (isDash)
            return;

        // ������ Ȯ��
        isGround = Physics2D.OverlapCircle(pos.position, checkRadious, islayer);

        if (isGround)
        {
            jumpCount = 1;
            // ���� �� �뽬 �ѹ��� ���
            dashCount = 1;
            anim.SetBool("isJump", false);
            anim.SetBool("isJumpDown", false);

            if(rigid.velocity.x == 0)
                anim.SetBool("Idle", true);
        }
        
        else if (!isGround)
        {
            anim.SetBool("Idle", false);
            anim.SetBool("isJump", true);
        }

        if((isGround == true || jumpCount == 1) && Input.GetKeyDown(KeyCode.Z))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rigid.velocity = Vector2.up * jumpPower;
            
        }
        
        // Ȧ�� �� ���� ���� ���
        if((isJumping == true || jumpCount == 1) && Input.GetKey(KeyCode.Z))
        {
            if (jumpTimeCounter > 0)
            {
                rigid.velocity = Vector2.up * jumpPower;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                if(jumpCount == 0)
                {
                    isJumping = false;
                }
            }
        }

        if(rigid.velocity.y < -1f)
        {
            anim.SetBool("isJump", false);
            anim.SetBool("isJumpDown", true);
        }
        
        if (Input.GetKeyUp(KeyCode.Z))
        {
            --jumpCount;
            if(jumpCount == 0)
            {
                isJumping = false;
            }
        }
    }
    IEnumerator Dash()
    {
        float originalGravity = rigid.gravityScale;
        // �߷� ����
        rigid.gravityScale = 0f;
        isDash = true;
        dashCount--;
        
        if (transform.localScale.x == -1)
            rigid.velocity = Vector2.right * dashSpeed;
        else
            rigid.velocity = Vector2.left * dashSpeed;

        yield return new WaitForSeconds(dashTime);
        rigid.gravityScale = originalGravity;
        rigid.velocity = Vector2.zero;
        isDash = false;
    }

    IEnumerator SkillBullet()
    {
        float bulletTime = 0.4f;
        float bulletSpeed = 50f;
        isSkill = false;
        // ��ų �ҷ� ����
        GameObject bullet = ObjectManager.instance.BulletPooledObject();
        if (bullet != null)
        {
            bullet.transform.position = bulletSpawn.position;
            Vector3 scale = bullet.transform.localScale;
            scale.x = transform.localScale.x * (-1);
            bullet.transform.localScale = scale;
            bullet.SetActive(true);
        }

        if (transform.localScale.x < 0)
            bullet.transform.GetComponent<Rigidbody2D>().velocity = Vector3.right * bulletSpeed;
        else
            bullet.transform.GetComponent<Rigidbody2D>().velocity = Vector3.left * bulletSpeed;
        yield return new WaitForSeconds(bulletTime);
        bullet.SetActive(false);
        isSkill = true;
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Monster"))
        {
            life--;
            uiManager.UpdateLifeIcon(life);
        }
    }


}

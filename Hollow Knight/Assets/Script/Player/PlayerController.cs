using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Pool;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private Transform pos;
    [SerializeField] private float checkRadious;
    [SerializeField] private LayerMask islayer;
    [SerializeField] private Transform effectPosition;
    [SerializeField] private Transform attackPositionX;
    [SerializeField] private Transform attackUpPosition;
    [SerializeField] private Transform attackDownPosition;
    [SerializeField] private Transform skillPosition;
    
    
    private int moveDirection;
    public ObjectManager objectManager;

    private bool isGround;
    private bool isJumping;
    private float jumpTimeCounter;
    private float jumpTime;
    private int jumpCount;

    private bool skillCoolTime;
    private bool isDash;
    private float dashSpeed;
    private float dashTime;

    private float slashTime;

    private float keyDownTime;
    private bool isHeal;
    private bool isHealRunning;

    
    private Animator anim;
    internal Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;
    private Collider2D col;
    
    
    
    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        objectManager = GetComponent<ObjectManager>();
        col = GetComponent<Collider2D>();

        jumpTime = 0.5f;
        skillCoolTime = true;
        dashSpeed = 24f;
        dashTime = 0.2f;
        slashTime = 0.3f;
        keyDownTime = 0f;
        
    }

    void FixedUpdate()
    {
        Move();
    }
    void Update()
    {
        Flip();
        Jump();
        SkillActive();
        SlashActive();
    }

    // ��ų Ȱ��ȭ
    private void SkillActive()
    {
        
        if (Input.GetKey(KeyCode.C) && skillCoolTime)
        {   
            StartCoroutine(Dash());
        }
        
        if (Input.GetKeyDown(KeyCode.A) && skillCoolTime)
        {
            keyDownTime = Time.time;
            isHeal = false;
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (Time.time - keyDownTime >= 0.5)
            {
                if(!isHealRunning)
                    StartCoroutine(Heal());
                isHeal = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.A) && !isHeal)
        {
            StartCoroutine(FireBall());
            StartCoroutine(SkillBullet());
        }
    }

    // ���� Ȱ��ȭ
    private void SlashActive()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                StartCoroutine(UpAttack());
                return;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                StartCoroutine(DownAttack());
                return;
            }
            StartCoroutine(Attack());
        }
    }

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
        if (Input.GetKey(KeyCode.LeftArrow))
            this.transform.localScale = new Vector3(1f, 1f, 1f);
        else if (Input.GetKey(KeyCode.RightArrow))
            this.transform.localScale = new Vector3(-1f, 1f, 1f);
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
            jumpCount = 2;
        }
        

        if((isGround == true || jumpCount == 1) && Input.GetKeyDown(KeyCode.Z))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rigid.velocity = Vector2.up * jumpPower;
            
        }
        // ���� ����
        if((isGround == false && jumpCount == 1) && Input.GetKeyDown(KeyCode.Z))
        {
            StartCoroutine(DoubleJump());
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
        
        if (Input.GetKeyUp(KeyCode.Z))
        {
            --jumpCount;
            if(jumpCount == 0)
            {
                isJumping = false;
            }
        }
    }
    IEnumerator DoubleJump()
    {
        float jumpTime = 0.3f;
        GameObject doubleJump = ObjectManager.instance.DoubleJumpEffectPooledObject();
        if(doubleJump != null)
        {
            doubleJump.transform.position = effectPosition.position;
            Vector3 scale = doubleJump.transform.localScale;
            scale.x = transform.localScale.x;
            doubleJump.transform.localScale = scale;
            doubleJump.SetActive(true);
        }
        yield return new WaitForSeconds(jumpTime);
        doubleJump.SetActive(false);
    }
    IEnumerator Dash()
    {
        // �߷� ����
        float originalGravity = rigid.gravityScale;
        rigid.gravityScale = 0f;

        skillCoolTime = false;
        isDash = true;


        if(rigid.velocity.x == 0)
        {
            if (transform.localScale.x == -1)
                rigid.velocity = new Vector2(transform.localScale.x * (-1) * dashSpeed, 0f);
            else
                rigid.velocity = new Vector2(transform.localScale.x * (-1) * dashSpeed, 0f);
        }

        else
        {
            rigid.velocity = new Vector2(moveDirection * dashSpeed, 0f);
        }

        // �뽬 ����Ʈ ����
        GameObject dash = ObjectManager.instance.DashPooledObject();
        if (dash != null)
        {
            dash.transform.position = effectPosition.position;
            Vector3 scale = dash.transform.localScale;
            scale.x = transform.localScale.x;
            dash.transform.localScale = scale;
            dash.SetActive(true);
        }
        GameObject dashSecond = ObjectManager.instance.DashPooledObject();
        if(dashSecond != null)
        {
            dashSecond.transform.position = effectPosition.position;
            Vector3 scale = dashSecond.transform.localScale;
            scale.x = transform.localScale.x * (-1);
            dashSecond.transform.localScale = scale;
            dashSecond.SetActive(true);
        }

        yield return new WaitForSeconds(dashTime);
        rigid.gravityScale = originalGravity;
        isDash = false;
        dash.SetActive(false);
        dashSecond.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        skillCoolTime = true;
    }

    IEnumerator Attack()
    {
        // Slash ����Ʈ ����
        GameObject slash = ObjectManager.instance.SlashPooledObject();
        if (slash != null)
        {
            slash.transform.position = attackPositionX.position;
            // ���� ����
            attackPositionX.GetComponent<Collider2D>().enabled = true;
            Vector3 scale = slash.transform.localScale;
            scale.x = transform.localScale.x;
            slash.transform.localScale = scale;
            slash.SetActive(true);
        }
        
       
        yield return new WaitForSeconds(slashTime);
        slash.SetActive(false);
        attackPositionX.GetComponent<Collider2D>().enabled = false;
    }

    IEnumerator UpAttack()
    {
        // UpSlash ����Ʈ ����
        GameObject upSlash = ObjectManager.instance.SlashUpPooledObject();
        if(upSlash != null)
        {
            upSlash.transform.position = attackUpPosition.position;
            // ���� ����
            attackUpPosition.GetComponent<Collider2D>().enabled = true;
            upSlash.SetActive(true);
        }

        yield return new WaitForSeconds(slashTime);
        upSlash.SetActive(false);
        attackUpPosition.GetComponent<Collider2D>().enabled = false;
    }
    IEnumerator DownAttack()
    {
        //DownSlash ����Ʈ ����
        GameObject downSlash = ObjectManager.instance.SlashDownPooledObject();
        if(downSlash != null)
        {
            downSlash.transform.position = attackDownPosition.position;
            //���� ����
            attackDownPosition.GetComponent<Collider2D>().enabled = true;
            downSlash.SetActive(true);
        }
        yield return new WaitForSeconds(slashTime);
        downSlash.SetActive(false);
        attackDownPosition.GetComponent<Collider2D>().enabled = false;
    }

    IEnumerator FireBall()
    {
        float skillTime = 0.3f;
        skillCoolTime = false;
        // ��ų ����Ʈ ����
        GameObject skill = ObjectManager.instance.SkillPooledObject();
        if(skill != null)
        {
            skill.transform.position = skillPosition.position;
            Vector3 scale = skill.transform.localScale;
            scale.x = transform.localScale.x * (-1);
            skill.transform.localScale = scale;
            skill.SetActive(true);
        }

        // ��ų ��� �� ���� ����
        Debug.Log("���� 25�� �����Ͽ����ϴ�.");
        yield return new WaitForSeconds(skillTime);
        skill.SetActive(false);
        skillCoolTime = true;
    }
    IEnumerator SkillBullet()
    {
        float bulletTime = 1f;
        float bulletSpeed = 30f;
        // ��ų �ҷ� ����
        GameObject bullet = ObjectManager.instance.SkillBulletPooledObject();
        if (bullet != null)
        {
            bullet.transform.position = skillPosition.position;
            Vector3 scale = bullet.transform.localScale;
            scale.x = transform.localScale.x * (-1);
            bullet.transform.localScale = scale;
            bullet.SetActive(true);
        }

        if(transform.localScale.x < 0)
            bullet.transform.GetComponent<Rigidbody2D>().velocity = Vector3.right * bulletSpeed;
        else
            bullet.transform.GetComponent<Rigidbody2D>().velocity = Vector3.left * bulletSpeed;

        yield return new WaitForSeconds(bulletTime);
        bullet.SetActive(false);
    }
    IEnumerator Heal()
    {
        isHealRunning = true;
        float healTime = 1f;
        float startTime = Time.time;
        skillCoolTime = false;
        // �� ����Ʈ ����
        GameObject heal = ObjectManager.instance.HealEffectPooledObject();
        if(heal != null)
        {
            heal.transform.position = effectPosition.position;
            heal.SetActive(true);
        }
        while (isHeal)
        {
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
            {
                // �� ��� �� ����Ű�� ���� ���
                isHeal = false;
                break;
            }
            else if (Time.time > startTime + healTime)
            {
                // �� ��� ����
                isHeal = false;
                //TODO : UI������ �Բ� ����
                Debug.Log("����� �ϳ� �þ���ϴ�.");
                Debug.Log("������ 20 ���Ǿ����ϴ�.");
                skillCoolTime = true;
                
            }
            yield return null;
        }
        heal.SetActive(false);
        isHealRunning = false;
        
        Debug.Log("����");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            //TODO : ���Ŀ� ���� ���� �� ���ݰ� �˹��� ���� ������ ����
            
            
        }
    }
}

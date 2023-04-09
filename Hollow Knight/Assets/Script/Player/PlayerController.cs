using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Pool;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] Transform pos;
    [SerializeField] float checkRadious;
    [SerializeField] LayerMask islayer;
    [SerializeField] Transform effectPosition;
    [SerializeField] Transform attackPositionX;
    [SerializeField] Transform attackUpPosition;
    [SerializeField] Transform attackDownPosition;
    [SerializeField] Transform skillPosition;
    
    
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

        if(Input.GetKeyDown(KeyCode.A) && skillCoolTime)
        {
            StartCoroutine(FireBall());
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
            jumpCount = 1;
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
        yield return new WaitForSeconds(skillTime);
        skill.SetActive(false);
        skillCoolTime = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //TODO : ���Ŀ� ���� ���� �� ���ݰ� �˹��� ���� ������ ����
            Debug.Log("20 �������� ����");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private Transform pos;
    [SerializeField] private float checkRadious;
    [SerializeField] private LayerMask islayer;
    [SerializeField] private UIManager uiManager;
    
    private int moveDirection;

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

    private int life;

    private SlashEffect _slashEffect;
    private UpSlashEffect _upSlashEffect;
    private DownSlashEffect _downSlashEffect;
    private Animator anim;
    internal Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;
    private Collider2D col;
    
    
    
    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        _slashEffect = GetComponentInChildren<SlashEffect>();
        _upSlashEffect = GetComponentInChildren<UpSlashEffect>();
        _downSlashEffect = GetComponentInChildren<DownSlashEffect>();


        jumpTime = 0.5f;
        skillCoolTime = true;
        dashSpeed = 24f;
        dashTime = 0.2f;
        slashTime = 0.3f;
        keyDownTime = 0f;
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
    }

    // ��ų Ȱ��ȭ
    private void SkillActive()
    {
        
        //if (Input.GetKey(KeyCode.C) && skillCoolTime)
        //{   
        //    StartCoroutine(Dash());
        //}
        
        if (Input.GetKeyDown(KeyCode.A) && skillCoolTime)
        {
            keyDownTime = Time.time;
            isHeal = false;
        }

        //if (Input.GetKeyUp(KeyCode.A) && !isHeal)
        //{
        //    StartCoroutine(FireBall());
        //    StartCoroutine(SkillBullet());
        //}
    }

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
            anim.SetBool("isJump", false);
            anim.SetBool("isJumpDown", false);
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

    //IEnumerator Dash()
    //{
    //    // �߷� ����
    //    float originalGravity = rigid.gravityScale;
    //    rigid.gravityScale = 0f;

    //    skillCoolTime = false;
    //    isDash = true;


    //    if(rigid.velocity.x == 0)
    //    {
    //        if (transform.localScale.x == -1)
    //            rigid.velocity = new Vector2(transform.localScale.x * (-1) * dashSpeed, 0f);
    //        else
    //            rigid.velocity = new Vector2(transform.localScale.x * (-1) * dashSpeed, 0f);
    //    }

    //    else
    //    {
    //        rigid.velocity = new Vector2(moveDirection * dashSpeed, 0f);
    //    }

    //    // �뽬 ����Ʈ ����
    //    GameObject dash = ObjectManager.instance.DashPooledObject();
    //    if (dash != null)
    //    {
    //        dash.transform.position = effectPosition.position;
    //        Vector3 scale = dash.transform.localScale;
    //        scale.x = transform.localScale.x;
    //        dash.transform.localScale = scale;
    //        dash.SetActive(true);
    //    }
    //    GameObject dashSecond = ObjectManager.instance.DashPooledObject();
    //    if(dashSecond != null)
    //    {
    //        dashSecond.transform.position = effectPosition.position;
    //        Vector3 scale = dashSecond.transform.localScale;
    //        scale.x = transform.localScale.x * (-1);
    //        dashSecond.transform.localScale = scale;
    //        dashSecond.SetActive(true);
    //    }

    //    yield return new WaitForSeconds(dashTime);
    //    rigid.gravityScale = originalGravity;
    //    isDash = false;
    //    dash.SetActive(false);
    //    dashSecond.SetActive(false);
    //    yield return new WaitForSeconds(0.5f);
    //    skillCoolTime = true;
    //}



    //IEnumerator FireBall()
    //{
    //    float skillTime = 0.3f;
    //    skillCoolTime = false;
    //    // ��ų ����Ʈ ����
    //    GameObject skill = ObjectManager.instance.SkillPooledObject();
    //    if(skill != null)
    //    {
    //        skill.transform.position = skillPosition.position;
    //        Vector3 scale = skill.transform.localScale;
    //        scale.x = transform.localScale.x * (-1);
    //        skill.transform.localScale = scale;
    //        skill.SetActive(true);
    //    }

    //    // ��ų ��� �� ���� ����
    //    uiManager.ConsumptionMpIcon(0.3f);
    //    yield return new WaitForSeconds(skillTime);
    //    skill.SetActive(false);
    //    skillCoolTime = true;
    //}
    //IEnumerator SkillBullet()
    //{
    //    float bulletTime = 1f;
    //    float bulletSpeed = 30f;
    //    // ��ų �ҷ� ����
    //    GameObject bullet = ObjectManager.instance.SkillBulletPooledObject();
    //    if (bullet != null)
    //    {
    //        bullet.transform.position = skillPosition.position;
    //        Vector3 scale = bullet.transform.localScale;
    //        scale.x = transform.localScale.x * (-1);
    //        bullet.transform.localScale = scale;
    //        bullet.SetActive(true);
    //    }

    //    if(transform.localScale.x < 0)
    //        bullet.transform.GetComponent<Rigidbody2D>().velocity = Vector3.right * bulletSpeed;
    //    else
    //        bullet.transform.GetComponent<Rigidbody2D>().velocity = Vector3.left * bulletSpeed;

    //    yield return new WaitForSeconds(bulletTime);
    //    bullet.SetActive(false);
    //}

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Monster"))
        {
            life--;
            uiManager.UpdateLifeIcon(life);
        }
    }


}

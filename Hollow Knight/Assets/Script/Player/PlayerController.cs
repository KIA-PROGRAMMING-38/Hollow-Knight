using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
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

    // 스킬 활성화
    private void SkillActive()
    {
        if (Input.GetKey(KeyCode.C) && skillCoolTime)
        {   
            StartCoroutine(Dash());
        }

        if(Input.GetKeyDown(KeyCode.A) && skillCoolTime)
        {
            StartCoroutine(FireBall());
            StartCoroutine(SkillBullet());
        }
    }

    // 공격 활성화
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
        // 대쉬 중 이동을 하지 않는다.
        if (isDash)
            return;
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            // 움직이지 않는다.
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
        // 대쉬 중 점프를 하지 않는다.
        if (isDash) 
            return;

        // 지면을 확인
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

        // 홀딩 시 점프 높이 상승
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
        // 중력 삭제
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

        // 대쉬 이펙트 생성
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
        // Slash 이펙트 생성
        GameObject slash = ObjectManager.instance.SlashPooledObject();
        if (slash != null)
        {
            slash.transform.position = attackPositionX.position;
            // 공격 판정
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
        // UpSlash 이펙트 생성
        GameObject upSlash = ObjectManager.instance.SlashUpPooledObject();
        if(upSlash != null)
        {
            upSlash.transform.position = attackUpPosition.position;
            // 공격 판정
            attackUpPosition.GetComponent<Collider2D>().enabled = true;
            upSlash.SetActive(true);
        }

        yield return new WaitForSeconds(slashTime);
        upSlash.SetActive(false);
        attackUpPosition.GetComponent<Collider2D>().enabled = false;
    }
    IEnumerator DownAttack()
    {
        //DownSlash 이펙트 생성
        GameObject downSlash = ObjectManager.instance.SlashDownPooledObject();
        if(downSlash != null)
        {
            downSlash.transform.position = attackDownPosition.position;
            //공격 판정
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
        // 스킬 이펙트 생성
        GameObject skill = ObjectManager.instance.SkillPooledObject();
        if(skill != null)
        {
            skill.transform.position = skillPosition.position;
            Vector3 scale = skill.transform.localScale;
            scale.x = transform.localScale.x * (-1);
            skill.transform.localScale = scale;
            skill.SetActive(true);
        }

        // 스킬 사용 후 마나 감소
        Debug.Log("마나 25가 감소하였습니다.");
        yield return new WaitForSeconds(skillTime);
        skill.SetActive(false);
        skillCoolTime = true;
    }
    IEnumerator SkillBullet()
    {
        float bulletTime = 1f;
        float bulletSpeed = 30f;
        // 스킬 불렛 생성
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //TODO : 추후에 몬스터 구현 시 공격과 넉백을 같이 구현할 예정
            Debug.Log("20 데미지를 입힘");
        }
    }
}

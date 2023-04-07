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
    [SerializeField] Transform dashPosition;
    
    
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

    private float attackTime;
    private bool isAttack;
    Animator anim;
    internal Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Collider2D col;
    
    
    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        objectManager = GetComponent<ObjectManager>();

        jumpTime = 0.5f;
        skillCoolTime = true;
        dashSpeed = 24f;
        dashTime = 0.2f;
        
        attackTime = 0.2f;
        
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
    }

    // 스킬 활성화
    private void SkillActive()
    {
        if (Input.GetKey(KeyCode.C) && skillCoolTime)
        {   
            StartCoroutine(Dash());
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
            spriteRenderer.flipX = true;
        else if (Input.GetKey(KeyCode.RightArrow))
            spriteRenderer.flipX = false;
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
            if (spriteRenderer.flipX == true)
                rigid.velocity = new Vector2(transform.localScale.x * dashSpeed, 0f);
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
            dash.transform.position = dashPosition.position;
            Vector3 scale = dash.transform.localScale;
            scale.x = spriteRenderer.flipX == true ? 1 : -1;
            dash.transform.localScale = scale;
            dash.SetActive(true);
        }
        GameObject dashSecond = ObjectManager.instance.DashPooledObject();
        if(dashSecond != null)
        {
            dashSecond.transform.position = dashPosition.position;
            Vector3 scale = dashSecond.transform.localScale;
            scale.x = spriteRenderer.flipX == true ? -1 : 1;
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


}

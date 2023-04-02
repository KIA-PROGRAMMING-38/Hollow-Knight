using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] Transform pos;
    [SerializeField] float checkRadious;
    [SerializeField] LayerMask islayer;

    internal bool isGround;
    internal bool isJumping;
    private float jumpTimeCounter;
    internal float jumpTime;
    internal int jumpCount;
    Animator anim;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Collider2D col;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        jumpTime = 0.5f;
    }

    void FixedUpdate()
    {
        Move();
        Flip();
    }
    void Update()
    {
        Jump();

    }

    void Move()
    {
        
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            // �������� �ʴ´�.
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
            rigid.velocity = new Vector2(speed * -1, rigid.velocity.y);
            
        
        else if (Input.GetKey(KeyCode.RightArrow))
            rigid.velocity = new Vector2(speed, rigid.velocity.y);
           
        
        
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

    
}

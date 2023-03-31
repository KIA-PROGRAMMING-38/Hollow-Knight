using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    Animator anim;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    
    void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        Move();
        Flip();
    }
    void Update()
    {
        
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            // Don't Move
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        else if (Input.GetKey(KeyCode.RightArrow))
            transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    public void Flip()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            spriteRenderer.flipX = true;
        else if (Input.GetKey(KeyCode.RightArrow))
            spriteRenderer.flipX = false;
    }
    
}

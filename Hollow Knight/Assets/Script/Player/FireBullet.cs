using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FireBullet : MonoBehaviour
{
    private Animator anim;
    
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            anim.SetTrigger("isHit");
        }
    }
}

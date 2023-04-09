using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //TODO : 스킬 데미지 수치 및 히트 이펙트 추가는 몬스터 구현과 같이 예정
            Debug.Log("40 데미지를 입혔습니다.");
        }
    }
   
}

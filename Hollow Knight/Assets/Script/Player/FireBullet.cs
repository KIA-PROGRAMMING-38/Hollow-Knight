using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            //TODO : ��ų ������ ��ġ �� ��Ʈ ����Ʈ �߰��� ���� ������ ���� ����
            Debug.Log("40 �������� �������ϴ�.");
        }
    }
   
}

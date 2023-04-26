using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager instance;
    
    [SerializeField] private GameObject skillBulletPrefab;
    private List<GameObject> bulletPool = new List<GameObject>();
    private int bullet = 5;
    

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        Bullet();
    }

    private void Bullet()
    {
        for (int index = 0; index < bullet; ++index)
        {
            GameObject obj = Instantiate(skillBulletPrefab);
            obj.SetActive(false);
            bulletPool.Add(obj);
        }
    }
    
    public GameObject BulletPooledObject()
    {
        for(int index = 0; index < bulletPool.Count; ++index)
        {
            if (!bulletPool[index].activeInHierarchy)
            {
                return bulletPool[index];
            }
        }
        return null;
    }
   
  
}

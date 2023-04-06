using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager instance;

    private List<GameObject> dashEffectPool = new List<GameObject>();
    private List<GameObject> dashSecondEffectPool = new List<GameObject>();
    private int dashEffect = 1;
    private int dashSecondEffect = 1;
    
    [SerializeField] private GameObject dashEffectPrefab;
    [SerializeField] private GameObject dashSecondEffectPrefab;
    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        Dash();
    }

    private void Dash()
    {
        for (int index = 0; index < dashEffect; ++index)
        {
            GameObject obj = Instantiate(dashEffectPrefab);
            obj.SetActive(false);
            dashEffectPool.Add(obj);
        }
        for (int index = 0; index < dashSecondEffect; ++index)
        {
            GameObject obj = Instantiate(dashSecondEffectPrefab);
            obj.SetActive(false);
            dashSecondEffectPool.Add(obj);
        }
    }
    public GameObject DashPooledObject()
    {
        for(int index = 0; index < dashEffectPool.Count; ++index)
        {
            if (!dashEffectPool[index].activeInHierarchy)
            {
                return dashEffectPool[index];
            }
            
            else if (!dashSecondEffectPool[index].activeInHierarchy)
            {
                return dashSecondEffectPool[index];
            }
        }

        return null;
    }

}

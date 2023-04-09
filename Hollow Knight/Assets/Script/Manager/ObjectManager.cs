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

    private List<GameObject> slashEffectPool = new List<GameObject>();
    private List<GameObject> slashUpEffectPool = new List<GameObject>();
    private List<GameObject> slashDownEffectPool = new List<GameObject>();
    private int slashEffect = 2;
    private int slashUpEffect = 2;
    private int slashDownEffect = 2;

    private List<GameObject> skillEffectPool = new List<GameObject>();
    private List<GameObject> skillBulletPool = new List<GameObject>();
    private int skillEffect = 2;
    private int skillBullet = 5;

    private List<GameObject> healEffectPool = new List<GameObject>();
    private int healEffect = 2;

    [SerializeField] private GameObject dashEffectPrefab;
    [SerializeField] private GameObject dashSecondEffectPrefab;
    [SerializeField] private GameObject slashEffectPrefab;
    [SerializeField] private GameObject slashUpEffectPrefab;
    [SerializeField] private GameObject slashDownEffectPrefab;
    [SerializeField] private GameObject skillEffectPrefab;
    [SerializeField] private GameObject skillBulletPrefab;
    [SerializeField] private GameObject healEffectPrefab;

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
        Attack();
        UpAttack();
        DownAttack();
        Skill();
        SkillBullet();
        HealEffect();
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

    private void Attack()
    {
        for(int index = 0; index < slashEffect; ++index)
        {
            GameObject obj = Instantiate(slashEffectPrefab);
            obj.SetActive(false);
            slashEffectPool.Add(obj);
        }
    }
    private void UpAttack()
    {
        for(int index = 0; index < slashUpEffect; ++index)
        {
            GameObject obj = Instantiate(slashUpEffectPrefab);
            obj.SetActive(false);
            slashUpEffectPool.Add(obj);
        }
    }
    private void DownAttack()
    {
        for(int index = 0; index < slashDownEffect; ++index)
        {
            GameObject obj = Instantiate(slashDownEffectPrefab);
            obj.SetActive(false);
            slashDownEffectPool.Add(obj);
        }
    }
    private void Skill()
    {
        for(int index = 0; index < skillEffect; ++index)
        {
            GameObject obj = Instantiate(skillEffectPrefab);
            obj.SetActive(false);
            skillEffectPool.Add(obj);
        }
    }
    private void SkillBullet()
    {
        for(int index = 0; index < skillBullet; ++index)
        {
            GameObject obj = Instantiate(skillBulletPrefab);
            obj.SetActive(false);
            skillBulletPool.Add(obj);
        }
    }
    private void HealEffect()
    {
        for(int index = 0; index < healEffect; ++index)
        {
            GameObject obj = Instantiate(healEffectPrefab);
            obj.SetActive(false);
            healEffectPool.Add(obj);
        }
    }
    public GameObject SlashPooledObject()
    {
        for(int index = 0; index < slashEffectPool.Count; ++index)
        {
            if (!slashEffectPool[index].activeInHierarchy)
            {
                return slashEffectPool[index];
            }
        }
        return null;
    }
    public GameObject SlashUpPooledObject()
    {
        for(int index = 0; index < slashUpEffectPool.Count; ++index)
        {
            if (!slashUpEffectPool[index].activeInHierarchy)
            {
                return slashUpEffectPool[index];
            }
        }
        return null;    
    }
    public GameObject SlashDownPooledObject()
    {
        for(int index = 0; index < slashDownEffectPool.Count; ++index)
        {
            if (!slashDownEffectPool[index].activeInHierarchy)
            {
                return slashDownEffectPool[index];
            }
        }
        return null;
    }
    public GameObject SkillPooledObject()
    {
        for(int index = 0; index < skillEffectPool.Count; ++index)
        {
            if (!skillEffectPool[index].activeInHierarchy)
            {
                return skillEffectPool[index];
            }
        }
        return null;
    }
    public GameObject SkillBulletPooledObject()
    {
        for(int index = 0; index < skillBulletPool.Count; ++index)
        {
            if (!skillBulletPool[index].activeInHierarchy)
            {
                return skillBulletPool[index];
            }
        }
        return null;
    }
    public GameObject HealEffectPooledObject()
    {
        for(int index = 0; index < healEffectPool.Count; ++index)
        {
            if (!healEffectPool[index].activeInHierarchy)
            {
                return healEffectPool[index];
            }
        }
        return null;
    }
}

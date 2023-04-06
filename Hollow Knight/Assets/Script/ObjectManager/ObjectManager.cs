using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject dashEffectPreFab;
    private int poolSize = 10;

    private Queue<GameObject> queue = new Queue<GameObject>();
    void Start()
    {
        for(int i = 0; i < poolSize; ++i)
        {
            GameObject dashEffect = Instantiate(dashEffectPreFab, transform);
            dashEffect.SetActive(false);
            queue.Enqueue(dashEffect);
        }
    }

    public void PlayEffect(Vector3 position)
    {
        if(queue.Count > 0)
        {
            GameObject dashEffect = queue.Dequeue();
            dashEffect.transform.position = position;
            dashEffect.SetActive(true);
            StartCoroutine(DeactivateEffect(dashEffect));
        }

        else
        {
            GameObject dashEffect = Instantiate(dashEffectPreFab, position, Quaternion.identity, transform);
            StartCoroutine(DeactivateEffect(dashEffect));
        }
    }

    private IEnumerator DeactivateEffect(GameObject dashEffect)
    {
        yield return new WaitForSeconds(dashEffect.GetComponent<ParticleSystem>().main.duration);
        dashEffect.SetActive(false);
        queue.Enqueue(dashEffect);
    }
    
}

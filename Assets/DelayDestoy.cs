using System.Collections;
using UnityEngine;

public class DelayDestoy : MonoBehaviour
{
    public void init(bool pool)
    {
        if (pool) StartCoroutine(destroyPool());
        else StartCoroutine(destroy());
    }

    IEnumerator destroy()
    {
        yield return new WaitForSeconds(1f);
        BulletCounter.instance.count--;
        Destroy(gameObject);

    }

    IEnumerator destroyPool()
    {
        yield return new WaitForSeconds(1f);
        BulletCounter.instance.count--;
        BulletPool.instance.addToPool(gameObject);
    }
}

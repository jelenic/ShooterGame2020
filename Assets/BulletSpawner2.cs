using System.Collections;
using UnityEngine;

public class BulletSpawner2 : MonoBehaviour
{
    public float frequency;
    public int spawnPositionsNum;
    private BulletPool pool;
    private BulletCounter counter;

    public bool enabled;



    private void Awake()
    {
        if (enabled)
        {
            pool = BulletPool.instance;
            StartCoroutine(getPool());
        }
    }

    IEnumerator getPool()
    {
        while (counter == null)
        {
            counter = BulletCounter.instance;
            yield return new WaitForSeconds(0.1f);
        }

        while (pool == null)
        {
            pool = BulletPool.instance;
            yield return new WaitForSeconds(0.1f);
        }

        for (int i = 0; i < spawnPositionsNum; i++)
        {
            Vector2 pos = 5f * Random.insideUnitCircle;
            Quaternion rot = Random.rotation;
            StartCoroutine(spawnLoop(pos, rot));
        }
    }

    IEnumerator spawnLoop(Vector2 pos, Quaternion rot)
    {
        WaitForSeconds wait = new WaitForSeconds(frequency);
        yield return new WaitForSeconds(Random.Range(1f, 3f));


        while (true)
        {
            var bullet = pool.getFromPool();
            bullet.transform.position = pos;
            bullet.transform.rotation = rot;
            bullet.GetComponent<DelayDestoy>().init(true);
            counter.count++;
            yield return wait;
        }
    }

}

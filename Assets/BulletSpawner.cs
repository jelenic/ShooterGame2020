using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bullet;
    public float frequency;

    public int spawnPositionsNum;
    private BulletCounter counter;

    public bool enabled;



    private void Awake()
    {
        if (enabled) StartCoroutine(setUp());
    }

    IEnumerator setUp()
    {
        while (counter == null)
        {
            counter = BulletCounter.instance;
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
        yield return new WaitForSeconds(Random.Range(1f, 10f));
        WaitForSeconds wait = new WaitForSeconds(frequency);

        while (true)
        {
            Instantiate(bullet, pos, rot).GetComponent<DelayDestoy>().init(false);
            counter.count++;
            yield return wait;
        }
    }

}

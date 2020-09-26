using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerArcade : MonoBehaviour
{
    private ArcadeManager arcadeManager;

    private Coroutine spawnerLoopCor;

    public float spawnRate;
    private float spawnRandomOffset;

    private Transform transform;

    private void Awake()
    {
        transform = GetComponent<Transform>();
        spawnRandomOffset = spawnRate * 0.2f;
        StartCoroutine(setArcadeManager());
    }

    private IEnumerator setArcadeManager() // necessary since arcade manager is created after spawners
    {
        yield return new WaitForSeconds(2f);
        arcadeManager = ArcadeManager.instance;
        arcadeManager.OnWaveStartedCallback += onWaveStart;
        arcadeManager.OnWaveEndedCallback += onWaveEnd;
    }

    private void onWaveStart(int wave)
    {
        spawnerLoopCor = StartCoroutine(spawnerLoop());
    }
    private void onWaveEnd()
    {
        StopCoroutine(spawnerLoopCor);
    }


    IEnumerator spawnerLoop()
    {
        yield return new WaitForSeconds(1f);

        while (true)
        {
            Vector3 randomPos = Random.insideUnitCircle*5f;
            randomPos += transform.position;
            Debug.Log("spawning enemy at " + (randomPos));
            arcadeManager.summonRandomEnemy(randomPos);
            yield return new WaitForSeconds(spawnRate + Random.Range(-spawnRandomOffset, spawnRandomOffset));
        }
    }


}

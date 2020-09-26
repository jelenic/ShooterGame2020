using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerArcade : MonoBehaviour
{
    private ArcadeManager arcadeManager;

    private Coroutine spawnerLoopCor;

    private Transform transform;

    private void Awake()
    {
        transform = GetComponent<Transform>();
        StartCoroutine(setArcadeManager());
    }

    private IEnumerator setArcadeManager() // necessary since arcade manager is created after spawners
    {
        yield return new WaitForSeconds(2f);
        arcadeManager = ArcadeManager.instance;
        arcadeManager.OnWaveStartedCallback += onWaveStart;
        arcadeManager.OnWaveEndedCallback += onWaveEnd;
    }

    private void onWaveStart(float spawnRate, int maxSpawn)
    {
        spawnerLoopCor = StartCoroutine(spawnerLoop(spawnRate, maxSpawn));
    }
    private void onWaveEnd()
    {
        StopCoroutine(spawnerLoopCor);
    }


    IEnumerator spawnerLoop(float spawnRate, int maxSpawn)
    {
        yield return new WaitForSeconds(Random.Range(1f, 4f));
        int spawned = 0;

        while (spawned < maxSpawn)
        {
            for(int i = 0; i < 3; i++) // can spawn 3 enemies at once if lucky
            {
                if (Random.Range(0f,1f) <= spawnRate)
                {
                    Vector3 randomPos = Random.insideUnitCircle * 5f;
                    randomPos += transform.position;
                    Debug.Log("spawning enemy at " + (randomPos));
                    arcadeManager.summonRandomEnemy(randomPos);
                }
                
                yield return new WaitForSeconds(1f);
            }
            
            yield return new WaitForSeconds(Random.Range(3f, 10f));
        }
    }


}

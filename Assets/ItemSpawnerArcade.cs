using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnerArcade : MonoBehaviour
{
    private ArcadeManager arcadeManager;
    public GameObject[] possibleItems;

    private Coroutine spawnerLoopCor;

    public float spawnRate;

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

    private void onWaveStart(int wave)
    {
        spawnerLoopCor = StartCoroutine(spawnerLoop());
    }
    private void onWaveEnd()
    {
        StopCoroutine(spawnerLoopCor);
    }

    private void spawnRandomItem()
    {
        Vector3 randomPos = Random.insideUnitCircle * 5f;
        Debug.Log("spawning item at " + (transform.position + randomPos));

        GameObject item = possibleItems[Random.Range(0, possibleItems.Length)];

        Instantiate(item, transform.position + randomPos, Quaternion.identity);

    }


    IEnumerator spawnerLoop()
    {
        yield return new WaitForSeconds(1f);

        while (true)
        {
            spawnRandomItem();
            yield return new WaitForSeconds(spawnRate);
        }
    }


}

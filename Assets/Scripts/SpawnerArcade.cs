﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerArcade : MonoBehaviour
{
    private ArcadeManager arcadeManager;
    public GameObject enemy;

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


    IEnumerator spawnerLoop()
    {
        yield return new WaitForSeconds(1f);

        while (true)
        {
            Vector3 randomPos = Random.insideUnitCircle*5f;
            Debug.Log("spawning enemy at " + (transform.position + randomPos));
            Instantiate(enemy, transform.position + randomPos, Quaternion.identity);
            yield return new WaitForSeconds(spawnRate);
        }
    }


}

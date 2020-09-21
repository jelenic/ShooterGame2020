using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Refresher : MonoBehaviour
{
    AstarPath active;
    Transform player;
    GridGraph gg;
    
    private void Start()
    {
        active = AstarPath.active;
        gg = active.data.gridGraph;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(refresh());
    }

    IEnumerator refresh()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);
            if (player != null) gg.center = player.position;
            active.Scan();
        }
    }
}

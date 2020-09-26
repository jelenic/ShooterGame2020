using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class CustomAIArcade : MonoBehaviour
{


    Stats stats;
    //private float speed;
    private float nextWaypontDistance;

    Path path;
    int currentWaypoint;
    bool reachedEndOfPath;

    Seeker seeker;
    Rigidbody2D rb;
    public Transform target;
    private Transform player;
    private Transform startPosition;

    private Coroutine updatePathCor;
    private bool findingPath;




    private enum State
    {
        goToTargetState,
        maintainRangeAttackState,
        disabled
    }

    private State state;

    void setTarget()
    {
        /*if (player != null && Vector2.Distance(rb.position, player.position) <= stats.range)
        {
            target = player;
        }
        else
        {
            target = startPosition;
        }*/
        target = player;
    }

    private IEnumerator UpdatePath()
    {
        while (true){
            setTarget();

            if (seeker.IsDone())
            {
                seeker.StartPath(rb.position, target.position, OnPathComplete);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }


    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void Start()
    {

        stats = GetComponent<Stats>();
        state = State.goToTargetState;
        reachedEndOfPath = false;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        nextWaypontDistance = 3f;
        currentWaypoint = 0;

        GameObject gb = new GameObject();
        gb.transform.position = rb.position + Random.insideUnitCircle*5;
        gb.transform.rotation = rb.transform.rotation;
        startPosition = gb.transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;

        updatePathCor = StartCoroutine(UpdatePath());
        findingPath = true;

        setTarget();
    }

    private void FixedUpdate()
    {
        if (player != null && Vector2.Distance(rb.position, player.position) <= stats.stoppingDistance)
        {
            state = State.maintainRangeAttackState;
        }
        else if (player != null && Vector2.Distance(rb.position, player.position) >= stats.stoppingDistance *10)
        {
            state = State.goToTargetState;
        }
        //rotate towards player
        Vector2 directionPlayer = target.position - transform.position;
        float angle = Mathf.Atan2(directionPlayer.y, directionPlayer.x) * Mathf.Rad2Deg - 90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, stats.angleSpeed * Time.deltaTime);

        switch (state)
        {
            default:
            case State.goToTargetState:
                #region pathfinder
                //plot path
                if (path == null)
                {
                    return;
                }
                if (currentWaypoint >= path.vectorPath.Count)
                {
                    reachedEndOfPath = true;
                    return;
                }
                else
                {
                    reachedEndOfPath = false;
                }

                Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
                Vector2 force = direction * stats.speed * Time.deltaTime;
                //Debug.Log("Adding force" + force.ToString());
                if (player != null && Vector2.Distance(rb.position, player.position) >= stats.stoppingDistance)
                {
                    rb.AddForce(force);
                }

                float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
                if (distance < nextWaypontDistance)
                {
                    currentWaypoint += 1;
                }

                #endregion

                break;
            case State.maintainRangeAttackState:
                if (player != null && Vector2.Distance(rb.position, player.position) >= (stats.stoppingDistance))
                {
                    Vector2 dir = ((Vector2)player.position - rb.position).normalized;
                    Vector2 f = dir * stats.speed * Time.deltaTime / 4;
                    rb.AddForce(f);
                }

                else if (player != null && Vector2.Distance(rb.position, player.position) <= (stats.stoppingDistance))
                {
                    Vector2 dir = ((Vector2)player.position - rb.position).normalized;
                    Vector2 f = dir * stats.speed * Time.deltaTime / 4;
                    rb.AddForce(-f);
                }


                break;
            case State.disabled:
                break;

        
        }
    }
}

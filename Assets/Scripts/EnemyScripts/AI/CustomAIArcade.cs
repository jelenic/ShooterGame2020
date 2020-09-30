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

    private Coroutine updatePathCor;
    private bool findingPath;

    private float cooldownFlyby;
    private bool flybyReady;


    public enum State
    {
        goToTargetState,
        maintainRangeAttackState,
        disabled,
        flyby
    }

    public State state;

    void setTarget()
    {
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

    private IEnumerator flyByCD()
    {
        flybyReady = false;
        gameObject.layer = 14;
        yield return new WaitForSeconds(0.2f);
        gameObject.layer = 10;
        yield return new WaitForSeconds(Random.Range(3f, 8f));
        flybyReady = true;

    }

    private void Start()
    {
        flybyReady = true;

        stats = GetComponent<Stats>();
        state = State.goToTargetState;
        reachedEndOfPath = false;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        nextWaypontDistance = 3f;
        currentWaypoint = 0;

        player = GameObject.FindGameObjectWithTag("Player").transform;

        updatePathCor = StartCoroutine(UpdatePath());
        findingPath = true;

        cooldownFlyby = 0;

        setTarget();
    }

    private void FixedUpdate()
    {
        //cooldownFlyby -= Time.deltaTime;

        if (player == null) return;
        //rotate towards player
        Vector2 directionPlayer = target.position - transform.position;
        float angle = Mathf.Atan2(directionPlayer.y, directionPlayer.x) * Mathf.Rad2Deg - 90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, stats.angleSpeed * Time.deltaTime);

        switch (state)
        {

            #region caseSwitchStateMachine
            default:
                state = State.goToTargetState;
                break;
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

                if (player != null && Vector2.Distance(rb.position, player.position) <= stats.stoppingDistance)
                {
                    state = State.maintainRangeAttackState;
                }

                break;
            case State.maintainRangeAttackState:
                float distanceToPlayer = Vector2.Distance(rb.position, player.position);
                if (distanceToPlayer >= (stats.stoppingDistance))
                {
                    Vector2 dir = ((Vector2)player.position - rb.position).normalized;
                    Vector2 f = dir * stats.speed * Time.deltaTime / 4;
                    rb.AddForce(f);
                }

                else if (player != null && distanceToPlayer <= (stats.stoppingDistance))
                {
                    if (stats.flyby && flybyReady)
                    {
                        state = State.flyby;
                    }
                    Vector2 dir = ((Vector2)player.position - rb.position).normalized;
                    Vector2 f = dir * stats.speed * Time.deltaTime / 4;
                    rb.AddForce(-f);
                }

                //if (player != null && Vector2.Distance(rb.position, player.position) <= stats.stoppingDistance && stats.flyby )
                //{
                //    int rand = Random.Range(0, 10);
                //    if (flybyReady)
                //    {
                //        //cooldownFlyby = rand;
                //        state = State.flyby;
                //    }
                //}

                if (Vector2.Distance(rb.position, player.position) >= stats.stoppingDistance * 3)
                {
                    state = State.goToTargetState;
                }

                break;
            case State.disabled:
                break;

            case State.flyby:
                Vector3 dirF = (player.position - transform.position).normalized;
                Debug.Log("flying by1 " + dirF);
                dirF += Vector3.Cross(dirF, new Vector3(0, 0, 1)) * 0.6f * (Random.value > 0.5 ? 1f : -1f);
                //Debug.Log("flying by2 " + dirF);
                StartCoroutine(flyByCD());
                rb.AddForce(dirF * stats.speed * Time.deltaTime * 35);

                if (player != null && Vector2.Distance(rb.position, player.position) <= stats.stoppingDistance)
                {
                    state = State.maintainRangeAttackState;
                }

                else if (player != null && Vector2.Distance(rb.position, player.position) >= stats.stoppingDistance * 3)
                {
                    state = State.goToTargetState;
                }

                break;

                #endregion


        }
    }
}

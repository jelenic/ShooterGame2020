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
    public bool flybyReady;

    public float stateUpdateTime;
    public float mainLoopFrequency;
    private CombatVariables cv;

    protected IEnumerator updateState(float frequency)
    {
        WaitForSeconds waitingTime = new WaitForSeconds(frequency);
        yield return waitingTime;
        while (true)
        {
            if (player == null) break;
            checkHP();
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            //Debug.Log("distance to player " + distanceToPlayer);
            if (state.Equals(State.goToTargetState) && distanceToPlayer <= 1.3f*stats.stoppingDistance) state = State.maintainRangeAttackState;
            else if (state.Equals(State.maintainRangeAttackState))
            {
                if (distanceToPlayer >= 1.3f*stats.stoppingDistance) state = State.goToTargetState;
                else if (distanceToPlayer <= 0.6f*stats.stoppingDistance) state = State.backAway;
            }
            else if (state.Equals(State.backAway)) 
            { 
                if (distanceToPlayer >= 0.6f * stats.stoppingDistance) state = State.maintainRangeAttackState;
                else if (distanceToPlayer >= 1.3f * stats.stoppingDistance) state = State.goToTargetState;
            }
            yield return waitingTime;
        }
    }

    protected IEnumerator mainLoop(float frequency) {
        WaitForSeconds waitingTime = new WaitForSeconds(frequency);

        int moveDirection = 0;
        int directionDuration = (int)(2 * 10 / frequency);

        while(true)
        {
            if (player == null) break;

            if (rb.velocity.sqrMagnitude > 175f) yield return waitingTime;

            Vector3 directionToPlayer = (player.position - transform.position);
            float distanceToPlayer = directionToPlayer.magnitude;
            switch (state)
            {
                case State.goToTargetState:
                    rb.AddForce(directionToPlayer * 0.025f * stats.speed * frequency * (distanceToPlayer / stats.stoppingDistance));
                    break;
                case State.maintainRangeAttackState:
                    moveDirection = (moveDirection + 1) % directionDuration;
                    directionToPlayer = Quaternion.Euler(0, 0, 70 * (moveDirection > (directionDuration / 2) ? 1 : -1)) * directionToPlayer;
                    rb.AddForce(directionToPlayer.normalized * stats.speed * frequency * 0.2f);
                    break;
                case State.backAway:
                    moveDirection = (moveDirection + 1) % directionDuration;
                    directionToPlayer = Quaternion.Euler(0, 0, 50 * (moveDirection > (directionDuration / 2) ? 1 : -1)) * directionToPlayer;
                    rb.AddForce(-directionToPlayer * 0.04f * stats.speed * frequency * (stats.stoppingDistance / distanceToPlayer));
                    break;
            }
            yield return waitingTime;
        }
    }
    protected void checkHP()
    {
        stats.stoppingDistance = stats.og.stoppingDistance * (1f + (1f - (float)cv.hp / stats.og.hp)); // moves up to 2x further depending on how damaged it is
    }

    private void flyByPlayer(Vector3 direction)
    {
        direction += Vector3.Cross(direction, new Vector3(0, 0, 1)) * 0.3f * (Random.value > 0.5 ? 1f : -1f);
        StartCoroutine(flyByCD());
        rb.AddForce(direction * stats.speed * Time.deltaTime * 35);
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

        //updatePathCor = StartCoroutine(UpdatePath());
        findingPath = true;

        cooldownFlyby = 0;

        setTarget();

        cv = GetComponent<CombatVariables>();
        StartCoroutine(updateState(stateUpdateTime));
        StartCoroutine(mainLoop(mainLoopFrequency));

    }


    public enum State
    {
        goToTargetState,
        maintainRangeAttackState,
        disabled,
        flyby,
        backAway
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

    

    private void FixedUpdate()
    {
        if (player == null) return;

        Vector2 directionPlayer = player.position - transform.position;
        float angle = Mathf.Atan2(directionPlayer.y, directionPlayer.x) * Mathf.Rad2Deg - 90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, stats.angleSpeed * Time.deltaTime);

        //int moveDirection = 0;
        //int directionDuration = (int)(2 * 10 / Time.deltaTime);

        //Vector3 directionToPlayer = (player.position - transform.position);
        //float distanceToPlayer = directionToPlayer.magnitude;

        //switch (state)
        //{
        //    case State.goToTargetState:
        //        rb.AddForce(directionToPlayer * 0.01f * stats.speed * Time.deltaTime * (distanceToPlayer / stats.stoppingDistance - 1f));
        //        break;
        //    case State.maintainRangeAttackState:
        //        moveDirection = (moveDirection + 1) % directionDuration;
        //        directionToPlayer = Quaternion.Euler(0, 0, 70 * (moveDirection > directionDuration / 2 ? 1 : -1)) * directionToPlayer;
        //        rb.AddForce(directionToPlayer.normalized * stats.speed * Time.deltaTime * 0.2f);
        //        break;
        //    case State.backAway:
        //        rb.AddForce(-directionToPlayer * 0.1f * stats.speed * Time.deltaTime * (stats.stoppingDistance / distanceToPlayer - 1f));
        //        break;
        //}
    }


    //private void FixedUpdate()
    //{
    //    //cooldownFlyby -= Time.deltaTime;

    //    if (player == null) return;
    //    //rotate towards player
    //    Vector2 directionPlayer = target.position - transform.position;
    //    float angle = Mathf.Atan2(directionPlayer.y, directionPlayer.x) * Mathf.Rad2Deg - 90;
    //    Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    //    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, stats.angleSpeed * Time.deltaTime);

    //    switch (state)
    //    {

    //        #region caseSwitchStateMachine
    //        default:
    //            state = State.goToTargetState;
    //            break;
    //        case State.goToTargetState:
    //            #region pathfinder
    //            //plot path
    //            if (path == null)
    //            {
    //                return;
    //            }
    //            if (currentWaypoint >= path.vectorPath.Count)
    //            {
    //                reachedEndOfPath = true;
    //                return;
    //            }
    //            else
    //            {
    //                reachedEndOfPath = false;
    //            }

    //            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
    //            Vector2 force = direction * stats.speed * Time.deltaTime;
    //            //Debug.Log("Adding force" + force.ToString());
    //            if (player != null && Vector2.Distance(rb.position, player.position) >= stats.stoppingDistance)
    //            {
    //                rb.AddForce(force);
    //            }

    //            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
    //            if (distance < nextWaypontDistance)
    //            {
    //                currentWaypoint += 1;
    //            }

    //            #endregion

    //            if (player != null && Vector2.Distance(rb.position, player.position) <= stats.stoppingDistance)
    //            {
    //                state = State.maintainRangeAttackState;
    //            }

    //            break;
    //        case State.maintainRangeAttackState:
    //            float distanceToPlayer = Vector2.Distance(rb.position, player.position);
    //            if (distanceToPlayer >= (stats.stoppingDistance))
    //            {
    //                Vector2 dir = ((Vector2)player.position - rb.position).normalized;
    //                Vector2 f = dir * stats.speed * Time.deltaTime / 4;
    //                rb.AddForce(f);
    //            }

    //            else if (player != null && distanceToPlayer <= (stats.stoppingDistance))
    //            {
    //                if (stats.flyby && flybyReady)
    //                {
    //                    state = State.flyby;
    //                }
    //                Vector2 dir = ((Vector2)player.position - rb.position).normalized;
    //                Vector2 f = dir * stats.speed * Time.deltaTime / 4;
    //                rb.AddForce(-f);
    //            }

    //            //if (player != null && Vector2.Distance(rb.position, player.position) <= stats.stoppingDistance && stats.flyby )
    //            //{
    //            //    int rand = Random.Range(0, 10);
    //            //    if (flybyReady)
    //            //    {
    //            //        //cooldownFlyby = rand;
    //            //        state = State.flyby;
    //            //    }
    //            //}

    //            if (Vector2.Distance(rb.position, player.position) >= stats.stoppingDistance * 3)
    //            {
    //                state = State.goToTargetState;
    //            }

    //            break;
    //        case State.disabled:
    //            break;

    //        case State.flyby:
    //            flyByPlayer();

    //            if (player != null && Vector2.Distance(rb.position, player.position) <= stats.stoppingDistance)
    //            {
    //                state = State.maintainRangeAttackState;
    //            }

    //            else if (player != null && Vector2.Distance(rb.position, player.position) >= stats.stoppingDistance * 3)
    //            {
    //                state = State.goToTargetState;
    //            }

    //            break;

    //            #endregion


    //    }
    //}


}

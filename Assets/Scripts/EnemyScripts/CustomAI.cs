using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class CustomAI : MonoBehaviour
{

    public Transform target;
    private float speed;
    private float nextWaypontDistance;
    private float AngleSpeed;

    Path path;
    int currentWaypoint;
    bool reachedEndOfPath;

    Seeker seeker;
    Rigidbody2D rb;

    private bool lineOfSight;
    RaycastHit2D hit;
    private float timeTillRaycastSight;

    //public BoxCollider2D dodgeDetector;
    private float timeTillDodge;


    // Start is called before the first frame update
    void Start()
    {
        speed = 400f;
        nextWaypontDistance = 3f;
        currentWaypoint = 0;
        reachedEndOfPath = false;
        AngleSpeed = 20;
        lineOfSight = false;
        timeTillRaycastSight = 0.2f;
        timeTillDodge = 3f;

        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);


    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
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
    // Update is called once per frame
    void Update()
    {

        timeTillDodge -= Time.deltaTime;


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
        Vector2 force = direction * speed * Time.deltaTime;
        //Debug.Log("Adding force" + force.ToString());
        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypontDistance)
        {
            currentWaypoint += 1;
        }



        //rotate towards player
        Vector2 directionPlayer = target.position - transform.position;
        float angle = Mathf.Atan2(directionPlayer.y, directionPlayer.x) * Mathf.Rad2Deg - 90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, AngleSpeed * Time.deltaTime);


        //check line of sight
        timeTillRaycastSight -= Time.deltaTime;
        if (timeTillRaycastSight <= 0)
        {
            timeTillRaycastSight = 0.2f;
            hit = GetFirstRaycastHit(target.transform.position - transform.position);
            //hit = Physics2D.Raycast(transform.position, target.transform.position - transform.position);
            if (hit.collider.gameObject.tag == "Player")
            {
                //Debug.Log("ClearLineOfSight");
                lineOfSight = true;
            }
            else if (hit.collider.gameObject.tag == "Walls")
            {
                lineOfSight = false;
                //Debug.Log("I found something else with name = " + hit.collider.name);
            }

        }










    }

    public RaycastHit2D GetFirstRaycastHit(Vector2 direction)
    {
        //Check "Queries Start in Colliders" in Edit > Project Settings > Physics2D
        RaycastHit2D[] hits = new RaycastHit2D[2];
        Physics2D.RaycastNonAlloc(transform.position, direction, hits);
        //hits[0] will always be the Collider2D you are casting from.
        return hits[1];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //very bad stuff
        /*if (collision.tag == "PlayerProjectile" && timeTillDodge <= 0)
        {
            timeTillDodge = 3f;
            int randDir = Random.Range(0, 2) * 2 - 1;
            int randForce = Random.Range(3, 6);
            Vector2 force = new Vector2(randDir*300, 0);
            for(int i = 0; i >= randForce; i++)
            {
                rb.AddForce(transform.up.normalized * force);
            }
            Debug.Log("atempted dodge");
        }*/
        if (collision.tag == "PlayerProjectile" && timeTillDodge <= 0)
        {
            
        }

    }
}

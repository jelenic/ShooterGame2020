using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Tilemaps;

public class CustomAI : MonoBehaviour
{
    Stats stats;
    //private float speed;
    private float nextWaypontDistance;
    private float AngleSpeed;
    //public float range;

    Path path;
    int currentWaypoint;
    bool reachedEndOfPath;

    Seeker seeker;
    Rigidbody2D rb;
    public Transform target;
    private Transform player;
    private Transform startPosition;

    private bool lineOfSight;
    RaycastHit2D hit;
    private float timeTillRaycastSight;
    private float timeTillFire;

    //public BoxCollider2D dodgeDetector;
    private float timeTillDodge;
    private float speedDodge;
    private bool dodging;
    private int n;
    private int randDir;

    //public GameObject Bullets;



    void setTarget()
    {
        if (Vector2.Distance(rb.position, player.transform.position) <= stats.range)
        {
            target = player;
        } else
        {
            target = startPosition;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<Stats>();


        reachedEndOfPath = false;
        AngleSpeed = 20;
        lineOfSight = false;
        timeTillRaycastSight = 0.2f;
        timeTillDodge = 3f;
        speedDodge = 10f;
        dodging = false;

        nextWaypontDistance = 3f;
        currentWaypoint = 0;

        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        GameObject gb = new GameObject();
        gb.transform.position = rb.position + Random.insideUnitCircle*5;
        gb.transform.rotation = rb.transform.rotation;
        startPosition = gb.transform;


        InvokeRepeating("UpdatePath", 0f, 0.5f);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        setTarget();

    }

    void UpdatePath()
    {
        setTarget();

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
    void FixedUpdate()
    {

        timeTillDodge -= Time.deltaTime;
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
        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypontDistance)
        {
            currentWaypoint += 1;
        }


        #endregion





        //rotate towards player
        Vector2 directionPlayer = target.position - transform.position;
        float angle = Mathf.Atan2(directionPlayer.y, directionPlayer.x) * Mathf.Rad2Deg - 90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, AngleSpeed * Time.deltaTime);

        #region firecontrols
        //check line of sight
        timeTillRaycastSight -= Time.deltaTime;
        timeTillFire -= Time.deltaTime;
        if (timeTillRaycastSight <= 0)
        {
            timeTillRaycastSight = 0.2f;
            hit = GetFirstRaycastHit(target.transform.position - transform.position);
            //hit = Physics2D.Raycast(transform.position, target.transform.position - transform.position);
            //Debug.Log(hit.collider);
            if (hit.collider == null)
            {
                Debug.Log("nothing in sight");
                lineOfSight = false;
            }
            else if (hit.collider.gameObject.tag == "Player")
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

        if (lineOfSight && timeTillFire <= 0)
        {
            //Debug.Log("fireing");
            timeTillFire = 0.5f;
            //Shoot();
        }








        #endregion

        #region dodging

        if (dodging && n <= 10)
        {
            //Debug.Log(n.ToString());
            transform.Translate(transform.right * randDir * Time.deltaTime * speedDodge, Space.Self);
            n += 1;
        }
        else if (dodging && n > 10)
        {
            dodging = false;
        }


        #endregion








    }

    public RaycastHit2D GetFirstRaycastHit(Vector2 direction)
    {
        //Check "Queries Start in Colliders" in Edit > Project Settings > Physics2D
        RaycastHit2D[] hits = new RaycastHit2D[2];
        Physics2D.RaycastNonAlloc(transform.position, direction, hits);
        //hits[0] will always be the Collider2D you are casting from.
        //Debug.Log(hits);
        return hits[1];
    }



    //dodging here
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

        //enemy teleports
        /*if (collision.tag == "PlayerProjectile" && timeTillDodge <= 0)
        {
            timeTillDodge = 3f;
            int randDir = Random.Range(0, 2) * 2 - 1;
            rb.MovePosition(transform.position += transform.right * randDir * speedDodge);
        }*/

        //still teleports
        /*if (collision.tag == "PlayerProjectile" && timeTillDodge <= 0)
        {
            timeTillDodge = 3f;
            int randDir = Random.Range(0, 2) * 2 - 1;
            transform.Translate(transform.right * randDir * Time.deltaTime * speedDodge, Space.Self);
        }*/
        if (collision.tag == "PlayerProjectile" && timeTillDodge <= 0)
        {
            timeTillDodge = 3f;
            randDir = Random.Range(0, 2) * 2 - 1;
            n = 0;
            //Debug.Log("startDodging");
            dodging = true;
        }


    }


    //private void Shoot()
    //{
    //    Instantiate(Bullets, firepoint.position, firepoint.rotation);
    //}
}

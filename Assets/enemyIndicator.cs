using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyIndicator : MonoBehaviour
{
    public GameObject indicator;
    private GameObject indicatorInstance;
    private Transform target;
    private Transform indicatorTransform;
    public float speed;
    //Renderer rd;

    [SerializeField]
    private LayerMask affectedLayers;

    // Start is called before the first frame update
    void Start()
    {
        speed = Mathf.Max(10f, speed);
        //rd = GetComponentInChildren<Renderer>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        indicatorInstance = Instantiate(indicator, Vector3.zero, Quaternion.identity);
        indicatorTransform = indicatorInstance.GetComponent<Transform>();
        indicatorInstance.SetActive(false);
        //StartCoroutine(checkForIndicator());

    }

    private IEnumerator checkForIndicator()
    {
        while (true)
        {
            Vector2 direction = target.position - transform.position;
            RaycastHit2D ray = Physics2D.Raycast(transform.position, direction, direction.magnitude*1.3f, affectedLayers);
            //Debug.Log(ray.point.ToString());

            if (ray.collider != null)
            {
                indicatorInstance.SetActive(true);
                indicatorInstance.transform.position = ray.point;
            }
            else
            {
                indicatorInstance.SetActive(false);
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 direction = target.position - transform.position;
        RaycastHit2D ray = Physics2D.Raycast(transform.position, direction, direction.magnitude*1.3f, affectedLayers);
        //Debug.Log(ray.point.ToString());

        if (ray.collider != null)
        {
            indicatorInstance.SetActive(true);
            indicatorInstance.transform.position = ray.point;
            indicatorTransform.position = Vector3.Lerp(indicatorTransform.position, ray.point, Time.deltaTime * speed);
        }
        else
        {
            indicatorInstance.SetActive(false);
        }
        /*Debug.Log(rd.isVisible.ToString());
        if (rd.isVisible == false)
        {
            if (indicator.activeSelf == false)
            {
                indicator.SetActive(true);
            }

            Vector2 direction = target.transform.position - transform.position;
            RaycastHit2D ray = Physics2D.Raycast(transform.position, direction, 300f, affectedLayers);
            Debug.Log(ray.point.ToString());

            if (ray.collider != null)
            {
                indicator.transform.position = ray.point;
            }

        }
        else
        {
            if (indicator.activeSelf == true)
            {
                indicator.SetActive(false);
            }
        }*/

        /*Vector2 direction = target.transform.position - transform.position;
        RaycastHit2D ray = Physics2D.Raycast(transform.position, direction, 300f, affectedLayers);
        //Debug.Log(ray.point.ToString());

        if (ray.collider != null)
        {
            indicator.SetActive(true);
            indicator.transform.position = ray.point;
        }
        else
        {
            indicator.SetActive(false);
        }*/
    }
}

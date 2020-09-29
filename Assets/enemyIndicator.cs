using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyIndicator : MonoBehaviour
{
    public GameObject indicator;
    private GameObject target;

    //Renderer rd;

    [SerializeField]
    private LayerMask affectedLayers;

    // Start is called before the first frame update
    void Start()
    {
        //rd = GetComponentInChildren<Renderer>();
        target = GameObject.FindGameObjectWithTag("Player").gameObject;
        StartCoroutine(checkForIndicator());
    }

    private IEnumerator checkForIndicator()
    {
        Vector2 direction = target.transform.position - transform.position;
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
        }
        yield return new WaitForSeconds(0.5f);
    }
    

    // Update is called once per frame
    void Update()
    {
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

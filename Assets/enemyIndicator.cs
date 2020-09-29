using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyIndicator : MonoBehaviour
{
    public GameObject indicator;
    private GameObject indicatorInstance;
    private Transform indicatorTransform;
    Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        indicatorInstance = Instantiate(indicator, Vector3.zero, Quaternion.identity);
        indicatorTransform = indicatorInstance.GetComponent<Transform>();
        indicatorInstance.SetActive(false);
    }


    // Update is called once per frame
    void LateUpdate()
    {
        float border = 10f;

        Vector3 fromPosition = camera.transform.position;
        fromPosition.z = 0f;
        Vector3 dir = (transform.position - fromPosition).normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;


        Vector3 targetPositionScreenPoint = camera.WorldToScreenPoint(transform.position);
        bool isOffscreen = targetPositionScreenPoint.x <= border || targetPositionScreenPoint.x >= Screen.width - border || targetPositionScreenPoint.y <= border || targetPositionScreenPoint.y >= Screen.height - border;

        if (isOffscreen)
        {
            indicatorInstance.SetActive(true);

            targetPositionScreenPoint.x = Mathf.Clamp(targetPositionScreenPoint.x, border, Screen.width - border);
            targetPositionScreenPoint.y = Mathf.Clamp(targetPositionScreenPoint.y, border, Screen.height - border);

            Vector3 pointerWorldPosition = camera.ScreenToWorldPoint(targetPositionScreenPoint);
            indicatorTransform.position = pointerWorldPosition;

        }
        else
        {
            indicatorInstance.SetActive(false);
        }
    }
}

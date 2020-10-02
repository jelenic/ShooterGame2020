using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyIndicator : MonoBehaviour
{
    public GameObject indicator;
    private Transform indicatorTransform;
    Camera camera;
    private bool visible;

    void Start()
    {
        camera = Camera.main;
        indicatorTransform = indicator.GetComponent<Transform>();
    }

    private void OnBecameVisible()
    {
        visible = true;
        indicator.SetActive(false);
    }

    private void OnBecameInvisible()
    {
        visible = false;
        indicator.SetActive(true);
    }


    void LateUpdate()
    {
        if (!visible)
        {
            float border = 10f;

            Vector3 targetPositionScreenPoint = camera.WorldToScreenPoint(transform.position);

            targetPositionScreenPoint.x = Mathf.Clamp(targetPositionScreenPoint.x, border, Screen.width - border);
            targetPositionScreenPoint.y = Mathf.Clamp(targetPositionScreenPoint.y, border, Screen.height - border);

            Vector2 pointerWorldPosition = camera.ScreenToWorldPoint(targetPositionScreenPoint);
            indicatorTransform.position = pointerWorldPosition;
        }
    }
}

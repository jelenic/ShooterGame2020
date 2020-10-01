using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotation : MonoBehaviour
{
    public int rotationSpeed;
    public float rotationAvgDuration;

    private Stats stats;
    public int currentSpeed;


    private void Awake()
    {
        stats = GetComponent<Stats>();
        currentSpeed = rotationSpeed * (Random.value > 0.5 ? 1 : -1);
        StartCoroutine(directionChange());
    }

    private IEnumerator directionChange()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.5f * rotationAvgDuration, 2f * rotationAvgDuration));

            int beforeSpeed = currentSpeed;
            currentSpeed = 0;
            yield return new WaitForSeconds(Random.Range(0.5f, 1f));
            currentSpeed = -beforeSpeed;
        }
        

    }

    //private IEnumerator speedChange() // needs fixing
    //{

    //    int transitionDuration = Random.Range(20, 30);
    //    int direction = currentSpeed > 0 ? -1 : 1;

    //    Debug.LogWarning(direction + " speed changing " + transitionDuration);
    //    int i = transitionDuration;
    //    bool stopped = false;
    //    while (i > 0)
    //    {
    //        currentSpeed += direction * (int) ((1f / transitionDuration) * 2 * rotationSpeed);

    //        if (!stopped && currentSpeed < 10 && currentSpeed > 10)
    //        {
    //            stopped = true;
    //            yield return new WaitForSeconds(2f);
    //        }

    //        i--;
    //        Debug.LogWarningFormat("speed changing, {0}, {1}, {2}", transitionDuration, direction * ((int)(1f / transitionDuration) * 2 * rotationSpeed), currentSpeed);
    //        yield return new WaitForSeconds(0.1f);
    //    }

    //    StartCoroutine(directionChange());

    //} 


    private void FixedUpdate()
    {
        transform.Rotate(0, 0, (stats.speed / stats.og.speed) * currentSpeed);
    }
}

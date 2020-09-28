using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumeOnPickup : MonoBehaviour
{
    public ConsumableItem consumable;
    private CombatVariables cv;
    private SpriteRenderer sr;
    private Transform transform;
    private Coroutine rotateCor;
    private bool consumed; // since trigger sometimes activates twice quickly

    private void Awake()
    {
        transform = GetComponent<Transform>();
        sr = GetComponent<SpriteRenderer>();
        if (consumable.pickUpIcon != null) sr.sprite = consumable.pickUpIcon;
        sr.color = consumable.pickUpIconColor;
        cv = GameObject.FindGameObjectWithTag("Player").GetComponent<CombatVariables>();

        rotateCor = StartCoroutine(periodicRotate());

        Destroy(gameObject, consumable.disappearTime);

    }


    IEnumerator periodicRotate()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            transform.Rotate(0, 0, 45);
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (consumed) return;
        if (!collision.CompareTag("Player")) return;
        consumed = true;
        consumable.consume(cv);
        StopCoroutine(rotateCor);
        Destroy(gameObject);
    }

   

}

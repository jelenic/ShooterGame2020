using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumeOnPickup : MonoBehaviour
{
    public ConsumableItem consumable;
    private CombatVariables cv;
    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = consumable.color;
        cv = GameObject.FindGameObjectWithTag("Player").GetComponent<CombatVariables>();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        consumable.consume(cv);

        Destroy(gameObject);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{

    public string itemId;
    public GameObject gameManager;
    // Start is called before the first frame update

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.LogFormat("items just collided with {0}", collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.LogFormat("player just collected item: {0}", itemId);
            gameManager.GetComponent<Inventory>().addItem(itemId);

            Destroy(gameObject);
        }
    }
}

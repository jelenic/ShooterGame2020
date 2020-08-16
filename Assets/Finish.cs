using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public GameObject levelManager;
    public float duration;
    private float remaining;
    private bool playerIn;
    private Transform transform;
    private int lastTimerNumber;
    // Start is called before the first frame update

    public GameObject floatingNumberText;

    public void createFloatingNumberText(Vector2 position, Color color, string text = "oops")
    {
        if (color == null) color = Color.white;
        GameObject floatingNumber = Instantiate(floatingNumberText, position, Quaternion.identity);
        TextMesh tm = floatingNumber.GetComponent<TextMesh>();
        tm.text = text;
        tm.color = color;

    }

    void Start()
    {
        duration = duration == 0 ? 5f : duration;
        remaining = duration;
        levelManager = GameObject.FindGameObjectWithTag("LevelManager");
        transform = GetComponent<Transform>();
        floatingNumberText = Resources.Load("FloatingNumberText") as GameObject;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIn = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIn = false;
            remaining = duration;
            lastTimerNumber = (int) duration + 1;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerIn)
        {
            int remainingInt = (int)Math.Round(remaining);
            if (remainingInt != lastTimerNumber) {
                createFloatingNumberText(transform.position, Color.white, remainingInt.ToString());
                lastTimerNumber = remainingInt;
            }
            remaining -= Time.deltaTime;
        }
        
        if (remaining <= 0)
        {
            levelManager.GetComponent<LevelManager>().finishLevel();
        }
    }
}

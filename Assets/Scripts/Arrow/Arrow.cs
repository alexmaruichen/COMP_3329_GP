﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Rigidbody2D rb;
    public int speed = 5;
    public int damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        // Set the vertical speed to make the bullet move upward
    }

    // Gets called when the object goes out of the screen
    void OnBecameInvisible()
    {
        // Destroy the bullet
        Destroy(gameObject);        
    }
    void OnTriggerEnter2D(Collider2D obj)
    {
        string tag = obj.gameObject.tag;
        if (tag != "Item" && tag != gameObject.tag)
            Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        if (rb.position.x > 30 || rb.position.x < 0 || rb.position.y > 0 || rb.position.y < -30)
            Destroy(gameObject);
    }
}

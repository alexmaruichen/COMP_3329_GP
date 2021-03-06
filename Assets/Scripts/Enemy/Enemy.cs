using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Public variable that contains the speed of the enemy
    public float speed = 1f;
    public GameObject EnemyArrow;
    public GameObject ShootingMethod;
    public GameObject GM;
    public float shootfreq = 1f;
    private Transform target;
    public float enemy_sight = 2f;
    public int HP = 1;
    private int method = 0;
    public int damage = 1;
    public AudioSource AS;
    // Start is called before the first frame update
    void Start()
    {
        //define target for chasing
        target = GameObject.Find("Main").GetComponent<Transform>();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 v = rb.velocity;
        v.x = 0;
        v.y = 0;
        rb.velocity = v;
        rb.angularVelocity = Random.Range(-200, 200);
        //Destroy(gameObject, 3);

        //call the shoot function every shootfreq seconds
        InvokeRepeating("shoot", shootfreq, shootfreq);

        AS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnWillRenderObject()
    {
        //make the enemy chase the player
        if (Vector2.Distance(transform.position, target.position) < enemy_sight)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }
    //Function called when the enemy collides with another object
     void OnTriggerEnter2D(Collider2D obj)
     {
        string name = obj.gameObject.name;
        //if it collided with bullet
        if (name == "arrow(Clone)")
        {
            AudioSource.PlayClipAtPoint(AS.clip,transform.position);
            HP -= obj.gameObject.GetComponent<Arrow>().damage;
            
        }
        else if (name == "Main")
        {
            AudioSource.PlayClipAtPoint(AS.clip, transform.position);
            HP--;
            
        }
            
        if (HP <= 0)
        {
            GM.GetComponent<GameManager>().score++;
            Destroy(gameObject);   
        }
     } 
    
    void shoot()
    {
        ShootingMethod.GetComponent<Shoot>().shoot(method, EnemyArrow, transform, damage);
    }

}

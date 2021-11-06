using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustBunnyScript : MonoBehaviour
{
    public float Xvelocity = -1f;

    [HideInInspector]
    public Rigidbody2D rb;

    public Transform sightStart;
    public Transform sightEnd;

    public LayerMask detectWhat;

    public bool colliding;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        rb.velocity = new Vector2(Xvelocity, 0);

        colliding = Physics2D.Linecast(sightStart.position, sightEnd.position, detectWhat);

        if (colliding)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y * 1);
            Xvelocity *= -1;
        } 
    }
}

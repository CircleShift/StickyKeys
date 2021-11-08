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

    public Transform weakspot;
    public GameObject DeathParticle;

    public bool colliding;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        rb.velocity = new Vector2(Xvelocity, rb.velocity.y);

        colliding = Physics2D.Linecast(sightStart.position, sightEnd.position, detectWhat);

        if (colliding)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y * 1);
            Xvelocity *= -1;
        } 
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            float height = coll.contacts[0].point.y - weakspot.position.y;

            if (height > 0)
            {
                Die();
                GetComponent<AudioSource>().Play();
                coll.rigidbody.AddForce(new Vector2(0, 300));
            }
            else
            {
                coll.gameObject.GetComponent<CharacterController2D>().GoCheckpoint();
            }
        }
       
    }
    void Die()
    {
        transform.localScale = new Vector3(1, 0.5f, 1);
        transform.position -= new Vector3(0, 0.25f, 0);
        Instantiate(DeathParticle, transform.position, Quaternion.identity);
        Destroy(this.gameObject, 0.1f);
    }
}

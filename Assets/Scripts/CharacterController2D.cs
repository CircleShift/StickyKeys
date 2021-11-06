using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    // Movement keys
    public bool hasDKey;
    public bool hasAKey;
    public bool hasWKey;
    public bool hasSKey;
    // Action key
    public bool hasSpacebar;
    //Color changing keys
    public bool hasRKey;
    public bool hasGKey;
    public bool hasBKey;
    // Map key
    public bool hasMKey;

    public float jumpForce = 10.0f;
    public float speed = 5.0f;
    private bool isGrounded;
    private bool isCrouching;
    Rigidbody2D rigidBody;
    Vector2 startSize;
    Vector2 startOffset;

    bool IsGrounded() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, (GetComponent<BoxCollider2D>().size.y / 2) + .2f);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				return true;
			}
		}
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        startSize = GetComponent<BoxCollider2D>().size;
        startOffset = GetComponent<BoxCollider2D>().offset;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("a") && hasAKey) {
            rigidBody.velocity = new Vector2(-speed, rigidBody.velocity.y);
            GetComponent<SpriteRenderer>().flipX = false;
        } else if (Input.GetKey("d") && hasDKey) {
            rigidBody.velocity = new Vector2(speed, rigidBody.velocity.y);
            GetComponent<SpriteRenderer>().flipX = true;
        } else {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        }
        
        if (Input.GetKey("w") && hasWKey && IsGrounded()) {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
        }
        if (Input.GetKey("s") && hasSKey) {
            GetComponent<SpriteRenderer>().size = new Vector2(1.0f, 0.5f);
            GetComponent<BoxCollider2D>().size = startSize * new Vector2(1.0f, 0.5f);
            GetComponent<BoxCollider2D>().offset = startOffset - new Vector2(0.0f, startSize.y * 0.25f);
        } else {
            GetComponent<SpriteRenderer>().size = new Vector2(1.0f, 1.0f);
            GetComponent<BoxCollider2D>().size = startSize;
            GetComponent<BoxCollider2D>().offset = startOffset;
        }
        isCrouching = (Input.GetKey("s") && hasSKey);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.GetComponent<CollectableKey>() != null) {
            Debug.Log("Key");
        }
    }
}
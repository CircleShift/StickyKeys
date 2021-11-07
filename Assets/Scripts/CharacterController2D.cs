using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    //animator components
    public Animator anim;
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

    public bool isBlue;
    public bool isGreen;
    public bool isRed;

    public float jumpForce = 10.0f;
    public float speed = 5.0f;
    private bool isGrounded;
    private bool isCrouching;
    Rigidbody2D rigidBody;
    Vector2 startSize;
    Vector2 startOffset;

    Vector3 currentRGB;
    Material material;
    private GameObject[] blueBoxes;
    private GameObject[] greenBoxes;
    private GameObject[] redBoxes;

    bool IsGrounded() {
        /*Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position - new Vector3(0.0f, GetComponent<BoxCollider2D>().size.y / 2, 0.0f), (GetComponent<BoxCollider2D>().size.y / 2) + .2f);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				return true;
			}
		}
        return false;*/
        RaycastHit2D hit = Physics2D.Raycast(transform.position - new Vector3(0.0f, GetComponent<BoxCollider2D>().size.y / 2, 0.0f), -Vector2.up);
        if (hit.collider != null)
        {
            float distance = Mathf.Abs(hit.point.y - transform.position.y);
            if (distance < .5f)
            {
                anim.SetBool("isJumping", false);
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
        anim = GetComponentInChildren<Animator>();

        blueBoxes = GameObject.FindGameObjectsWithTag("Blue");
        greenBoxes = GameObject.FindGameObjectsWithTag("Green");
        redBoxes = GameObject.FindGameObjectsWithTag("Red");
		WaypointManager.SetPlayer(this);
		WaypointManager.Init();

        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("a") && hasAKey) {
            rigidBody.velocity = new Vector2(-speed, rigidBody.velocity.y);
            GetComponentInChildren<SpriteRenderer>().flipX = false;
            anim.SetBool("isWalking", true);
        } else if (Input.GetKey("d") && hasDKey) {
            rigidBody.velocity = new Vector2(speed, rigidBody.velocity.y);
            GetComponentInChildren<SpriteRenderer>().flipX = true;
            anim.SetBool("isWalking", true);
        } else {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
            anim.SetBool("isWalking", false);
        }
        
        if (Input.GetKey("w") && hasWKey && IsGrounded()) {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
            GetComponent<AudioSource>().Play();
            anim.SetBool("isJumping", true);
        }
        if (Input.GetKey("s") && hasSKey) {
            GetComponentInChildren<SpriteRenderer>().size = new Vector2(1.0f, 0.5f);
            GetComponent<BoxCollider2D>().size = startSize * new Vector2(1.0f, 0.5f);
            GetComponent<BoxCollider2D>().offset = startOffset - new Vector2(0.0f, startSize.y * 0.25f);
        } else {
            GetComponentInChildren<SpriteRenderer>().size = new Vector2(1.0f, 1.0f);
            GetComponent<BoxCollider2D>().size = startSize;
            GetComponent<BoxCollider2D>().offset = startOffset;
        }

		Vector4 rgb = new Vector4(0.0f, 0.0f, 0.0f);
		
        if (isRed)
			rgb.x = 1.0f;
		if (isGreen)
			rgb.y = 1.0f;
		if (isBlue)
			rgb.z = 1.0f;

		material.SetVector("_RGB", rgb);

        isCrouching = (Input.GetKey("s") && hasSKey);

        ChangeColor();
        CheckRGBBoxes();
    }

    public void ChangeColor()
    {
        if (hasBKey && Input.GetKeyDown(KeyCode.B))
        {
            isBlue = !isBlue;
        }
        if (hasGKey && Input.GetKeyDown(KeyCode.G))
        {
            isGreen = !isGreen;
        }
        if (hasRKey && Input.GetKeyDown(KeyCode.R))
        {
            isRed = !isRed;
        }
    }

    private void CheckRGBBoxes()
    {
        if (blueBoxes != null)
        {
             if (isBlue)
                    {
                        foreach(GameObject box in blueBoxes)
                        {
                            box.GetComponent<BoxCollider2D>().enabled = false;
                        }
                    }
                    else
                    {
                        foreach (GameObject box in blueBoxes)
                        {
                            box.GetComponent<BoxCollider2D>().enabled = true;
                        }
                    }
        }
        if (greenBoxes != null)
        {
            if (isGreen)
            {
                foreach (GameObject box in greenBoxes)
                {
                    box.GetComponent<BoxCollider2D>().enabled = false;
                }
            }
            else
            {
                foreach (GameObject box in greenBoxes)
                {
                    box.GetComponent<BoxCollider2D>().enabled = true;
                }
            }
        }
        if (redBoxes != null)
        {
            if (isBlue)
            {
                foreach (GameObject box in redBoxes)
                {
                    box.GetComponent<BoxCollider2D>().enabled = false;
                }
            }
            else
            {
                foreach (GameObject box in redBoxes)
                {
                    box.GetComponent<BoxCollider2D>().enabled = true;
                }
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.GetComponent<CollectableKey>() != null) {
            Debug.Log("Key");
        }
    }

    public void onDamaged() {
        //teleport back to the last checkpoint
        WaypointManager.GoCheckpoint();
    }
}

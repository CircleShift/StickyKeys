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
    AnimatorPlayer animPlayer;
    private GameObject[] blueBoxes;
    private GameObject[] greenBoxes;
    private GameObject[] redBoxes;

    public AudioClip jumpSound;
    public AudioClip deathSound;

    bool IsGrounded() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position - new Vector3(0.0f, GetComponent<Collider2D>().bounds.extents.y, 0.0f), -Vector2.up);
        if (hit.collider != null) {
            float distance = Mathf.Abs(hit.point.y - transform.position.y);
            if ((hit.collider.gameObject.tag == "Red" || hit.collider.gameObject.tag == "Green" 
                || hit.collider.gameObject.tag == "Blue" || hit.collider.gameObject.tag == "LevelTilemap") && distance < .5f) {
                return true;
            }
        }
        return false;
        /*if (hit.collider != null)
        {
            float distance = Mathf.Abs(hit.point.y - transform.position.y);
            if (distance < .5f)
            {
                return true;
            }            
        }
        return false;*/
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

		anim = GetComponentInChildren<Animator>();

        blueBoxes = GameObject.FindGameObjectsWithTag("Blue");
        greenBoxes = GameObject.FindGameObjectsWithTag("Green");
        redBoxes = GameObject.FindGameObjectsWithTag("Red");
		WaypointManager.SetPlayer(gameObject);
		WaypointManager.Init();

        animPlayer = GetComponentInChildren<AnimatorPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGrounded())
        {
            anim.SetBool("isJumping", false);
        }
        else
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isJumping", true);
        }
        if (Input.GetKey("a") && hasAKey) {
            rigidBody.velocity = new Vector2(-speed, rigidBody.velocity.y);
            animPlayer.setFlip(false);
            if (IsGrounded())
				anim.SetBool("isWalking", true);
        } else if (Input.GetKey("d") && hasDKey) {
            rigidBody.velocity = new Vector2(speed, rigidBody.velocity.y);
            animPlayer.setFlip(true);
            if (IsGrounded())
				anim.SetBool("isWalking", true);
        } else {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
            anim.SetBool("isWalking", false);
        }        
        if (Input.GetKey("w") && hasWKey && IsGrounded()) {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
            GetComponent<AudioSource>().clip = jumpSound;
            GetComponent<AudioSource>().Play();
        }       
        if (Input.GetKeyDown("s") && hasSKey) {
			transform.localScale = new Vector3(1, 0.5f, 1);
			transform.position -= new Vector3(0, 0.25f, 0);
			isCrouching = true;
        } else if (Input.GetKeyUp("s") && isCrouching) {
			transform.position += new Vector3(0, 0.25f, 0);
			transform.localScale = new Vector3(1, 1, 1);
			isCrouching = false;
        }

		Vector4 rgb = new Vector3(0.0f, 0.0f, 0.0f);
		
        if (isRed)
			rgb.x = 1.0f;
		if (isGreen)
			rgb.y = 1.0f;
		if (isBlue)
			rgb.z = 1.0f;

		animPlayer.setRGB(rgb);

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
                    if (box.GetComponent<BoxCollider2D>().bounds.Contains(transform.position)) {
                        GoCheckopint();
                    }
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
                    if (box.GetComponent<BoxCollider2D>().bounds.Contains(transform.position)) {
                        GoCheckopint();
                    }
                }
            }
        }
        if (redBoxes != null)
        {
            if (isRed)
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
                    if (box.GetComponent<BoxCollider2D>().bounds.Contains(transform.position)) {
                        GoCheckopint();
                    }
                }
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.GetComponent<CollectableKey>() != null) {
            Debug.Log("Key");
        }
    }

    public void GoCheckopint() {
        //teleport back to the last checkpoint
        WaypointManager.GoCheckpoint();
        GetComponent<AudioSource>().clip = deathSound;
        GetComponent<AudioSource>().Play();
    }
}

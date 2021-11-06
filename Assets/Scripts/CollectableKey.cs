using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableKey : MonoBehaviour
{
    private float startY;
    public string KeyName;
    private bool enterPressed;
    // Start is called before the first frame update
    void Start()
    {
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x, startY + (Mathf.Sin(Time.frameCount / 300.0f) / 10.0f));
        enterPressed = Input.GetKey(KeyCode.Return);
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (enterPressed && other.name == "player") {
            CharacterController2D player_controller = other.gameObject.GetComponent<CharacterController2D>();
            switch (KeyName) {
                case "w":
                    player_controller.hasWKey = true;
                    break;
                case "a":
                    player_controller.hasAKey = true;
                    break;
                case "s":
                    player_controller.hasSKey = true;
                    break;
                case "d":
                    player_controller.hasDKey = true;
                    break;
            }
            Destroy(this.gameObject);
        }
    }
}

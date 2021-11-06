using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableKey : MonoBehaviour
{
    private float startY;
    public string KeyName;
    private bool enterPressed;

    private TutorialController tutorial;
    // Start is called before the first frame update
    void Start()
    {
        tutorial = GameObject.Find("Canvas").GetComponent<TutorialController>();
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x, startY + (Mathf.Sin(Time.frameCount / 300.0f) / 10.0f));
        enterPressed = Input.GetKey(KeyCode.Return);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        switch (KeyName) {
            case "a":
                tutorial.toggleOn(2);
                tutorial.toggleOff(1);
                break;
        }
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
                    tutorial.toggleOff(2);
                    break;
                case "s":
                    player_controller.hasSKey = true;
                    break;
                case "d":
                    player_controller.hasDKey = true;
                    tutorial.toggleOff(0);
                    tutorial.toggleOn(1);
                    break;
            }
            Destroy(this.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableKey : MonoBehaviour
{
    private float startY;
    public string KeyName;
    private bool enterPressed;
    private bool isFade;

    private TutorialController tutorial;
    // Start is called before the first frame update
    void Start()
    {
        tutorial = GameObject.Find("Canvas").GetComponent<TutorialController>();
        startY = transform.position.y;
        transform.GetChild(0).localScale = new Vector3(0.0f, 0.0f, 0.0f);
        isFade = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFade) {
            transform.position = new Vector2(transform.position.x, startY + (Mathf.Sin(Time.frameCount / 300.0f) / 10.0f));
        }
        enterPressed = Input.GetKey(KeyCode.Return);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        StartCoroutine(growGlow());
        switch (KeyName) {
            case "a":
                tutorial.toggleOn(2);
                tutorial.toggleOff(1);
                tutorial.toggleOff(0);
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        StartCoroutine(shrinkGlow());    
    }

    private IEnumerator growGlow() {
        float glow_scale = 0f;
        while (glow_scale < 1f) {
            float smooth_scale = Mathf.SmoothStep(0.1f, 0.2f, glow_scale);
            transform.GetChild(0).localScale = new Vector3(smooth_scale, smooth_scale, smooth_scale);
            glow_scale += Time.deltaTime * 2;
            yield return null;
        }
    }

    private IEnumerator shrinkGlow() {
        float glow_scale = 1f;
        while (glow_scale > 0f) {
            float smooth_scale = Mathf.SmoothStep(0.1f, 0.2f, glow_scale);
            if (transform.GetChild(0) != null) {
                transform.GetChild(0).localScale = new Vector3(smooth_scale, smooth_scale, smooth_scale);
            }
            glow_scale -= Time.deltaTime * 2;
            yield return null;
        }
    }

    public IEnumerator FadeOut(float t)
    {
        Color currentColor = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = new Color(currentColor.r, currentColor.g, currentColor.b, 1);
        while (currentColor.a > 0.0f)
        {
            currentColor = new Color(currentColor.r, currentColor.g, currentColor.b, currentColor.a - (Time.deltaTime / t));
            GetComponent<SpriteRenderer>().color = currentColor;
            transform.Translate(0, Time.deltaTime * 0.1f, 0);
            yield return null;
        }
        Destroy(this.gameObject, 1.0f);
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
            Destroy(this.GetComponent<BoxCollider2D>());
            Destroy(transform.GetChild(0).gameObject);
            GetComponent<AudioSource>().Play();
            StopAllCoroutines();
            StartCoroutine(FadeOut(0.75f));
        }
    }
}

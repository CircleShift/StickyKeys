using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorPlayer : MonoBehaviour
{
	SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    public void setFlip(bool b) {
		spriteRenderer.flipX = b;
	}

	public void setRGB (Vector3 rgb) {
		spriteRenderer.material.SetVector("_RGB", rgb);
	}
}

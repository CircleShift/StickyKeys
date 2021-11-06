using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{

	[SerializeField]
	float yOffset;

	private void OnTriggerEnter2D(Collider2D other) {
		
	}
}
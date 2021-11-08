using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockKillPlayer : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player")
			other.gameObject.GetComponent<CharacterController2D>().GoCheckpoint();
	}

	private void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.tag == "Player")
			other.gameObject.GetComponent<CharacterController2D>().GoCheckpoint();
	}
}
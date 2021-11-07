using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : Waypoint
{

	virtual protected void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			if(WaypointManager.GetLastCheckpoint() != this) {
				WaypointManager.SetLastCheckpoint(this);
				GetComponent<AudioSource>().Play();
			}
		}
	}
}

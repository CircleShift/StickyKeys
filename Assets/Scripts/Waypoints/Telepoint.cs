using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telepoint : Waypoint
{
	[SerializeField]
	bool teleportAll;
	[SerializeField]
	bool checkpoint;
	[SerializeField]
	string goToScene;
	[SerializeField]
	string goToWaypoint;

	int teleporting;

    override public void GoToWaypoint(GameObject c) {
		teleporting += 1;
		base.GoToWaypoint(c);
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(teleporting == 0) {
			if (teleportAll)
				WaypointManager.GoWaypoint(other.gameObject, goToWaypoint);
			else if (other.gameObject.tag == "Player")
				WaypointManager.GoWaypoint(goToScene, goToWaypoint);
		} else if (checkpoint && other.gameObject.tag == "Player")
			WaypointManager.SetLastCheckpoint(this);
	}

	private void OnTriggerExit2D(Collider2D other) {
		teleporting -= 1;
	}
}

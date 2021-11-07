using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telepoint : Checkpoint
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

	override protected void OnTriggerEnter2D(Collider2D other) {
		if(teleporting == 0) {
			if (teleportAll)
				WaypointManager.GoWaypoint(other.gameObject, goToWaypoint);
			else if (other.gameObject.tag == "Player")
				WaypointManager.GoWaypoint(goToScene, goToWaypoint);
		} else if (checkpoint) 
			base.OnTriggerEnter2D(other);

		if(teleporting > 0 && (teleportAll || other.gameObject.tag == "Player"))
			teleporting -= 1;
	}
}

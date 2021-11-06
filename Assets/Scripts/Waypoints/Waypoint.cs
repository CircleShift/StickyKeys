using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
	[SerializeField]
	bool teleporter;
	[SerializeField]
	bool checkpoint;
	[SerializeField]
	bool defaultWaypoint;
	[SerializeField]
	string waypointName;
	[SerializeField]
	string connectedWPScene;
	[SerializeField]
	string connectedWPName;

	bool teleporting;


    // Start is called before the first frame update
    void Start()
    {
        WaypointManager.AddWaypoint(this);

		if(defaultWaypoint)
			WaypointManager.SetDefault(this);
    }

	public string GetName() {
		return waypointName;
	}

	public void GoToWaypoint(CharacterController2D c) {
		teleporting = true;
		c.gameObject.transform.position = this.gameObject.transform.position;
		if(checkpoint)
			WaypointManager.SetLastCheckpoint(this);
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "Player" && !teleporting){
			if (checkpoint)
				WaypointManager.SetLastCheckpoint(this);
			else if (teleporter && connectedWPScene == "")
				WaypointManager.GoWaypoint(connectedWPName);
			else if (teleporter)
				WaypointManager.GoWaypoint(connectedWPScene, connectedWPName);
		} 
	}

	private void OnTriggerExit2D(Collider2D other) {
		teleporting = false;
	}
}

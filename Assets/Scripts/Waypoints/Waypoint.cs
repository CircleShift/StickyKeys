using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
	[SerializeField]
	bool teleporter;
	[SerializeField]
	bool teleportAll;
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

	int teleporting;


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

	public void GoToWaypoint(GameObject c) {
		teleporting += 1;
		c.transform.position = this.gameObject.transform.position;
		if(checkpoint)
			WaypointManager.SetLastCheckpoint(this);
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(teleporter && teleporting == 0) {
			if (teleportAll)
				WaypointManager.GoWaypoint(other.gameObject, connectedWPName);
			else if (other.gameObject.tag == "Player")
				WaypointManager.GoWaypoint(connectedWPScene, connectedWPName);
			GetComponent<AudioSource>().Play();
		} else if (other.gameObject.tag == "Player" && checkpoint) {
			WaypointManager.SetLastCheckpoint(this);
		}
	}

	private void OnTriggerExit2D(Collider2D other) {
		teleporting -= 1;
	}
}

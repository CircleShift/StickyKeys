using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
	[SerializeField]
	bool intermediate;
	[SerializeField]
	bool door;
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


    // Start is called before the first frame update
    void Start()
    {
		if(intermediate)
			WaypointManager.SetIntermediate(this);
		else
        	WaypointManager.AddWaypoint(this);

		if(defaultWaypoint)
			WaypointManager.SetDefault(this);
    }

	public string GetName() {
		return waypointName;
	}

	public void GoToWaypoint(CharacterController2D c) {
		c.gameObject.transform.position = this.gameObject.transform.position;
		if(checkpoint)
			WaypointManager.SetLastCheckpoint(this);
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "Player" && checkpoint) {
			WaypointManager.SetLastCheckpoint(this);
		}
	}
}

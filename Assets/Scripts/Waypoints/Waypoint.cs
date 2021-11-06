using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
	[SerializeField]
	bool isIntermediate;
	[SerializeField]
	bool door;
	[SerializeField]
	bool checkpoint;
	[SerializeField]
	string waypointName;
	[SerializeField]
	string connectedWPScene;
	[SerializeField]
	string connectedWPName;


    // Start is called before the first frame update
    void Start()
    {
		if(isIntermediate)
			WaypointManager.SetIntermediate(this);
		else
        	WaypointManager.AddWaypoint(this);
    }

	public string GetName() {
		return waypointName;
	}
}

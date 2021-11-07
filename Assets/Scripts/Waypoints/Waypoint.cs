using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{	
	[SerializeField]
	bool defaultWaypoint;
	[SerializeField]
	string waypointName;

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

	virtual public void GoToWaypoint(GameObject c) {
		c.transform.position = this.gameObject.transform.position;
	}
}

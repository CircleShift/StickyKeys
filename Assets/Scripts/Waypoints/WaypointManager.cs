using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class WaypointManager {
	private static List<Waypoint> currentWaypoints = new List<Waypoint>(0);
	private static Waypoint lastCheckpoint = null;
	private static Waypoint intermediate = null;

	private static CharacterController2D player = null;

	private static string goingWP = "";

	private static bool initialized = false;


	// Initialize the subscriber methods
	public static void Init() {
		if(!initialized) {
			SceneManager.activeSceneChanged += SceneChanging;
			SceneManager.sceneLoaded += SceneLoaded;
			initialized = true;
		}
	}

	private static void SceneChanging(Scene current, Scene next){
		SoftReset();
	}

	private static void SceneLoaded(Scene loaded, LoadSceneMode next){
		if(goingWP != "" && intermediate != null && player != null) {
			intermediate.GoToWaypoint(player);
			GoWaypoint(goingWP);
		}

		goingWP = "";
	}

	/*
		For use by Waypoint.cs or extended classes.
	*/
	public static void AddWaypoint(Waypoint w) {
		currentWaypoints.Add(w);
	}

	public static bool RemoveWaypoint(Waypoint w) {
		return currentWaypoints.Remove(w);
	}

	public static void SoftReset() {
		currentWaypoints = new List<Waypoint>(0);
		intermediate = null;
		player = null;
	}

	public static void HardReset() {
		SoftReset();
		goingWP = "";
	}

	/*
		Getters and setters for useful waypoints
	*/
	public static Waypoint GetLastCheckpoint() {
		return lastCheckpoint;
	}

	public static void SetLastCheckpoint(Waypoint w) {
		lastCheckpoint = w;
	}

	public static Waypoint GetIntermediate() {
		return intermediate;
	}

	public static void SetIntermediate(Waypoint w) {
		intermediate = w;
	}

	public static void SetPlayer(CharacterController2D c) {
		player = c;
	}

	/*
		General functions
	*/

	/** Get a waypoint by name (current scene only)
		name - name of the waypoint
	*/
	public static Waypoint GetWaypoint(string name) {
		foreach(Waypoint w in currentWaypoints) {
			if(w.GetName() == name)
				return w;
		}
		return null;
	}

	/** Go to a waypoint (any)
		scene - name of the scene containing the waypoint
		name - name of the waypoint
	*/
	public static void GoWaypoint(string scene, string name) {
		goingWP = name;
		if(SceneManager.GetActiveScene().name == scene)
			GoWaypoint(name);
		else
			SceneManager.LoadScene(scene, LoadSceneMode.Single);
	}

	/** Go to a waypoint in the current scene only
		name - name of the waypoint
	*/
	public static bool GoWaypoint(string name) {
		Waypoint going = GetWaypoint(name);
		if(going != null) {
			going.GoToWaypoint(player);
			return true;
		}
		return false;
	}

	/** Go to the last checkpoint (if available)
		name - name of the waypoint
	*/
	public static bool GoCheckpoint() {
		if(lastCheckpoint != null) {
			lastCheckpoint.GoToWaypoint(player);
			return true;
		}
		return false;
	}
}
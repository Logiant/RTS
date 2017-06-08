using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RTS;

public class Player : MonoBehaviour {

	public string playerName;
	public bool isHuman;


	public float resources;


	List<WorldObject> units;
	List<WorldObject> structures;

	public List<Command> activeCommands;
	//list of buildings that produce
	//list of reseource drop offs
	//list of workers
	//list of soldiers

	void Awake() {
		units = new List<WorldObject> ();
		structures = new List<WorldObject> ();
		activeCommands = new List<Command> ();
		activeCommands.Add(new Command(Command.TYPES.NONE, new Vector3()));
	}

	public void addResources(float amt) {
		resources += amt;
	}

	public void addUnit(WorldObject unit) {
		units.Add (unit);
	}

	public void addStructure(WorldObject unit) {
		structures.Add (unit);
	}

	public List<WorldObject> getUnits() {
		return units;
	}

	public List<WorldObject> getStructures() {
		return structures;
	}

	// Use this for initialization
	void Start () {
		if (isHuman) {
			GameState.player = this;
		}
	}

	//add a command from the UI/input
	public void AddCommand(Command c) {
		activeCommands.Add (c);
	}

	// Update is called once per frame
	void Update () {
		foreach (Command cmd in activeCommands) {
			Debug.Log (cmd.Text ());
		}
		//loop through each command. if it's completed delete it
	}

}

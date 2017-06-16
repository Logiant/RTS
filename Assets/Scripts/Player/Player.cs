using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RTS;

public class Player : MonoBehaviour {

	public string playerName;
	public bool isHuman;

    public Backpack bp; //total resource count


	List<Unit> units;
	List<Structure> structures;

	public Command nothing;
	public List<Command> activeCommands;
	//list of buildings that produce
	//list of reseource drop offs
	//list of workers
	//list of soldiers

	void Awake() {
		units = new List<Unit> ();
		structures = new List<Structure> ();
        bp = new Backpack();

		nothing = new Command (Command.TYPES.NONE);

		activeCommands = new List<Command> ();
		activeCommands.Add(nothing);
	}

	public void addUnit(Unit unit) {
		units.Add (unit);
	}

	public void addStructure(Structure str) {
		structures.Add (str);
	}

	public List<Unit> getUnits() {
		return units;
	}

	public List<Structure> getStructures() {
		return structures;
	}

    public List<Warehouse> getWarehouses() {
        List<Warehouse> wh = new List<Warehouse>();

        foreach (Structure s in structures) {
            if (s is Warehouse) {
                wh.Add((Warehouse)s);
            }
        }

        return wh;
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
        List<Unit> idles = nothing.actors;
        //TODO sort into list(s?) based on qualification
        //TODO sort list based on distance to job

        for (int i = 0; i < idles.Count; i++) {
            idles[i].Update(); //TODO there should be a better way to do this
        }


	}

    //TODO do an early update for all units on the 'nothing' job?


	// Update is called once per frame
	void Update () {
		for (int i = activeCommands.Count - 1; i >= 0; i--) {
			Command cmd = activeCommands[i];

			if (cmd.complete && cmd.type != Command.TYPES.NONE) {
				activeCommands.Remove (cmd);
			}
		}
        //update backpack
        bp.cornQty = 0; bp.woodQty = 0; bp.toolQty = 0;
        List<Warehouse> wh = getWarehouses();
        foreach (Warehouse w in wh) {
            bp.cornQty += w.bp.cornQty;
            bp.woodQty += w.bp.woodQty;
            bp.toolQty += w.bp.toolQty;
        }


    }

}

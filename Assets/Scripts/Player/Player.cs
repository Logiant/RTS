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

    public GameObject CmdFlag;

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
        //starting resources
        bp.Add(new Backpack.Resources(15, 15, 10, 10));

		nothing = new Command (Command.TYPES.NONE, RTS.GameState.InvalidPosition);

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

    public int getNumHouses() {
        int nHouses = 0;

        foreach (Structure s in structures) {
            if (s is House) {
                nHouses ++;
            }
        }

        return nHouses;
    }

    // Use this for initialization
    void Start () {
		if (isHuman) {
			GameState.player = this;
		}
	}

    //add a command from the UI/input
    public void AddCommand(Command c) {
        activeCommands.Add(c);
        //instantiate the command flag at c.position
        List<Unit> idles = nothing.actors;
        //TODO sort into list(s?) based on qualification
        if (c.type != Command.TYPES.NONE) {
            GameObject go = (Instantiate(CmdFlag, c.position, new Quaternion()) as GameObject);
            CommandFlag cf = go.GetComponent<CommandFlag>();
            cf.cmd = c;
        }
        //TODO sort list based on distance to job?

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
        List<Warehouse> wh = getWarehouses();
        foreach (Warehouse w in wh) {
            bp.Transfer(ref w.bp.res);
        }


    }

}

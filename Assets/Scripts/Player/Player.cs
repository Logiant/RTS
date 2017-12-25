using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RTS;

public class Player : MonoBehaviour {

	public string playerName;
	public bool isHuman;

    public Backpack bp; //total resource count
    public ItemStorage ist;

    public GameObject Churl;


    private int housing_capacity = 3;

	List<Unit> units;
	List<Structure> structures;

    public GameObject CmdFlag;

	public Command nothing;
    public CmdCraft craft;
	public List<Command> activeCommands;
	//list of buildings that produce
	//list of reseource drop offs
	//list of workers
	//list of soldiers

	void Awake() {
		units = new List<Unit> ();
		structures = new List<Structure> ();
        bp = new Backpack();
        ist = new ItemStorage();
        //starting resources
        bp.Add(new Backpack.Resources(15, 15, 10, 10));

        ist.Add(new Tool(Tool.TOOLTYPES.AXE));
        ist.Add(new Tool(Tool.TOOLTYPES.HAMMER));
        ist.Add(new Tool(Tool.TOOLTYPES.SAW));
        ist.Add(new Tool(Tool.TOOLTYPES.SCYTHE));


        nothing = new Command (Command.TYPES.NONE, RTS.GameState.InvalidPosition, this);
        craft = new CmdCraft(this);
		activeCommands = new List<Command> ();
		activeCommands.Add(nothing);
        activeCommands.Add(craft);
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

    //TODO these should be dynamically changed as new units/models are added
    public List<Warehouse> getWarehouses() {
        List<Warehouse> wh = new List<Warehouse>();

        foreach (Structure s in structures) {
            if (s is Warehouse) {
                wh.Add((Warehouse)s);
            }
        }
        return wh;
    }

    public List<House> getHouses() {
        List<House> h = new List<House>();

        foreach (Structure s in structures) {
            if (s is House) {
                h.Add((House)s);
            }
        }
        return h;
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

    public List<Workshop> getWorkshops() {
        List<Workshop> w = new List<Workshop>();

        foreach (Structure s in structures) {
            if (s is Workshop) {
                w.Add((Workshop)s);
            }
        }
        return w;
    }

    public int getNumWorkshops() {
        return getWorkshops().Count;
    }

    public int getCapacity() {
        return getNumHouses() * housing_capacity;
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

        Spawn();
    }

    protected void Spawn() {
        //TODO put some sort of cooldown system in place
        //TODO some "critical food" surplus should remain
        List<House> houses = getHouses();
        //if we have enough food and houses
        if (units.Count < getCapacity() && this.bp.res.corn >= 5) {
            bp.Remove(new Backpack.Resources(5, 0, 0, 0));
            //spawn a churl @ one of the houses
            int index = Random.Range(0, houses.Count);

            GameObject go = Instantiate(Churl, houses[index].accessPoint.position, houses[index].transform.rotation) as GameObject;
            go.GetComponent<WorldObject>().player = this;

        }
    }

}

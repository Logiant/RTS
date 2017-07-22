using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foundation : Structure {

	GameObject finalBuilding;

	float buildTime = 2.5f; //takes 2.5 * 3 seconds to build
	float timer = 0;

    float count = 3; //counts to build this structure

    CmdConstruct activeCommand;

    Backpack.Resources cost;

	// Use this for initialization
	public override void Start () {
		timer = buildTime;
	}

    public string GetName() {
        return finalBuilding.GetComponent<WorldObject>().objectName;
    }

    public void Deliver(ref Backpack.Resources res) {
        //TODO only remove resources needed :'(
        cost.Remove(res);
        res.Clear();
    }

    public bool Ready() {
        return cost.wood == 0;
    }

    public void Increment() {
        if (timer <= 0) {
            timer = buildTime;
            count--;
        }
    }

	// Update is called once per frame
	public override void Update () {
		if (finalBuilding != null) {
			timer -= Time.deltaTime;

			if (count <= 0) {
                activeCommand.complete = true;
				//instantiate the new building
				finalBuilding.SetActive(true);
				WorldObject wo = finalBuilding.GetComponent<WorldObject>();
				wo.player = RTS.GameState.player;
				//destroy this
				Destroy(this.gameObject);
			}

		}
	}

    public override Backpack.Resources getCost() {
        return cost;
    }

    public void Run(GameObject final) {
		finalBuilding = Instantiate (final, transform.position, transform.rotation);
		finalBuilding.SetActive (false);
        //get the cost
        cost = new Backpack.Resources(final.GetComponent<Structure>().getCost());
		//set the size of this object
		Bounds b = new Bounds (Vector3.zero, Vector3.zero);

		foreach (Renderer c in final.GetComponentsInChildren<Renderer>()) {
			b.Encapsulate (new Bounds(c.transform.localPosition, c.bounds.extents*2));
		}
		Vector3 scale = b.extents*2;
		scale.y = 1;
		transform.GetComponentInChildren<MeshRenderer> ().transform.localScale = scale;

        //add a command to the stack
        activeCommand = new CmdConstruct(this);
		player.AddCommand (activeCommand);

	}
}

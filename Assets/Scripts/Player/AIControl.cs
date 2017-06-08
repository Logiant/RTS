using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControl : MonoBehaviour {

	Player player;

	/*public Harvestable tree;

	public float updateTime = 5f; //time between command updates
	float updateTimer = 0f;


	// Use this for initialization
	void Start () {
		player = GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		updateTimer -= Time.deltaTime;

		if (updateTimer <= 0) {
			updateTimer += updateTime;
			//get all workers

			List<WorldObject> units = player.getUnits();
			foreach (WorldObject unit in units) {
				if (unit == null) {
					continue;
				}

				Harvester h = unit.GetComponent<Harvester> ();
				if (h != null && !h.isHarvesting ()) {
					//TODO procedurally grab trees from the terrain
					h.command (tree.gameObject, tree.transform.position);
				}
			}

			List<WorldObject> structures = player.getStructures ();

			foreach (WorldObject structure in structures) {
				if (structure == null) {
					continue;
				}

				Producer p = structure.GetComponent<Producer> ();
				if (p != null) {
					//replace with some sort of high-level production manager
					p.doAction ("Peasant");
				}
			}

		}
	}*/
}

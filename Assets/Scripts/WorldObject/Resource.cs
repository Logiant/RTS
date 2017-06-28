using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : WorldObject {

    public int wood = 4;
    public int metal = 0;

    public CmdHarvest cmd;

    float harvestTime = 5f;
    float timer = 0f;

	// Use this for initialization
	override public void  Start () {
        timer = harvestTime;
    }

    public Backpack.Resources Harvest() {
        Backpack.Resources res = new Backpack.Resources();

        timer -= Time.deltaTime;
        if (timer <= 0) {
            timer += harvestTime;
            if (wood > 0) {
                res.wood++;
                wood--;
            } if (metal > 0) {
                res.metal++;
                metal--;
            }
        }
        return res;
    }
	
	// Update is called once per frame
	override public void Update () {
        //TODO fix this hack
        if (cmd != null && cmd.complete) {
            cmd = null;
        }

        if (wood <= 0 && metal <= 0) {
            cmd.complete = true;

            Destroy(this.gameObject);
        }
	}
}

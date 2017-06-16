using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : WorldObject {

    public int wood = 4;
    public int stone = 0;

    public CmdHarvest cmd;

    float harvestTime = 5f;
    float timer = 0f;

	// Use this for initialization
	override public void  Start () {
        timer = harvestTime;
    }

    public int Harvest() {
        timer -= Time.deltaTime;
        if (timer <= 0) {
            timer += harvestTime;
            wood--;
            return 1;
        }
        return 0;
    }
	
	// Update is called once per frame
	override public void Update () {
        if (wood == 0 && stone == 0) {
            cmd.complete = true;

            Destroy(this.gameObject);
        }
	}
}

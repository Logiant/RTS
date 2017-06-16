using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : Structure {

    float growTime = 10;
    float timer = 0;
    

	// Use this for initialization
	public override void Start () {
        base.Start();
        player.AddCommand(new CmdFarm(this));
        timer = growTime;
    }

    public void Work() {
        //water the plants or whatever
        timer -= Time.deltaTime;
    }

    public bool isHarvestable() {
        return timer <= 0;
    }

    public int Harvest() {
        timer = growTime;
        return 1;
    }

	// Update is called once per frame
	public override void Update () {

	}
}

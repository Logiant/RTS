using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Structure : WorldObject {

    public Transform accessPoint;
    public int WoodCost;

    private Backpack.Resources res;

    // Use this for initialization
    public override void Start() {
        base.Start();
        player.addStructure(this);

        res = new Backpack.Resources(0, WoodCost, 0, 0);

    }

    public Backpack.Resources getCost() {
        return new Backpack.Resources(0, WoodCost, 0, 0);
    }
}

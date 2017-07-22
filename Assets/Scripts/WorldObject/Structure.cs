using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Structure : WorldObject {

    public Transform accessPoint;
    public int WoodCost = 5;

    public Sprite icon;

    private Backpack.Resources res;

    // Use this for initialization
    public override void Start() {
        base.Start();
        player.addStructure(this);

        res = new Backpack.Resources(0, WoodCost, 0, 0);

    }

     public virtual Backpack.Resources getCost() {
        return new Backpack.Resources(0, WoodCost, 0, 0);
    }
}

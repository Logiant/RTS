using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warehouse : Structure {

    public Backpack bp;

    // Use this for initialization
    public override void Start() {
        base.Start();
        bp = new Backpack();
    }

}

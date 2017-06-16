using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Structure : WorldObject {

	// Use this for initialization
	public override void Start () {
        base.Start();
        player.addStructure(this);
	}
}

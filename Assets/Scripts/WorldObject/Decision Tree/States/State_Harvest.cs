using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Harvest : State {

	Resource target;

	State_Goto go;
	State_Chop chop;

	Behavior_Nodal node;

	public State_Harvest (DecisionTree parent) : base(parent) {

		go = new State_Goto (parent);
		chop = new State_Chop (parent);

		node = new Behavior_Nodal ();
		node.Add (go);
		node.Add (chop);
	}

	//each state must DO something
	public override void Act () {
		if (target == null) {
			flow = FLOW.fail;
			return;
		}
		flow = node.Act ();
	}

	//each sate must reset-able
	public override void Reset() {
		flow = FLOW.fresh;
		node.Reset ();
		target = null;
	}

	//// New functionality ////
	public void SetTarget(Resource target) {
		this.target = target;

		go.SetPosition (target.transform.position);
		chop.SetTarget (target);

	}

	public Resource getTarget() {
		return target;
	}

}

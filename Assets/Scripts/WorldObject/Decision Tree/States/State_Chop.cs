using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Chop : State {

	Resource target;

	float timer = 5f;
	float cd_time = 5f;

	public State_Chop (DecisionTree parent) : base(parent) {
	
	}

	//each state must DO something
	public override void Act () {
		//fail if there is no target
		if (target == null) {
			flow = FLOW.fail;
			return;
		}
		//TODO fail if inventory is full


		//TODO scale timer based on tools
		// multiply by parent.unit.GetHarvestMultiplier() ??
		if (flow == FLOW.fresh) {
			timer = cd_time;
		}
		flow = FLOW.run;

		timer -= Time.deltaTime;
		if (timer <= 0) {
			Backpack.Resources amt = target.Harvest ();
			parent.GetUnit ().bp.Add (amt);
			flow = FLOW.pass;
		}
	}

	//each sate must reset-able
	public override void Reset() {
		flow = FLOW.fresh;
		target = null;
	}

	//// New functionality ////
	public void SetTarget(Resource target) {
		this.target = target;
	}



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Construct : State {

	Foundation foundation;

	float timer = 5f;
	float cd_time = 5f;

	public State_Construct (DecisionTree parent) : base(parent) {
	
	}

	//each state must DO something
	public override void Act () {
		//fail if there is no target
		if (foundation == null) {
			flow = FLOW.fail;
			return;
		}

		//TODO scale timer based on tools
		// multiply by parent.unit.GetHarvestMultiplier() ??
		if (flow == FLOW.fresh) {
			timer = cd_time;
		}
		flow = FLOW.run;

		timer -= Time.deltaTime;
		if (timer <= 0) {
            if (foundation.Increment()) {
                flow = FLOW.pass;
            }
		}

  	}

	//each sate must reset-able
	public override void Reset() {
		flow = FLOW.fresh;
		foundation = null;
	}

	//// New functionality ////
	public void SetTarget(Foundation foundation) {
		this.foundation = foundation;
	}



}

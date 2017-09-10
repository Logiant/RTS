using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Transform : State {

    //target backpack to create tools
	Backpack target;

    int metalsIn = 2;
    int toolsOut = 1;

    float timer = 0f;
    float cd_time = 10f;

	public State_Transform (DecisionTree parent) : base(parent) {
	
	}

	//each state must DO something
	public override void Act () {
		//fail if there is no target
		if (target == null) {
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
            if (target.res.metal > metalsIn) {
                flow = FLOW.fail;
            } else {
                target.res.metal -= metalsIn;
                target.giveItem(new Tool());
                flow = FLOW.pass;
            }
		}
	}

	//each sate must reset-able
	public override void Reset() {
		flow = FLOW.fresh;
		target = null;
	}

	//// New functionality ////
	public void Configure(Backpack target) {
		this.target = target;
	}



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Tend : State {

	Field target;

	float timer = 10f;
	float cd_time = 10f;

    bool oneshot = false;

    State_Goto move;

	public State_Tend(DecisionTree parent) : base(parent) {

        move = new State_Goto(parent);

	}

	//each state must DO something
	public override void Act () {
		//fail if there is no target
		if (target == null || move.getFlow() == FLOW.fail) {
			flow = FLOW.fail;
			return;
		}
        flow = FLOW.run;
        //move if we're not finished with that
        if (move.getFlow() == FLOW.fresh || move.getFlow() == FLOW.run) {
            move.Act();
            move.getFlow();
        }
        //do tending
        else {
            if (oneshot) {
                oneshot = false;
                timer = cd_time + Time.deltaTime;
                flow = FLOW.run;
            }

            timer -= Time.deltaTime;

            if (timer <= 0) {
                //add food to the target farm
                target.Tend();
                if (target.isHarvestable()) {
                    flow = FLOW.pass;
                } else {
                    timer = cd_time;
                }
            }
        }
	}


	//each sate must reset-able
	public override void Reset() {
		flow = FLOW.fresh;
        oneshot = true;
		target = null;
        move.Reset();
	}

	//// New functionality ////
	public void SetTarget(Field target) {
		this.target = target;
        move.SetPosition(target.accessPoint.transform.position);
	}



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Trade : State {

	//sub-states
	State_Goto go;
	State_Transfer trade;
	//required information
	Backpack target = null;
	//behavior component
	Behavior_Nodal nodes;

	//TODO trade should have a give_amount and take_amount

	public State_Trade(DecisionTree parent) : base(parent) {
		go = new State_Goto (parent);
		trade = new State_Transfer (parent);

		nodes = new Behavior_Nodal ();

		nodes.Add (go);
		nodes.Add (trade);
	}

	//each state must DO something
	public override void Act () {
		//make sure everything is valid
		if (target == null) {
			flow = FLOW.fail;
			return;
		}

		flow = nodes.Act();
	}

	//each sate must reset-able
	public override void Reset() {
		flow = FLOW.fresh;
		nodes.Reset();
	}

	//// New functionality ////
	public void SetTrade(Vector3 location, ref Backpack target, Backpack.Resources amount) {
		go.SetPosition (location);
		trade.SetTrade (ref target, amount);
		this.target = target;
	}



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Transfer : State {

	//location to trade with
	Backpack target;
	//amount to trade
	Backpack.Resources tradeAmt;


	public State_Transfer(DecisionTree parent) : base(parent) {
	
	}

	//each state must DO something
	public override void Act () {
		//fail if target is invalid
		if (target == null || tradeAmt.isEmpty()) {
			flow = FLOW.fail;
			Debug.Log (tradeAmt);
			return;
		}
		//TODO fail if we're too far from the target


		//ask target if it can trade resources
		if (target.CanTrade (tradeAmt)) {
			target.res.Add (tradeAmt);
			parent.GetUnit ().bp.Remove (tradeAmt);
			flow = FLOW.pass;
		} else {
			Debug.Log ("Target " + target + " can't trade!");
			flow = FLOW.fail;
		}
	}

	//each sate must reset-able
	public override void Reset() {
		flow = FLOW.fresh;
		target = null;
		tradeAmt = new Backpack.Resources();
	}

	//// New functionality ////
	public void SetTrade(ref Backpack target, Backpack.Resources amount) {
		this.target = target;
		this.tradeAmt = amount;
		Debug.Log ("Trading" + tradeAmt);
	}
}

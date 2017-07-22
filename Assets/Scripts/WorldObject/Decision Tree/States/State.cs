using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State {
	//the flow of each state
	public enum FLOW {
		fresh, run, pass, fail
	}

	//the flow of the current state
	protected FLOW flow = FLOW.fresh;

	//parent decision tree
	protected DecisionTree parent;

	//get the parent tree in the constructor
	public State(DecisionTree p) {
		parent = p;
	}

	//each state must DO something
	public abstract void Act ();

	//each sate must reset-able
	public abstract void Reset();

	//flow getter
	public FLOW getFlow() {
		return flow;
	}

}

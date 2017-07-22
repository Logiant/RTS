using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavior_Nodal {

	List<State> children;

	public Behavior_Nodal() {
		children = new List<State> ();
	}

	public void Add(State s) {
		children.Add (s);
	}

	public State.FLOW Act() {
		State.FLOW flow = State.FLOW.run;
		//run each child
		foreach (State child in children) {
			if (child.getFlow () == State.FLOW.fresh) {
				Debug.Log ("Transitioning to " + child);
			}


			if (child.getFlow () == State.FLOW.fresh ||
			    child.getFlow () == State.FLOW.run) {
				child.Act ();
				if (child.getFlow () == State.FLOW.fail) {
					flow = State.FLOW.fail;
				}
				break;
			}
			else if (child.getFlow () == State.FLOW.fail) {
				flow = State.FLOW.fail;
				break;
			}


		}
		//if the last node is success, return success
		if (children [children.Count - 1].getFlow() == State.FLOW.pass) {
			flow = State.FLOW.pass;
		}
		return flow;
	}

	public void Reset() {
		foreach (State child in children) {
			child.Reset ();
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this node has a bunch of children states that can be run in any order
//any being uniformy distributed random, of course!
public class NodeAnyOrder : State {

	List<State> children;
    State toRun = null;

	public NodeAnyOrder(DecisionTree parent, List<State> children) : base(parent, DecisionTree.STATES.NODE) {
		this.children = children;
	}

	public override bool Update(List<Command> commands) {
        bool canRun = false;
        foreach (State s in children) {
            if (s.Update(commands)) {
                canRun = true;
                toRun = s;
                break;
            }
        }
		return canRun;
	}

    public override void Act() {
        toRun.Act();
    }

    public override string ToString() {
        return toRun.ToString();
    }

}
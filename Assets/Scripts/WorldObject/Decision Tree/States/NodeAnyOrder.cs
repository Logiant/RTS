using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this node has a bunch of children states that can be run in any order
//any being uniformy distributed random, of course!
public class NodeAnyOrder : State {

	List<State> children;
    State toRun = null;

	public NodeAnyOrder(DecisionTree parent, List<State> children) : base(parent, DecisionTree.STATES.NODE) {
        //initialize child nodes
        this.children = new List<State>(children.Count);
        //randomly place child nodes - order is not important
        for (int i = children.Count-1; i >= 0; i--) {
            int index = Random.Range(0, children.Count);
            this.children.Add(children[index]);
            children.RemoveAt(index);
        }
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
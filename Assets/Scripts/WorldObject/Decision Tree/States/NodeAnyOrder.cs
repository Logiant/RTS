using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this node has a bunch of children states that can be run in any order
//any being uniformy distributed random, of course!
public class NodeAnyOrder : State {

	List<State> children;

	public NodeAnyOrder(DecisionTree parent, List<State> children) : base(parent, DecisionTree.STATES.NODE) {
		this.children = children;
	}

	public override bool Update(List<Command> commands) {
		return false;
	}

	public override void Act() {
		//TODO implement
	}

}
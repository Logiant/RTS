using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCraft : State {

	public StateCraft(DecisionTree parent) : base(parent, DecisionTree.STATES.CRAFT) {

	}

	public override bool Update(List<Command> commands) {
		//TODO implement
		return false;
	}

	public override void Act() {
		//TODO implement -- maybe walk to the town center or home?
	}
}
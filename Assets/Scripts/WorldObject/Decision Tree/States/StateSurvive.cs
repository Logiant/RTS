using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSurvive : State {

	public StateSurvive(DecisionTree parent) : base(parent, DecisionTree.STATES.SURVIVE) {

	}

	public override bool Update(List<Command> commands) {
		//TODO implement
		return false;
	}

	public override void Act() {
		//TODO implement -- maybe walk to the town center or home?
	}
}
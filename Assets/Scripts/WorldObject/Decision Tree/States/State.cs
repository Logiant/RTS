using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State {
	//constant state enum value
	public readonly DecisionTree.STATES state;
	//owner of this state
	protected DecisionTree parent;

	public State(DecisionTree p, DecisionTree.STATES s) {
		state = s;
		parent = p;
	}

	public abstract bool Update(List<Command> commands);

	public abstract void Act ();

}
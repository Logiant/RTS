using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DecisionTree {
	//possible states (leaves) in this tree
	public enum STATES {
		MILITARY, 	//offensive/garrison military acts
		SURVIVE, 	//self defense, healing, fleeing
		BUILD,    	//build a structure
		CRAFT,		//craft or process items
		FARM,		//farm land to gather food
		GATHER,		//gather wood or metal
		IDLE,		//do nothing
		MOVE,		//move to some point
		NODE		//something went wrong (used for nodes)
	}
	//current state of this unit
	public State currentState;

	//parent unit
	protected Unit parent;
	//the decision tree data structure - we are technically the root node
	protected List<State> tree;
	//the command currently being executed
	public Command currentCommand;

	public DecisionTree(Unit parent) {
		this.parent = parent;
		tree = new List<State> ();
		currentCommand = parent.player.activeCommands [0];
	}

	public Unit GetUnit() {
		return parent;
	}
	//TODO is there a better way to access this?
	public void SetCommand(Command cmd) {
		if (currentCommand != null) { //TODO just replace this with the NOTHING command
			currentCommand.actors.Remove (this.parent);
		}
		currentCommand = cmd;
	}
	//select one of the in-progress commands
	public virtual void Update (List<Command> commands) {
		Descend (commands);
		//do our current state TODO will this ever be null?
		currentState.Act();

	}

	protected virtual void Descend(List<Command> commands) {
		Debug.Log ("Running commands!");
		foreach (State s in tree) {
			if (s.Update (commands)) {
				currentState = s;
				//we have a new active command!
				break;
			}
		}
	}

}

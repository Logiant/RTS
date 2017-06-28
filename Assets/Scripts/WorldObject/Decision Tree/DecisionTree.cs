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
	protected Command currentCommand;

	public DecisionTree(Unit parent) {
		this.parent = parent;
		tree = new List<State> ();
		currentCommand = parent.player.nothing;
        currentCommand.actors.Add(this.parent);
	}

	public Unit GetUnit() {
		return parent;
	}
	//TODO is there a better way to access this?
	public void SetCommand(Command cmd) {
		currentCommand.actors.Remove (this.parent);
		currentCommand = cmd;
		currentCommand.actors.Add (this.parent);
	}

	public void ClearCommand() {
        SetCommand(parent.player.nothing);
        parent.Halt();
	}

	//select one of the in-progress commands
	public virtual void Update (List<Command> commands) {
        if (currentCommand.complete || currentCommand.paused) {
            ClearCommand();
        }
        //TODO this can be delayed with a timer to save processing power for high numbers of units if required - be sure to add some random offset!
		Descend (commands);
		//do our current state!
		currentState.Act();

	}

    public Command getCommand() {
        return currentCommand;
    }

	protected virtual void Descend(List<Command> commands) {
		foreach (State s in tree) {
			if (s.Update (commands)) {
				currentState = s;
				//we have a new active command!
				return;
			}
		}
	}

}

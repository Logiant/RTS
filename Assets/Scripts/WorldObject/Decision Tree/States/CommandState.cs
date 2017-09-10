using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CommandState : State {

    protected Command.TYPES type;

	public CommandState (DecisionTree parent) : base (parent) { }

	public abstract bool Update(List<Command> commands);

}

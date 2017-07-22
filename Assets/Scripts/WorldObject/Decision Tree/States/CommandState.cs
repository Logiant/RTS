using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CommandState : State {

	public CommandState (DecisionTree parent) : base (parent) { }

	public abstract bool Update(List<Command> commands);

}

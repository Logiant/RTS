using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_CmdBuild : CommandState {


    Behavior_Nodal nodes;

    //child states of Build
    State_Goto deliver; //TODO replace this with a delivery state
    State_Construct construct;
    //DELIVER
    //CONSTRUCT

    //current command type
    Command.TYPES type = Command.TYPES.BUILD;

    //get the parent tree in the constructor
    public State_CmdBuild(DecisionTree p) : base(p) {

        nodes = new Behavior_Nodal();
        deliver = new State_Goto(p);
        construct = new State_Construct(p);


        nodes.Add(deliver);
        nodes.Add(construct);

        Reset();

    }

    public override bool Update(List<Command> commands) {
        if (parent.getCommand().type == this.type) {
            return true; //we have a command of equal priority
        }
        //else, pick a new, higher priority command
        bool avail = false;
        foreach (Command cmd in commands) {
            if (cmd.type == this.type && cmd.actors.Count < 1 && !cmd.complete) {
                //we've found an open command of this type
                parent.SetCommand(cmd);
                parent.currentState = this;
                avail = true;
                Reset();
                CmdConstruct cc = (CmdConstruct)cmd;
                //initialize substates
                deliver.SetPosition(cc.foundation.transform.position);
                construct.SetTarget(cc.foundation);

                break;
            }

        }
        return avail;
    }

	//each state must DO something
	public override void Act () {
        flow = nodes.Act();
        if (flow == FLOW.pass) {
            parent.ClearCommand();
        }
    }

	//each sate must reset-able
	public override void Reset() {
        flow = FLOW.fresh;
        nodes.Reset();
    }

}

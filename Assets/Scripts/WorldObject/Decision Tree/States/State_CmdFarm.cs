using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_CmdFarm : CommandState {

    Behavior_Nodal nodes;

    //child states of Farm
    //Tend
    State_Tend tend;
    //Harvest
    State_Harvest_Farm farm;

    //current command type
    Command.TYPES type = Command.TYPES.FARM;
    public State_CmdFarm(DecisionTree p) : base(p) {

        type = Command.TYPES.FARM;
        //move = new State_Goto (p);
        nodes = new Behavior_Nodal();

        tend = new State_Tend(p);
        farm = new State_Harvest_Farm(p);

        nodes.Add(tend);
        nodes.Add(farm);

		Reset ();
	
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
                CmdFarm cf = (CmdFarm)cmd;
                //initialize substates
                tend.SetTarget(cf.farm);
                farm.SetTarget(cf.farm);
                break;
            }

        }
        return  avail;
    }

	//each state must DO something
	public override void Act () {
        flow = nodes.Act();
        if (flow == FLOW.pass) {
            nodes.Reset();
            CmdFarm cf = (CmdFarm)parent.getCommand();
            //initialize substates
            tend.SetTarget(cf.farm);
            farm.SetTarget(cf.farm);
        }
	}

	//each sate must reset-able
	public override void Reset() {
		flow = FLOW.fresh;
        nodes.Reset();
	}

}

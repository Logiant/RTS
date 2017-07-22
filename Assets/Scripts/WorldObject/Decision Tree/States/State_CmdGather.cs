using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_CmdGather : CommandState {

	Behavior_Nodal nodes;

	//child states of Gather
	State_Harvest harvest;
	State_Trade trade;
	//helper bool
	bool oneshot = false;

	//current command type
	Command.TYPES type = Command.TYPES.HARVEST;

	//get the parent tree in the constructor
	public State_CmdGather(DecisionTree p) : base(p) {
	
		nodes = new Behavior_Nodal ();
		harvest = new State_Harvest (p);
		trade = new State_Trade (p);

		nodes.Add (harvest);

		nodes.Add (trade);

		Reset ();
	
	}

	public override bool Update(List<Command> commands) {
		if (parent.getCommand ().type == this.type) {
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
				Reset ();
				CmdHarvest ch = (CmdHarvest)cmd;
				harvest.SetTarget (ch.resource);

				break;
			}

		}
		return avail;
	}

	//each state must DO something
	public override void Act () {
		flow = nodes.Act ();
		if (!oneshot && harvest.getFlow () == FLOW.pass) {
			oneshot = true;
			Warehouse nearest = RTS.Utility.GetNearestWarehouse(this.parent.GetUnit().transform.position, this.parent.GetUnit ().player.getWarehouses ());

			trade.SetTrade(nearest.accessPoint.position, ref nearest.bp , this.parent.GetUnit().bp.res);
		}

		if (flow == FLOW.pass) {
			Reset ();
			if (parent.getCommand() is CmdHarvest) {
				CmdHarvest ch = (CmdHarvest)parent.getCommand();
				harvest.SetTarget (ch.resource);
			}
		}
	}

	//each sate must reset-able
	public override void Reset() {
		flow = FLOW.fresh;
		oneshot = false;
		nodes.Reset ();
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCraft : State {

	public StateCraft(DecisionTree parent) : base(parent, DecisionTree.STATES.CRAFT) {

	}

	public override bool Update(List<Command> commands) {
        if (parent.getCommand().type == Command.TYPES.CRAFT) {
            return true;
        }
        bool avail = false;
        foreach (Command cmd in commands) {
            if (cmd.paused) {
                continue;
            }
            if (cmd.type == Command.TYPES.CRAFT && cmd.actors.Count < 1) {
                //check if there is anything to craft
                CmdCraft cc = (CmdCraft)cmd;
                if (cc.workshop.GetDemand() > 0) {
                    parent.SetCommand(cmd);
                    avail = true;
                    break;
                }
            }
        }
        return avail;
    }

	public override void Act() {
        //get necessary objects
        Unit body = parent.GetUnit();
        CmdCraft cmd = (CmdCraft)(parent.getCommand());

        if (cmd.workshop.GetDemand() <= 0) {
            parent.ClearCommand();
            return;
        }

        Vector3 craftSpot = cmd.workshop.accessPoint.position;
        //move to the target position
        if ((body.transform.position - craftSpot).magnitude > 0.5) {
            body.MoveTo(craftSpot);
        } else {
            cmd.workshop.Craft();
        }

        //if we are not near the craft station move toward it
        //if we are near the craft station
        //cmd.workshop.craft() - timer is handeled internally

        //TODO move resources/goods back and forth between the workshops and warehouses
        //TODO craft specific tools based on... some criterai? Different types of workshops?


    }
}
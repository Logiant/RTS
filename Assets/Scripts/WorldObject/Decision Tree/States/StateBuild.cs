using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBuild : State {

	public StateBuild(DecisionTree parent) : base(parent, DecisionTree.STATES.BUILD) {

	}

	public override bool Update(List<Command> commands) {
        if (parent.getCommand().type == Command.TYPES.BUILD) {
            return true;
        }
        bool avail = false;
        foreach (Command cmd in commands) {
            if (cmd.paused) {
                continue;
            }
            if (cmd.type == Command.TYPES.BUILD && cmd.actors.Count < 1) {
                parent.SetCommand(cmd);
                avail = true;
                break;
            }
        }

		return avail;
	}

	public override void Act() {

        //get necessary objects
        Unit body = parent.GetUnit();
        CmdConstruct cmd = (CmdConstruct)(parent.getCommand());

        //check to make sure it's not already built
        if (cmd.foundation == null) {
            parent.ClearCommand();
            return;
        }

        Vector3 center = cmd.foundation.transform.position + new Vector3(0, 0.5f, 0);
        Warehouse nearest = null;
        //if the resources haven't been delivered
        if (!cmd.foundation.Ready()) {
            //if we're not holding the resources we need, go to the nearest warehouse
            if (nearest == null) {
                nearest = RTS.Utility.GetNearestWarehouse(body.transform.position, body.player.getWarehouses());
            }
            //if we can afford the cost, move to the foundation and transfer supplies
            if (body.bp.Contains(cmd.foundation.getCost())  ) {
                if ((center - body.transform.position).magnitude < 5) {
                    body.Halt();
                    cmd.foundation.Deliver(ref body.bp.res);
                } else {
                    body.MoveTo(cmd.foundation.transform.position + new Vector3(0, 0.5f, 0));
                }
            }
            //goto nearest (warehouse) and pick up the cost
            else {
                if ((body.transform.position - nearest.accessPoint.position).magnitude <= 0.1f) {
                    body.Halt();
                    //TODO empty everything
                    body.bp.Add(nearest.TakeFromReserve(cmd.foundation.getCost()));
                } else {
                    body.MoveTo(nearest.accessPoint.position);
                }
            }
        }
        //let's get to building!
        else { 
            if ((center - body.transform.position).magnitude < 5) {
                //   cmd.foundation.Give(body.bp.res);
                //stop moving
                body.MoveTo(body.transform.position);
                //build
                cmd.foundation.Increment();
            } else if (cmd.foundation != null) {
                body.MoveTo(cmd.foundation.transform.position + new Vector3(0, 0.5f, 0));
            } else {
                body.MoveTo(body.transform.position);
            }
        }
    }



}
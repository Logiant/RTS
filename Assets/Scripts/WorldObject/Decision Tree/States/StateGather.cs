using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateGather : State {

	public StateGather(DecisionTree parent) : base(parent, DecisionTree.STATES.IDLE) {

	}

    Warehouse nearest;

	public override bool Update(List<Command> commands) {
        if (parent.getCommand().type == Command.TYPES.HARVEST) {
            return true;
        }
        bool avail = false;
        foreach (Command cmd in commands) {
            if (cmd.paused) {
                continue;
            }
            if (cmd.type == Command.TYPES.HARVEST && cmd.actors.Count < 1) {
                parent.SetCommand(cmd);
                avail = true;
                break;
            }
        }

        return avail;
    }

	public override void Act() {
        float range = 0.75f;
        //get necessary objects
        Unit body = parent.GetUnit();
        CmdHarvest cmd = (CmdHarvest)(parent.getCommand());

        //check to make sure it's not already harvested
        if (cmd.resource == null) {
            if (body.bp.resourceQty() == 0) {
                parent.getCommand().complete = true;
                parent.ClearCommand();
                return;
            } else { //TODO MOVE THIS TO IDLE?
                //TODO this is repeated code, make it a function!!!
                if (nearest == null) {
                    Debug.Log("ERROR! NO WAREHOUSE FOUND!");
                    return;
                }
                //if near the warehouse
                if ((body.transform.position - nearest.accessPoint.position).magnitude <= range) {
                    body.Halt();
                    nearest.bp.Transfer(ref body.bp.res);
                } else {
                    body.MoveTo(nearest.accessPoint.position);
                }
            }
        }
        //if arms are full && resource exists
        else if (body.bp.resourceQty() >= 2) {
            if (nearest == null) {
                Debug.Log("ERROR! NO WAREHOUSE FOUND!");
                return;
            }
            //if near the warehouse
            if ((body.transform.position - nearest.accessPoint.position).magnitude <= range) {
                body.Halt();
                //TODO empty everything
                nearest.bp.Transfer(ref body.bp.res);
            } else {
                body.MoveTo(nearest.accessPoint.position);
            }
        } //else if arms are not full
        else {
            //if in harvesting range
            if ((cmd.resource.transform.position - body.transform.position).magnitude <= range*7) {
                body.Halt();
                body.bp.Add(cmd.resource.Harvest());
                if (body.bp.resourceQty() >= 2) {
                    nearest = RTS.Utility.GetNearestWarehouse(body.transform.position, body.player.getWarehouses());
                }
            }//else if out of range
            else {
                body.MoveTo(cmd.resource.transform.position);
            }
        }
    }
}
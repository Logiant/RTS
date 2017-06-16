using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateGather : State {

	public StateGather(DecisionTree parent) : base(parent, DecisionTree.STATES.IDLE) {

	}

    Warehouse nearest;

	public override bool Update(List<Command> commands) {
        if (parent.currentCommand.type == Command.TYPES.HARVEST) {
            return true;
        }
        bool avail = false;
        foreach (Command cmd in commands) {
            if (cmd.type == Command.TYPES.HARVEST && cmd.actors.Count < 1) {
                parent.SetCommand(cmd);
                avail = true;
                break;
            }
        }

        return avail;
    }

	public override void Act() {
        float range = 1.5f;
        //get necessary objects
        Unit body = parent.GetUnit();
        CmdHarvest cmd = (CmdHarvest)(parent.currentCommand);

        //check to make sure it's not already harvested
        if (cmd.resource == null) {
            if (body.bp.woodQty == 0) {
                parent.ClearCommand();
                return;
            } else { //TODO MOVE THIS TO IDLE
                //TODO this is repeated code, make it a function!!!
                if (nearest == null) {
                    Debug.Log("ERROR! NO WAREHOUSE FOUND!");
                    return;
                }
                //if near the warehouse
                if ((body.transform.position - nearest.transform.position).magnitude <= range) {
                    body.Halt();
                    nearest.bp.woodQty += body.bp.woodQty;
                    body.bp.woodQty = 0;
                } else {
                    body.targetPosition = nearest.transform.position;
                }
            }
        }
        //if arms are full && resource exists
        else if (body.bp.woodQty >= 2) {
            if (nearest == null) {
                Debug.Log("ERROR! NO WAREHOUSE FOUND!");
                return;
            }
            //if near the warehouse
            if ((body.transform.position - nearest.transform.position).magnitude <= range) {
                body.Halt();
                nearest.bp.woodQty += body.bp.woodQty;
                body.bp.woodQty = 0;
            } else {
                body.targetPosition = nearest.transform.position;
            }
        } //else if arms are not full
        else {
            //if in harvesting range
            if ((cmd.resource.transform.position - body.transform.position).magnitude <= range) {
                body.Halt();
                body.bp.woodQty += cmd.resource.Harvest();
                if (body.bp.woodQty >= 2) {
                    nearest = body.player.getWarehouses()[0];
                }
            }//else if out of range
            else {
                body.targetPosition = cmd.resource.transform.position;
            }
        }
    }
}
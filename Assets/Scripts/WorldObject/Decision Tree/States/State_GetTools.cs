using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_GetTools : State {

    Warehouse target;
    Tool.TOOLTYPES toolType = Tool.TOOLTYPES.NONE;

    State_Goto move;

	public State_GetTools(DecisionTree parent) : base(parent) {
        move = new State_Goto(parent);
	}

	//each state must DO something
	public override void Act () {
		//fail if the warehouse is invalid or the tools are not available
		if (target == null || toolType == Tool.TOOLTYPES.NONE || parent.GetUnit().player.ist.GetQty(toolType) <= 0) {
			flow = FLOW.pass;
            Debug.Log("Tool get failed!");
            Debug.Log(target);
            Debug.Log(toolType);
            Debug.Log(parent.GetUnit().player.ist.GetQty(toolType));
			return; //pass to skip over this step if tools are not available
		}
        if (parent.GetUnit().tool.GetToolType() == toolType) {
            flow = FLOW.pass;
            return; //don't rebuild the entire brain if we already had the tool!
        }

        //move to the nearest warehouse
        Debug.Log("Moving!");
        move.Act();
        //trade in for the current tool
        if (move.getFlow() == FLOW.pass) {
            //trade tools
            Tool t = parent.GetUnit().player.ist.take(toolType);
            if (t != null) {
                Tool old = parent.GetUnit().EquipTool(t);
                parent.GetUnit().player.ist.Add(old);
            } else {
                flow = FLOW.fail;
            }

            flow = FLOW.pass;
        } else { //we're either waiting, running, or failing
            flow = move.getFlow();
        }

        if (flow == FLOW.pass) {
            parent.Reset(); //reset the brain!
        }

	}

	//each sate must reset-able
	public override void Reset() {
		flow = FLOW.fresh;
        target = null;
        toolType = Tool.TOOLTYPES.NONE;

        move.Reset();
    }

	//// New functionality ////
	public void SetCommand(Command cmd) {
        toolType = Tool.TOOLTYPES.NONE;
        //set the tool type
        switch (cmd.type) {
            case Command.TYPES.BUILD:
                toolType = Tool.TOOLTYPES.SAW;
                break;
            case Command.TYPES.HARVEST:
                toolType = Tool.TOOLTYPES.AXE;
                break;
            case Command.TYPES.FARM:
                toolType = Tool.TOOLTYPES.SCYTHE;
                break;
            case Command.TYPES.CRAFT:
                toolType = Tool.TOOLTYPES.HAMMER;
                break;
            case Command.TYPES.ATTACK:
            case Command.TYPES.GARRISON:
            case Command.TYPES.MOVE:
            case Command.TYPES.NONE:
                toolType = Tool.TOOLTYPES.NONE;
                break;
        }
        Debug.Log("Set! Getting tool " + toolType);
        //TODO get the nearest workshop
        target = RTS.Utility.GetNearestWarehouse(parent.GetUnit().transform.position, (parent.GetUnit().player.getWarehouses()));

        move.SetPosition(target.accessPoint.position);
    }



}

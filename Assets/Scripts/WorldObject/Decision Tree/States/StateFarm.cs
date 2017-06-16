using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateFarm : State {

    Warehouse nearest;


	public StateFarm(DecisionTree parent) : base(parent, DecisionTree.STATES.FARM) {

	}

	public override bool Update(List<Command> commands) {
        if (parent.currentCommand.type == Command.TYPES.FARM) {
            return true;
        }
        bool avail = false;
        foreach (Command cmd in commands) {
            if (cmd.type == Command.TYPES.FARM && cmd.actors.Count < 1) {
                parent.SetCommand(cmd);
                avail = true;
                break;
            }
        }

        return avail;
    }

	public override void Act() {
        float range = 2.5f; //range to get to the farm

        //get necessary objects
        Unit body = parent.GetUnit();
        CmdFarm cmd = (CmdFarm)(parent.currentCommand);

        //TODO implement -- walk to the farm and hang out I guess
        //if inventory is full, walk to the nearest warehouse to drop it off
        if (body.bp.cornQty >= 1) {
            if (nearest == null) {
                Debug.Log("ERROR! NO WAREHOUSE FOUND!!!");
                return;
            }

            //if within range of a warehouse, dump everything off!
            if ((body.transform.position - nearest.transform.position).magnitude < range) {
                nearest.bp.cornQty += body.bp.cornQty;
                body.bp.cornQty = 0;
            } else { //move toward nearest
                body.targetPosition = nearest.transform.position;
            }
        }
        //elseif not near the farm, move towards it
        else if ((body.transform.position - cmd.farm.transform.position).magnitude > range) {
            body.targetPosition = cmd.farm.transform.position;
        } else {
            body.targetPosition = body.transform.position;
            //if !harvestable, water
            if (!cmd.farm.isHarvestable()) {
                cmd.farm.Work();
            } else {
                int qty = 0;
                qty = cmd.farm.Harvest();
                body.bp.cornQty += qty;
                if (qty == 1) { //TODO get nearest warehouse
                    nearest = body.player.getWarehouses()[0];
                }
            }
        }
	}
}
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateFarm : State {

    Warehouse nearest;


	public StateFarm(DecisionTree parent) : base(parent, DecisionTree.STATES.FARM) {

	}

	public override bool Update(List<Command> commands) {
        if (parent.getCommand().type == Command.TYPES.FARM) {
            return true;
        }
        bool avail = false;
        foreach (Command cmd in commands) {
            if (cmd.paused) {
                continue;
            }
            if (cmd.type == Command.TYPES.FARM && cmd.actors.Count < 1) {
                parent.SetCommand(cmd);
                avail = true;
                break;
            }
        }

        return avail;
    }

	public override void Act() {
        float range = 0.75f; //range to get to the farm

        //get necessary objects
        Unit body = parent.GetUnit();
        CmdFarm cmd = (CmdFarm)(parent.getCommand());

        //TODO implement -- walk to the farm and hang out I guess
        //if inventory is full, walk to the nearest warehouse to drop it off
        if (body.bp.res.corn >= 1) {
            if (nearest == null) {
                Debug.Log("ERROR! NO WAREHOUSE FOUND!!!");
                return;
            }

            //if within range of a warehouse, dump everything off!
            if ((body.transform.position - nearest.accessPoint.position).magnitude < range) {
                nearest.bp.Transfer(ref body.bp.res);
            } else { //move toward nearest
                body.MoveTo(nearest.accessPoint.position);
            }
        }
        //elseif not near the farm, move towards it
        else if ((body.transform.position - cmd.farm.accessPoint.position).magnitude > range*3) {
            body.MoveTo(cmd.farm.accessPoint.position);
        } else {
            body.MoveTo(body.transform.position);
            //if !harvestable, water
            if (!cmd.farm.isHarvestable()) {
                cmd.farm.Work();
            } else {
                int qty = 0;
                qty = cmd.farm.Harvest();
                body.bp.Add(new Backpack.Resources(qty, 0, 0, 0));
                if (qty == 1) { //TODO get nearest warehouse
                    nearest = RTS.Utility.GetNearestWarehouse(body.transform.position, body.player.getWarehouses());
                }
            }
        }
	}
}
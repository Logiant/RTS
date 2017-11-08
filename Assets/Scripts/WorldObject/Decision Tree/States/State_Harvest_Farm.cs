using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Harvest_Farm : State {

    Field target;

    State_Trade move;

    bool oneshot = true;

    public State_Harvest_Farm(DecisionTree parent) : base(parent) {
        move = new State_Trade(parent);
        Reset();
    }

    //each state must DO something
    public override void Act() {
        if (target == null) {
            flow = FLOW.fail;
            return;
        }
        if (target.isHarvestable() && oneshot) {
            oneshot = false;
            int food = target.Harvest();
            parent.GetUnit().bp.Add(new Backpack.Resources(food, 0, 0, 0));
            flow = FLOW.run;

            Warehouse nearest = RTS.Utility.GetNearestWarehouse(parent.GetUnit().transform.position, parent.GetUnit().player.getWarehouses());

            move.SetTrade(nearest.accessPoint.transform.position, ref nearest.bp, parent.GetUnit().bp.res);
        }
        else if (!oneshot) {
            move.Act();
            flow = move.getFlow();
        }
    }

    //each sate must reset-able
    public override void Reset() {
        flow = FLOW.fresh;
        target = null;
        oneshot = true;
    }

    //// New functionality ////
    public void SetTarget(Field target) {
        this.target = target;

    }

}

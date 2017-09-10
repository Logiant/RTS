using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_CmdIdle : CommandState {

	//child states of Gather
	State_Goto move;   //random movement
    State_Trade trade; //deliver goods if inventory isnt empty
    //helper timer
    float timer = 0f;
    float cd_time = 10f;

    //set a random displacement
    Vector3 disp = new Vector3();


	//get the parent tree in the constructor
	public State_CmdIdle(DecisionTree p) : base(p) {
	
		move = new State_Goto (p);
        trade = new State_Trade(p);
        type = Command.TYPES.NONE;
        Reset ();
	
	}

	public override bool Update(List<Command> commands) {
        parent.ClearCommand();

		return true;
	}

	//each state must DO something
	public override void Act () {
        //if we are holding items, return them
        if (parent.GetUnit().bp.resourceQty() != 0 && trade.getFlow() != State.FLOW.fail) {
            //TODO do a check so we don't constantly write this
            Warehouse nearest = RTS.Utility.GetNearestWarehouse(parent.GetUnit().transform.position, parent.GetUnit().player.getWarehouses());
            trade.SetTrade(nearest.accessPoint.position, ref nearest.bp, parent.GetUnit().bp.res);
            trade.Act();
        } else {//move randomly
            move.Act();
            if (move.getFlow() == FLOW.pass) {
                timer -= Time.deltaTime;
            }
            if (timer <= 0) {
                timer = cd_time + Random.Range(-5f, 5f); ;
                Reset();
            }
        }
	}

	//each sate must reset-able
	public override void Reset() {
		flow = FLOW.run;
		move.Reset ();
        trade.Reset();
        disp.x = Random.Range(-5f, 5f);
        disp.z = Random.Range(-5f, 5f);
        move.SetPosition(parent.GetUnit().transform.position + disp);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workshop : Structure {

    CmdCraft cmd;

    Unit worker;

    float craftTime = 10;
    float timer = 0;

    public int toolDemand = 0;
    public int axeDemand = 0;
    public int scytheDemand = 0;
    public int sawDemand = 0;
    public int hammerDemand = 0;

    // Use this for initialization
    public override void Start () {
        base.Start();
        cmd = player.craft;
        cmd.AddWorkshop(this);
        timer = craftTime;
    }

    public bool IsOccupied() {
        return worker != null;
    }

    public int GetDemand() {
        return cmd.TotalDemand();
    }

    public void Craft() {
        timer -= Time.deltaTime;
        if (timer <= 0 && player.bp.res.metal >= 2 && cmd.TotalDemand() > 0) {
            player.bp.Remove(new Backpack.Resources(0, 0, 2, 0));
            //pick tool type and create it
            int[] demand = cmd.GetDemand();
            if (demand[0] > 0) {
                //axe
                player.ist.Add(new Tool(Tool.TOOLTYPES.AXE));
            } else if(demand[1] > 0) {
                //scythe
                player.ist.Add(new Tool(Tool.TOOLTYPES.SCYTHE));
            } else if(demand[2] > 0) {
                //saw
                player.ist.Add(new Tool(Tool.TOOLTYPES.SAW));
            } else if (demand[3] > 0) {
                //hammer
                player.ist.Add(new Tool(Tool.TOOLTYPES.HAMMER));
            }
            player.ist.Add(new Tool());
            //reset craft timer
            timer = craftTime;
        }
    }

    // Update is called once per frame
    public override void Update() {

    }

}

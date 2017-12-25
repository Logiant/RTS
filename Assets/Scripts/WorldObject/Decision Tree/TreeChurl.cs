using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Decision tree specific to the Churl unit
 */
public class TreeChurl : DecisionTree {
    //TODO store each state as a reference, and just dynamically shift their locations in the decision tree???
    //will that save memory and processing power? Will it allow uninterrupted completion of commands??

    State_CmdBuild build;
    State_CmdGather gather;
    State_CmdFarm farm;
    State_CmdCraft craft;

    State_CmdMove move;
    State_CmdIdle idle;
    
    public TreeChurl(Unit body) : base(body) {
        //initialize command states 
        build = new State_CmdBuild(this);
        gather = new State_CmdGather(this);
        farm = new State_CmdFarm(this);
        craft = new State_CmdCraft(this);
        move = new State_CmdMove(this);
        idle = new State_CmdIdle(this);
        //tree priority goes from left to right
        //it's effectively a FIFO queue with depth-first search
        BuildTree(body);
    }

    public override void Reset() {
        //clear tree
        tree.RemoveRange(0, tree.Count);
        //rebuild
        BuildTree(parent);
    }

    protected void BuildTree(Unit body) {
        //labor is the equally-likely tasks
        List<CommandState> labor = new List<CommandState>(4);
        //add the primary and labor tasks to the tree
        switch (body.GetUnitType()) {
            case TYPES.GATHERER:
                labor.Add(build);
                tree.Add(gather);
                labor.Add(farm);
                labor.Add(craft);
                break;
            case TYPES.FARMER:
                labor.Add(build);
                labor.Add(gather);
                tree.Add(farm);
                labor.Add(craft);
                break;
            case TYPES.LABORER:
                tree.Add(build);
                labor.Add(gather);
                labor.Add(farm);
                labor.Add(craft);
                break;
            case TYPES.CRAFTSMAN:
                labor.Add(build);
                labor.Add(gather);
                labor.Add(farm);
                tree.Add(craft);
                break;
            case TYPES.SOLDIER:
            case TYPES.CHURL:
                labor.Add(build);
                labor.Add(gather);
                labor.Add(farm);
                labor.Add(craft);
                break;
        }

        //shuffle labor priority
        labor = RTS.Utility.Shuffle(labor);
        tree.AddRange(labor);
        //add the move and idle states at the end
        tree.Add(move);
        tree.Add(idle);
    }
}

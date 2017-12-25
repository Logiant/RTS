using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmdFarm : Command {

    public Field farm;

    //TODO this is only for the move command
    public CmdFarm(Field farm) : base(TYPES.FARM, farm.transform.position, farm.player) {
        this.farm = farm;
    }

    public override string Text() {
        return "Farming " + farm.transform.position + ", " + actors.Count;

    }
}

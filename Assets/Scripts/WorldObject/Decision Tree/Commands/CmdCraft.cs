using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * commands are assigned by the player (or AI)
 * examples:
 * 		move to X,Y,Z
 * 		craft item I
 * 		attack ZZ
 * 		Harvest X
 * 		Build structure b
 * 
 */
public class CmdCraft : Command {

	public List<Workshop> workshops;

    public int axeDemand = 0;
    public int scytheDemand = 0;
    public int sawDemand = 0;
    public int hammerDemand = 0;

    public CmdCraft(Player owner) : base(TYPES.CRAFT, RTS.GameState.InvalidPosition, owner) {
        workshops = new List<Workshop>();
	}

    public void AddWorkshop(Workshop building) {
        workshops.Add(building);
    }

    private void UpdateDemand() {
        //calculate demand
        axeDemand = Mathf.Max(workshops.Count - owner.ist.GetQty(Tool.TOOLTYPES.AXE));
        scytheDemand = Mathf.Max(workshops.Count - owner.ist.GetQty(Tool.TOOLTYPES.SCYTHE));
        sawDemand = Mathf.Max(workshops.Count - owner.ist.GetQty(Tool.TOOLTYPES.SAW));
        hammerDemand = Mathf.Max(workshops.Count - owner.ist.GetQty(Tool.TOOLTYPES.HAMMER));
    }

    public Workshop GetOpenWorkshop() {
        foreach (Workshop w in workshops) {
            Debug.Log(w);
            if (!w.IsOccupied()) {
                return w;
            }
        }
        return null;
    }

    public int[] GetDemand() {
        //calculate demand
        UpdateDemand();

        return new int[] {axeDemand, scytheDemand, sawDemand, hammerDemand };
    }

    public int TotalDemand() {
        UpdateDemand();
        return axeDemand + scytheDemand + sawDemand+hammerDemand;
}

	public override string Text() {
		return "Craft at " + workshops.Count + " by " + actors.Count;

	}
}

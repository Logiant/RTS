using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStorage  {

    List<Tool> tools;

    public ItemStorage() {
    //    items = new List<Item>();
        tools = new List<Tool>();
    }

    //TODO RequestItem() -- increments demand by 1, Take decrements demand by 1, CancelOrder() decrements command by 1
    //if demmand <=0, turn crafting job on!

    //TODO
    //getTool(enum type);
    //getWeapon(enum type);
    public void Add(Tool i) {
        tools.Add(i);
    }

    public bool HasItems() {
        return tools.Count > 0;
    }

    public Tool take(Tool.TOOLTYPES tp) {
        Tool t = null;
        for (int i = 0; i < tools.Count; i++) {
            if (tools[i].GetToolType() == tp) {
                t = tools[i];
                tools.RemoveAt(i);
                break;
            }
        }
        return t;
    }

    public int GetQty(Tool.TOOLTYPES tp) {
        int qty = 0;
        foreach (Tool t in tools) {
            if (t.GetToolType() == tp) {
                qty++;
            }
        }
        return qty;
    }

    public int[] GetQty() {
        return new int[] {GetQty(Tool.TOOLTYPES.AXE), GetQty(Tool.TOOLTYPES.HAMMER), GetQty(Tool.TOOLTYPES.SAW), GetQty(Tool.TOOLTYPES.SCYTHE)};
    }
}

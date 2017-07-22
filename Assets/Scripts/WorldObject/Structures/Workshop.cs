using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workshop : Structure {


    float craftTime = 10;
    float timer = 0;

    public int toolDemand = 0;

    // Use this for initialization
    public override void Start () {
        base.Start();
        player.AddCommand(new CmdCraft(this));
        timer = craftTime;
    }

    public int GetDemand() {
        return 5 - player.ist.GetNumTools();
    }

    public void Craft() {
        timer -= Time.deltaTime;
        if (timer <= 0 && player.bp.res.metal >= 2) {
            player.bp.Remove(new Backpack.Resources(0, 0, 2, 0));
            player.ist.Add(new Tool());
            timer = craftTime;
        }
    }

    // Update is called once per frame
    public override void Update() {

    }

}

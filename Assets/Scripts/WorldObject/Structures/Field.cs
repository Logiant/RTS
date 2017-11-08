using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : Structure {

    public GameObject crop;
    float minCropHt = -1.58f;
    float maxCropHt = 0.3f;

    public int currentTicks = 0;
    int maxTicks = 10;

	// Use this for initialization
	public override void Start () {
        base.Start();
        player.AddCommand(new CmdFarm(this));

        Vector3 cropPos = crop.transform.position;
        cropPos.y = minCropHt;
        crop.transform.position = cropPos;
    }

    public void Tend() {
        //add some crops or some shit
        if (currentTicks < maxTicks) {
            currentTicks++;

            Vector3 cropPos = crop.transform.position;
            cropPos.y = (currentTicks/(float)maxTicks)*(maxCropHt - minCropHt) + minCropHt;
            crop.transform.position = cropPos;
        }
    }

    public bool isHarvestable() {
        return currentTicks == maxTicks;
    }

    public int Harvest() {
        Vector3 cropPos = crop.transform.position;
        cropPos.y = minCropHt;
        crop.transform.position = cropPos;

        currentTicks = 0;
        return maxTicks;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warehouse : Structure {

    public Backpack bp;

    private Backpack reserve;

    // Use this for initialization
    public override void Start() {
        base.Start();
        bp = new Backpack();

        reserve = new Backpack();
    }


    //TOOD its not guaranteed that these are always valid
    public void Reserve(Backpack.Resources res) {
        bp.Remove(res);
        reserve.Add(res);
    }

    public Backpack.Resources TakeFromReserve(Backpack.Resources amount) {
        reserve.Remove(amount);
        return amount; //reserve.Remove(amount); //TODO return amount taken?
    }

}

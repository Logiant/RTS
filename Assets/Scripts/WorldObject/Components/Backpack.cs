using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack {

    public struct Resources {
        public int corn;
        public int wood;
        public int metal;

        public Resources(Resources other) {
            corn = other.corn;
            wood = other.wood;
            metal = other.metal;
        }

        public Resources(int c, int w, int m, int t) {
            corn = c; wood = w; metal = m;
        }

        public void Add(Resources other) {
            corn += other.corn;
            wood += other.wood;
            metal += other.metal;
        }

        public void Remove(Resources other) {
            corn -= other.corn;
            wood -= other.wood;
            metal -= other.metal;
        }

        public void Clear() {
            corn = 0;
            wood = 0;
            metal = 0;
        }

        public override string ToString() {
            return "Corn: " + corn + " Wood: " + wood + " Metal: " + metal;
        }
    }


    public Resources res = new Resources();

    public Item item = null;

    public int resourceQty() {
        return res.wood + res.metal;
    }

    public bool giveItem(Item toGive) {
        bool hadItem = (item == null);
        if (!hadItem) {
            item = toGive;
        }
        return hadItem;
    }

    public Item takeItem() {
        Item toGive = item;
        item = null;
        return toGive;
    }

    public bool Contains (Backpack.Resources other) {
        return (res.corn >= other.corn & res.wood >= other.wood & res.metal >= other.metal);
    }

    public void Clear() {
        res.Clear();
    }

    public void Add(Resources other) {
        res.Add(other);
    }

    public void Remove(Resources other) {
        res.Remove(other);
    }

    public void Transfer(ref Resources other) {
        Add(other);
        other.Clear();
    }

}

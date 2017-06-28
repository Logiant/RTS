using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack {

    public struct Resources {
        public int corn;
        public int wood;
        public int metal;
        public int tools;

        public Resources(Resources other) {
            corn = other.corn;
            wood = other.wood;
            metal = other.metal;
            tools = other.tools;
        }

        public Resources(int c, int w, int m, int t) {
            corn = c; wood = w; metal = m; tools = t;
        }

        public void Add(Resources other) {
            corn += other.corn;
            wood += other.wood;
            metal += other.metal;
            tools += other.tools;
        }

        public void Remove(Resources other) {
            corn -= other.corn;
            wood -= other.wood;
            metal -= other.metal;
            tools -= other.tools;
        }

        public void Clear() {
            corn = 0;
            wood = 0;
            metal = 0;
            tools = 0;
        }

        public override string ToString() {
            return "Corn: " + corn + " Wood: " + wood + " Metal: " + metal + " Tools: " + tools ;
        }
    }


    public Resources res = new Resources();

    public int resourceQty() {
        return res.wood + res.metal;
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

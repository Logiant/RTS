using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS {

    public static class Utility {

        static System.Random rng = new System.Random();

        public static Warehouse GetNearestWarehouse(Vector3 origin, List<Warehouse> goals) {
            Warehouse nearest = goals[0];
            float distanceSqr = Mathf.Infinity;

            foreach (Warehouse wh in goals) {
                Vector3 ds = (origin - wh.transform.position);
                if ((ds.sqrMagnitude >= 1 && ds.sqrMagnitude < distanceSqr) || (ds.sqrMagnitude <= 1 && ds.sqrMagnitude > distanceSqr)) {
                    distanceSqr = ds.sqrMagnitude;
                    nearest = wh;
                }


            }


            return nearest;
        }


        public static List<CommandState> Shuffle(List<CommandState> list) {
            int n = list.Count;
            while (n > 1) {
                n--;
                int k = rng.Next(n + 1);
                CommandState value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            return list;
        }

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS {

    public static class Utility {

       public static Warehouse GetNearestWarehouse(Vector3 origin, List<Warehouse> goals) {
            Warehouse nearest = goals[0];
            float distanceSqr = Mathf.Infinity;
            
            foreach (Warehouse wh in goals) {
                Vector3 ds = (origin - wh.transform.position);
                if ( (ds.sqrMagnitude >= 1 && ds.sqrMagnitude < distanceSqr) || (ds.sqrMagnitude <= 1 && ds.sqrMagnitude > distanceSqr) ) {
                    distanceSqr = ds.sqrMagnitude;
                    nearest = wh;
                }


            }            


            return nearest;
        }


    }

}

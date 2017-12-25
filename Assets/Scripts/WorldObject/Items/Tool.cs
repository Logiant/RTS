using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : Item {

    // Use this for initialization

    TOOLTYPES type = TOOLTYPES.NONE;

    public Tool(TOOLTYPES t) {
        type = t;
    }

    public Tool() :  this(TOOLTYPES.NONE) {
        
    }

    public enum TOOLTYPES {
        AXE,
        SCYTHE,
        SAW,
        HAMMER,
        NONE
    }

    public TOOLTYPES GetToolType() {
        return type;
    }

    public override string ToString() {
        string txt = "";
        switch (type) {
            case TOOLTYPES.AXE:
                txt = "Axe";
                break;
            case TOOLTYPES.SCYTHE:
                txt = "Scythe";
                break;
            case TOOLTYPES.SAW:
                txt = "Saw";
                break;
            case TOOLTYPES.HAMMER:
                txt = "Hammer";
                break;
            case TOOLTYPES.NONE:
                txt = "Hands";
                break;
        }
        return txt;
    }

}

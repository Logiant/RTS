using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RTS;

public class UI_Root : MonoBehaviour {

	UI_Base[] children;


	void Awake() {
		GameState.ui = this;
	}

	void Start() {
		children = GetComponentsInChildren<UI_Base> ();
	}

	//check if the UI wants to use a left click
	//
	public bool LeftClick() {
		bool ready = false;
		for (int i = 0; i < children.Length; i++) {
			if (children [i].LeftClick ()) {
				ready = true;
				break;
			}
		}
		return ready;
	}

    public bool RightClick() {
        bool ready = false;
        for (int i = 0; i < children.Length; i++) {
            if (children[i].RightClick()) {
                ready = true;
                break;
            }
        }
        return ready;
    }

}

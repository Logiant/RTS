using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandFlag : MonoBehaviour {

    public Command cmd;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (cmd == null || cmd.complete == true) {
            Destroy(this.gameObject);
        }
	}
}

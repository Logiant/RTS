using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CMD_BAR_DEBUG : MonoBehaviour {

    Text text;

    public Player player;

	// Use this for initialization
	void Start () {
        text = GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        string cmdStr = "Commands:\n-------------------------------------\n";

        foreach (Command cmd in player.activeCommands) {

            cmdStr += cmd.Text() + "\n";
        }

        text.text = cmdStr;
	}
}

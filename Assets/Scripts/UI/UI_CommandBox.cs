using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RTS;

public class UI_CommandBox : UI_Base {

    public GameObject button;

    List<GameObject> buttons;

    Text text;

    CommandFlag cmd;


    // Use this for initialization
    void Start () {
        text = GetComponentInChildren<Text>();

        buttons = new List<GameObject>();
        //create two buttons, set their text, add listeners, disable them
        buttons.Add(Instantiate(button, this.gameObject.transform) as GameObject);
        buttons.Add(Instantiate(button, this.gameObject.transform) as GameObject);
        //set text
        buttons[0].GetComponentInChildren<Text>().text = "Toggle";
        buttons[1].GetComponentInChildren<Text>().text = "Cancel";
        //adjust position
        buttons[0].transform.position = buttons[0].transform.position + new Vector3(5, -20, 0);
        buttons[1].transform.position = buttons[1].transform.position + new Vector3(85, -20, 0);
        //add click callback
        buttons[0].GetComponent<Button>().onClick.AddListener(() => SwitchCommand(buttons[0]));
        buttons[1].GetComponent<Button>().onClick.AddListener(() => SwitchCommand(buttons[1]));
        //hide
        Hide();
    }

    protected bool CanCancel(Command cmd) {
        return cmd.type == Command.TYPES.MOVE || cmd.type == Command.TYPES.ATTACK ||
               cmd.type == Command.TYPES.GARRISON || cmd.type == Command.TYPES.HARVEST ||
               cmd.type == Command.TYPES.BUILD;
    }

    public void SetButtonText() {
        if (cmd.cmd.paused) {
            buttons[0].GetComponentInChildren<Text>().text = "Resume";
        } else {
            buttons[0].GetComponentInChildren<Text>().text = "Pause";
        }
    }

    //command passed from button children
    public void SwitchCommand(GameObject b) {
        int index = buttons.IndexOf(b);
        if (index == 0) {
            //start/stop this command
            cmd.cmd.paused = !cmd.cmd.paused;
            SetButtonText();
        } else {
            //delete this command!
            cmd.cmd.Cancel();
        }


    }

    // Update is called once per frame
    void Update () {
        if (GameState.hasSelected && (cmd = GameState.Selected.GetComponent<CommandFlag>()) != null) {
            //get the command from CMD and display it
            //show two buttons, Start/Stop and Cancel
            text.text = cmd.cmd.Text();
            buttons[0].SetActive(true);
            if (CanCancel(cmd.cmd)) {
                buttons[1].SetActive(true);
            }
            SetButtonText();
        } else {
            Hide();
            cmd = null;
        }

	}

    void Hide() {
        text.text = "No Command Selected";
        buttons[0].SetActive(false);
        buttons[1].SetActive(false);
    }

    public override bool LeftClick() {
        return false;
    }

    public override bool RightClick() {
        return false;
    }

    public override void Mouseover(GameObject go) {
        //TODO implement if required
    }

    public override void MouseExit(GameObject go) {
        //TODO implement if required
    }
}

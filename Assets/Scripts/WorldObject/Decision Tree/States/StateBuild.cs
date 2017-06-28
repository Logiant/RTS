using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBuild : State {

	public StateBuild(DecisionTree parent) : base(parent, DecisionTree.STATES.BUILD) {

	}

	public override bool Update(List<Command> commands) {
        if (parent.getCommand().type == Command.TYPES.BUILD) {
            return true;
        }
        bool avail = false;
        foreach (Command cmd in commands) {
            if (cmd.paused) {
                continue;
            }
            if (cmd.type == Command.TYPES.BUILD && cmd.actors.Count < 1) {
                parent.SetCommand(cmd);
                avail = true;
                break;
            }
        }

		return avail;
	}

	public override void Act() {

        //get necessary objects
        Unit body = parent.GetUnit();
        CmdConstruct cmd = (CmdConstruct)(parent.getCommand());

        //check to make sure it's not already built
        if (cmd.foundation == null) {
            parent.ClearCommand();
            return;
        }

        Vector3 center = cmd.foundation.transform.position + new Vector3(0, 0.5f, 0);
        //TODO magic number!
        //TODO foundation should set "build points" and max # of builders
        if ((center - body.transform.position).magnitude < 5) {
            //stop moving
            body.MoveTo(body.transform.position);
            //build
            cmd.foundation.Increment();
        } else if (cmd.foundation != null) {
            body.MoveTo(cmd.foundation.transform.position + new Vector3(0, 0.5f, 0));
        } else {
            body.MoveTo(body.transform.position);
        }
    }



}
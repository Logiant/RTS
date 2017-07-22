using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Decision tree specific to the Churl unit
 */
public class TreeChurl : DecisionTree {

	public TreeChurl(Unit body) : base(body) {
		//tree priority goes from left to right
		//it's effectively a FIFO queue with depth-first search

		//the labor branch is all equally-likely labor tasks
		tree.Add (new State_CmdGather (this));

		tree.Add (new State_CmdMove (this));
	}
}

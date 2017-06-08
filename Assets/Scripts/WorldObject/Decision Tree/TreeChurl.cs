using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Decision tree specific to the Churl unit
 */
public class TreeChurl : DecisionTree {

	public TreeChurl(Unit parent) : base(parent) {
		//tree priority goes from left to right
		//it's effectively a FIFO queue

		//the labor branch is all equally-likely labor tasks
		List<State> labor = new List<State> ();
		//TODO add each labor state to the queue


		tree.Add (new NodeAnyOrder (this, labor));
		tree.Add (new StateMove (this));
		tree.Add (new StateIdle (this));


	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Decision tree specific to the Churl unit
 */
public class TreeChurl : DecisionTree {

	public TreeChurl(Unit prnt) : base(prnt) {
		//tree priority goes from left to right
		//it's effectively a FIFO queue

		//the labor branch is all equally-likely labor tasks
		List<State> labor = new List<State> ();
		//TODO randomize labor task order per unit
		labor.Add(new StateBuild(this));
		labor.Add (new StateCraft (this));
		labor.Add (new StateGather (this));
		labor.Add (new StateFarm (this));
		//create the main behavior tree
		tree.Add (new StateSurvive (this));
		tree.Add (new NodeAnyOrder (this, labor));
		tree.Add (new StateMove (this));
		tree.Add (new StateIdle (this));
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 * Units can:
 * 	-move
 *  -produce items
 *  -fight
 *  -equip items
 * 
 * Units have the component
 * 	-inventories
 */

//unit type is based on current equpment
//	-this prioritizes tasks for the unit
public enum TYPES {
	CHURL,		//basic human, 		no equipment
	GATHERER,	//gathers resources,equip tools [axe, pick]
	FARMER,		//farms,			equip scythe
	LABORER,	//builds structures,equip hammer
	CRAFTSMAN,	//builds items,		equip crafter
	SOLDIER		//fights,			equip weapon
}

//Begin the Unit class
public class Unit : WorldObject {
	//unit type - defines behavior, 
	TYPES type = TYPES.CHURL;
    //decision tree - controls behavior
    DecisionTree brain;
    //backpack - handles items
    public Backpack bp;
    //navmeshagent for movement
    protected NavMeshAgent nav;
    //equipped tool
    public Tool tool = new Tool();

	//movement TODO should this be in a separate script for readability? probably
	protected Vector3 targetPosition;
	public float speed = 3.5f; // m/s

    //animator and other aesthetics
    protected Animator anim;
    //mount locations
    public GameObject RHand;
    private GameObject equipped;

    //prefabs -- MOVE THIS INTO THE TOOL CLASS AND DO TOOL.GETMODEL()
    public GameObject axe;
    public GameObject hammer;
    public GameObject saw;
    public GameObject scythe;


    // Use this for initialization
    public override void Start () {
        bp = new Backpack();
        nav = GetComponent<NavMeshAgent>();
		targetPosition = transform.position;
		base.Start();
        brain = new TreeChurl(this);
        PickState (); //instantiates brain
        player.addUnit(this);

        anim = GetComponent<Animator>();
	}
	//set target position
    public void MoveTo(Vector3 target) {
        targetPosition = target;
        nav.SetDestination(targetPosition);
        nav.isStopped = (targetPosition == transform.position);
    }

    public Vector3 GetTarget() {
        return targetPosition;
    }

	// Update is called once per frame
	public override void Update () {
		base.Update ();
		//TODO remove after done debugging
		if (brain != null) {
			brain.Update (this.player.activeCommands); //feed in active commands from the player
		}
        //update animations
        if (anim != null) {
            anim.SetFloat("Speed", nav.velocity.magnitude / nav.speed);

            anim.SetBool("Punching", brain.getCommand().type != Command.TYPES.MOVE && brain.getCommand().type != Command.TYPES.NONE && anim.GetFloat("Speed") < 0.05);
        }
	}

    public void Halt() {
        MoveTo(transform.position);
    }

    public Tool EquipTool(Tool t) {
        Tool tmp = tool;
        tool = t;
        Debug.Log(t.GetToolType() + " TOOL TYPE");
        if (equipped != null) {
            Destroy(equipped);
        }
        switch(t.GetToolType()) {
            case Tool.TOOLTYPES.AXE:
                type = TYPES.GATHERER;
                equipped = Instantiate(axe, RHand.transform);
                break;
            case Tool.TOOLTYPES.HAMMER:
                type = TYPES.CRAFTSMAN;
                equipped = Instantiate(hammer, RHand.transform);
                break;
            case Tool.TOOLTYPES.SCYTHE:
                type = TYPES.FARMER;
                equipped = Instantiate(scythe, RHand.transform);
                break;
            case Tool.TOOLTYPES.SAW:
                type = TYPES.LABORER;
                equipped = Instantiate(saw, RHand.transform);
                break;
            case Tool.TOOLTYPES.NONE:
                type = TYPES.CHURL;
                break;
        }
        equipped.transform.Rotate(equipped.transform.right, 180);
        PickState();
        return tmp;
    }

	// when a new item has been equipped
	// update this units decision tree
	private void PickState() {
        //TODO clear the old brain
        brain.Reset();
	}

    public TYPES GetUnitType() {
        return type;
    }
}
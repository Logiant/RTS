using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RTS;

public class UserInput : MonoBehaviour {

	private Player player;

	//used to determine if the mouse is being dragged
	private Vector3 dragStart 	= Vector3.zero;
	private float dragTime = 0.1f; //time before a click is considered a drag - probably belongs in ResourceManager
	private float dragTimeFull = 0.1f; //time before a click is considered a drag - probably belongs in ResourceManager


	// Use this for initialization
	void Start () {
		player = GetComponent<Player> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (player.isHuman) {

			/*** KEYOBARD INPUT ***/
			//handle keyboard input
			cameraMovement();
			//if W/S/A/D || U/D/L/R || screen panning --> move the camera
				//otherwise try to do something with command hotkeys for the selected unit?

			/*** MOUSE INPUT ***/
			//if the left mouse is pressed down
			if (Input.GetMouseButtonDown (0)) {
				dragStart = Input.mousePosition;
				dragTime = dragTimeFull;
			}
			//while the left click button is down
			if (Input.GetMouseButton (0)) {
				dragTime -= Time.deltaTime;
				if (dragTime <= 0) {
					GameState.mouseDrag = true;
					//DragHover() --> do a hover effect for everything drug over I suppose
				}
			}
			//when left click is released
			if (Input.GetMouseButtonUp (0)) {
				if (GameState.mouseDrag) {
					//LeftDrag(); --> drag action based on start and end of left click
					leftClick(); //TODO replace this with the drag function
				} else {
					leftClick ();
				}
				//reset resource manager drag information
				GameState.mouseDrag = false;
			}
				
			//right click handler
			if (Input.GetMouseButtonDown (1)) {
				rightClick ();
			}

		}
	}

	//handle keyboard
	void cameraMovement() {
		Vector3 motion = new Vector3 ();
		if (Input.GetKey (KeyCode.W)) {
			motion += Camera.main.transform.forward;
		}
		if (Input.GetKey (KeyCode.S)) {
			motion -= Camera.main.transform.forward;
		}
		if (Input.GetKey (KeyCode.D)) {
			motion += Camera.main.transform.right;
		}
		if (Input.GetKey (KeyCode.A)) {
			motion -= Camera.main.transform.right;
		}

		motion.y = 0;
        motion.Normalize();

		Camera.main.transform.position += motion * Time.deltaTime * GameState.ScrollSpeed;


	}




	//handle mouse input
	//left click is only used to select things
	void leftClick() {
		//do nothing if the pointer is over a UI element or the UI wants to use it
		if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject () ||
			GameState.ui.LeftClick()) {
			return;
		}

		RaycastHit hit = mouseRaycast (Input.mousePosition);
		if (hit.collider == null) {
			//we clicked off the map somewhere, somehow
			if (!GameState.stickySelection && GameState.hasSelected) {
				Deselect ();
			}
			return;
		}

		GameObject clicked =  hit.collider.gameObject.transform.root.gameObject;
		WorldObject clickedObject = clicked.GetComponent<WorldObject> ();


		//if we left clicked and NOT (no clicked obj and sticky_selection is on) -> deselect the current thing
		// (if we're not dragging the mouse?)
		if (GameState.hasSelected && !(clickedObject == null && GameState.stickySelection) ) { //if something was is selected
			Deselect();
		}
		if (!GameState.hasSelected && clickedObject != null) { //if nothing is selected currently
			Select(clicked.GetComponent<WorldObject>());
		}

	}

	//helper functions
	void Select(WorldObject obj) {
		GameState.Selected = obj;
		obj.SetSelected (true);
	}

	void Deselect() {
		GameState.Selected.SetSelected(false);
		GameState.Selected = null;
	}

	void rightClick() {
		//do nothing if the pointer is over a UI element
		if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject () || 
            GameState.ui.RightClick()) {
			return;
		}
        //do nothing if we're cancelling a build job!!

		//we can't give commands if nothing is selected
	//	if (!GameState.hasSelected || GameState.Selected.player == null || !GameState.Selected.player.isHuman) {
	//		return;
	//	}
		RaycastHit hit = mouseRaycast (Input.mousePosition);
		//we can't give commands if nothing is right clicked
		if (hit.collider == null) {
			return;
		}

        //did we click a resource?
        Resource r;
        if ((r=hit.collider.GetComponentInParent<Resource>()) != null) {
            CmdHarvest hrv = new CmdHarvest(r);
            r.cmd = hrv;
            player.AddCommand(hrv);
        } else if (hit.collider.CompareTag("Terrain")) {
            player.AddCommand(new CmdMoveTo(hit.point));
        }

		//send a command (eg move, rally point, harvest, attack) to the currently selected object
		
	//	GameState.Selected.command (hit.collider.transform.root.gameObject, hit.point);

	}



	RaycastHit mouseRaycast(Vector3 mousePos) {

		RaycastHit hit;

		Physics.Raycast (Camera.main.ScreenPointToRay (mousePos), out hit);

		return hit;


	}


}

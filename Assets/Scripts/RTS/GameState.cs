using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS {

	public static class GameState {

		//human player
		public static Player player;
		//UI root
		public static UI_Root ui;

		//camera properties
		public static int ScrollWidth { get { return 15; } }
		public static float ScrollSpeed { get { return 25; } }
		public static float RotateAmount { get { return 10; } }
		public static float RotateSpeed { get { return 100; } }
        public static float ZoomSpeed { get { return 500; } }
        public static float MinCameraHeight { get { return 10; } }
		public static float MaxCameraHeight { get { return 40; } }

		//selection properties
		public static WorldObject Selected;
		public static bool hasSelected { get { return Selected != null; } }
		public static bool stickySelection { get { return false; } }

		//mouse properties
		public static bool mouseDrag;

		//error helpers
		static Vector3 invalidPosition = new Vector3(-99999, -99999, -99999);
		public static Vector3 InvalidPosition { get { return invalidPosition; } }

		static Bounds invalidBounds = new Bounds(new Vector3(-99999, -99999, -99999), new Vector3(99999, 99999, 99999));
		public static Bounds InvalidBounds { get { return invalidBounds; } }
	}

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UI_Base : MonoBehaviour {

	public abstract bool LeftClick ();

    public abstract bool RightClick();

    public abstract void Mouseover(GameObject go);

    public abstract void MouseExit(GameObject go);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_BuildRegion : MonoBehaviour {

	int count = 0;
	MeshRenderer mesh;

	GameObject ghost;

	Vector3 baseSize;

	// Use this for initialization
	void Start () {
		mesh = GetComponentInChildren<MeshRenderer> ();
		baseSize = mesh.transform.localScale;
		ColorMesh ();
	}

	public void Clear() {
		ghost = null;
		mesh.transform.localScale = baseSize;
		mesh.transform.localPosition = new Vector3 (0, 0.5f, 0);
		ColorMesh ();
		Reset ();
	}

	public bool isValid() {
		return count == 0;
	}

	private void Reset() {
		count = 0;
	}

	public void Select(GameObject go) {
		ghost = go;
		Bounds b = new Bounds (Vector3.zero, Vector3.zero);

		foreach (Renderer c in go.GetComponentsInChildren<Renderer>()) {
			b.Encapsulate (new Bounds(c.transform.localPosition, c.bounds.extents*2));
		}
		mesh.transform.localScale = b.extents*2;
		mesh.transform.localPosition = new Vector3(0, mesh.transform.localScale.y / 2, 0);
		ColorMesh ();
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter (Collider other) {
		if (!other.CompareTag ("Terrain")) {
			count++;
			ColorMesh ();
		}
	}


	void OnTriggerExit(Collider other) {
		if (!other.CompareTag ("Terrain")) {
			count--;
			ColorMesh ();
		}
	}

	private void ColorMesh() {
		if (ghost == null) {
			mesh.enabled = false;
		} else {
			mesh.enabled = true;
			if (count == 0) {
				mesh.material.color = Color.green;
			} else {
				mesh.material.color = Color.red;
			}
		}
	}
}

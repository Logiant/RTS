using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_DEBUG_BuildUnit : MonoBehaviour {

    public Player human;
    public GameObject Churl;

    Text text;

    int houses = 0;
    int units = 0;

    void Start() {
        text = GetComponentInChildren<Text>();
    }

    void Update() {
        units = human.getUnits().Count;
        houses = human.getNumHouses();
        text.text =units + "/" + houses + "\nHumans\n5 Food Each";
    }

    public void Spawn() {
        //if we have enough food and houses
        if (units < houses && human.bp.res.corn >= 5) {
            human.bp.Remove(new Backpack.Resources(5, 0, 0, 0));
            //spawn a churl @ one of the houses
            GameObject go = Instantiate(Churl) as GameObject;
            go.GetComponent<WorldObject>().player = human;

        }
    }
}

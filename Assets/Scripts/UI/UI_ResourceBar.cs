using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using RTS;

public class UI_ResourceBar : MonoBehaviour {

    //image boxes for up/down
    public Image foodRate;
    public Image woodRate;
    public Image metalRate;
    public Image toolRate;
    //sprites for up, down, same
    public Sprite up;
    public Sprite down;
    public Sprite neutral;
    //timer for sampling
    float countdownTimer = 30f;
    float timer = 0f;
    int avgSamples = 25;
    float thresh = 0.2f;

    Text text;
    Backpack.Resources res;
    //rate lists
    List<float> food;
    List<float> wood;
    List<float> metal;
    List<float> tools;

    // Use this for initialization
    void Start () {
		text = GetComponentInChildren<Text> ();
        //rate lists
        food = new List<float>();
        wood = new List<float>();
        metal = new List<float>();
        tools = new List<float>();

        foodRate.sprite = neutral;
        woodRate.sprite = neutral;
        metalRate.sprite = neutral;
        toolRate.sprite = neutral;

    }

    // Update is called once per frame
    void Update () {
        text.text = "";
        res = GameState.player.bp.res;

        text.text += GameState.player.getUnits().Count + "/" + GameState.player.getCapacity() + " humans\n";

        text.text += "Food: " + res.corn + "\n";
        text.text += "Wood: " + res.wood + "\n";
        text.text += "Metal: " + res.metal + "\n";
        text.text += "Demand: " + GameState.player.craft.TotalDemand() + "\n";

        text.text += "Idle: " + GameState.player.nothing.actors.Count;

        //check the rate of resource change
        timer--;
        if (timer <= 0) {
            timer += countdownTimer;
            CheckRate();
        }
    }

    void CheckRate() {
        food.Add(res.corn);
        wood.Add(res.wood);
        metal.Add(res.metal);
        tools.Add(GameState.player.ist.GetQty(Tool.TOOLTYPES.HAMMER));
        //take a moving avg
        if (food.Count > avgSamples) {
            food.RemoveAt(0);
            wood.RemoveAt(0);
            metal.RemoveAt(0);
            tools.RemoveAt(0);
        }

        float foodSum = 0;
        float woodSum = 0;
        float metalSum = 0;
        float toolsSum = 0;

        for (int i = 0; i < food.Count; i++) {
            foodSum += food[i];
            woodSum += wood[i];
            metalSum += metal[i];
            toolsSum += tools[i];
        }

        foodSum /= food.Count;
        woodSum /= wood.Count;
        metalSum /= metal.Count;
        toolsSum /= tools.Count;
        //set up/down images
        //food
        if (food[food.Count-1] - foodSum > thresh) {
            foodRate.sprite = up;
        } else if (food[food.Count - 1] - foodSum < -thresh) {
            foodRate.sprite = down;
        } else {
            foodRate.sprite = neutral;
        }
        //wood
        if (wood[wood.Count - 1] - woodSum > thresh) {
            woodRate.sprite = up;
        } else if (wood[wood.Count - 1] - woodSum < -thresh) {
            woodRate.sprite = down;
        } else {
            woodRate.sprite = neutral;
        }
        //metal
        if (metal[metal.Count - 1] - metalSum > thresh) {
            metalRate.sprite = up;
        } else if (metal[metal.Count - 1] - metalSum < -thresh) {
            metalRate.sprite = down;
        } else {
            metalRate.sprite = neutral;
        }
        //tools
        if (tools[tools.Count - 1] - toolsSum > thresh) {
            toolRate.sprite = up;
        } else if (tools[tools.Count - 1] - toolsSum < -thresh) {
            toolRate.sprite = down;
        } else {
            toolRate.sprite = neutral;
        }


    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Mouseover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    UI_Base parent;

    void Start() {
        parent = GetComponentInParent<UI_Base>();
    }

    public void OnPointerEnter(PointerEventData eventData) {
        //do stuff
        parent.Mouseover(this.gameObject);
    }

    public void OnPointerExit(PointerEventData eventData) {
        //do stuff
        parent.MouseExit(this.gameObject);
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class Drag : MonoBehaviour, IDragHandler
{


    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;

    }

}

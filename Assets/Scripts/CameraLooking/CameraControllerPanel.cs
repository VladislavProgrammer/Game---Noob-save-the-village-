using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; 

public class CameraControllerPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler 
{
    public bool pressed = false;
    public int fingerId;

    public void OnPointerDown(PointerEventData eventData)   {
        if(eventData.pointerCurrentRaycast.gameObject == gameObject){
            pressed = true;
            fingerId = Input.GetTouch(0).fingerId;
        }
    }

    public void OnPointerUp(PointerEventData eventData) {
        pressed = false;
        fingerId =0;
    }  

}

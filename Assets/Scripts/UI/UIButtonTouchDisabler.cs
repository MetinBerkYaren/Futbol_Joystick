using System.Collections;
using System.Collections.Generic;
using CappTools;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonTouchDisabler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        TouchManager.Instance.isActive = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        TouchManager.Instance.isActive = true;
    }
     
}

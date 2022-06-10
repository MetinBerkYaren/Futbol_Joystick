using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using CappTools;
using System;

public class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler {

    public Image ImgBg;// background image
    public Image ImgJoystick;//foreground image

    private Vector3 _inputVector;// click position
    public Vector3 InputVector {
        get {
            return _inputVector;
        }
    }

     void Start()
    {
        TouchManager.Instance.onTouchBegan += OnTouchBegan;
    }

    private void OnTouchBegan(TouchInput touch)
    {
        
    }

    public void OnPointerDown(PointerEventData e)// function when click down
    
     {

        
        OnDrag(e);
        
    }

    public void OnDrag(PointerEventData e) // function when drag
    
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(ImgBg.rectTransform,
                                                                    e.position,
                                                                    e.pressEventCamera,
                                                                    out pos)) {

            pos.x = (pos.x / ImgBg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / ImgBg.rectTransform.sizeDelta.y);

            _inputVector = new Vector3(pos.x * 2 , 0, pos.y * 2 );
            _inputVector = (_inputVector.magnitude > 1.0f) ? _inputVector.normalized : _inputVector;

            ImgJoystick.rectTransform.anchoredPosition = new Vector3(_inputVector.x * (ImgBg.rectTransform.sizeDelta.x * .4f),
                                                                     _inputVector.z * (ImgBg.rectTransform.sizeDelta.y * .4f));
        }

         
    }   

    public void OnPointerUp(PointerEventData e)// function when click up
    
    
     {
   
        _inputVector = Vector3.zero;
        ImgJoystick.rectTransform.anchoredPosition = Vector3.zero;
    }


    public float Horizontal()// function get the Horizontal position
    
     {
        if (_inputVector.x != 0) {
            return _inputVector.x;
        }

        return Input.GetAxis("Horizontal");
    }

    public float Vertical() // function get the Horizontal Vertical
    {
        if (_inputVector.z != 0) {
            return _inputVector.z;
        }

        return Input.GetAxis("Vertical");
    }

}
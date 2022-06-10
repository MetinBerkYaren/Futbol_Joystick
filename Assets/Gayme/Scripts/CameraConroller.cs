using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConroller : MonoBehaviour
{


    public Transform Ball;// the ball transform


 
    // Start is called before the first frame update
    void Start()
    {
      
       transform.position= new Vector3 (-5f,25f,-25f);
       transform.eulerAngles = new Vector3(40, 0, 0);


    }

    // Update is called once per frame
    void FixedUpdate()
    {

          transform.position = Vector3.Lerp(transform.position,new Vector3(Ball.position.x,transform.position.y,transform.position.z), 1f);// folow the ball 
        
    }


    public void Change_Position_Rotation()// function change direction after the mittan
    {

       transform.position= new Vector3 (-5f,25f,45f);



       transform.eulerAngles = new Vector3(40, 180, 0);

    }



}

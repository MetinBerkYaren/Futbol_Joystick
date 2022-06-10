using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Humain : MonoBehaviour {
   
    public Rigidbody Controller;// Rigidbody of the player 
   
  
    private float Drag = 0.5f;// the drag speed of the player 
    private float TerminalRotationSpeed = 25.0f;// the speed of rotation

    public int My_Number_In_My_Equipe;// the specific number of the player in his team A

    public Ball BallController;// class of the ball controller

    public Transform Ball;// the ball transform
    private Vector3 Start_Position;// the start position of the player

 

    private Vector3 My_Position_compared_Ball;// the position of the player compared will the position of the ball


    private bool Follow_Ball1=true;// condition of follow the ball when the player no ball
    private bool Follow_Ball2=false;// condition of follow the ball when the player no ball

    public PlayerController PlayerContrl;// class of the player controller

    public AnimationClip run_no_ball,stop;// animation of run and stop


    public  Animation anim;// the source of the animation



    


	void Start () {
        Controller.maxAngularVelocity = TerminalRotationSpeed;
        Controller.drag = Drag;

        Start_Position=transform.position;// get the start position


          anim.clip = stop;
            anim.Play(); 

  
    }
	public void Rest_Player()// function of rest the player after a new goal or start new mittan
    {
      transform.position=Start_Position;
        Follow_Ball1=true;
     Follow_Ball2=false;  


     transform.eulerAngles = new Vector3(0, 90, 0);// the player face the target of the computer 
      anim.clip = stop;
            anim.Play(); 
    }


     void Update ()
     
      {

  


if(BallController.NumberPLayer+1!=My_Number_In_My_Equipe)// when the player has no ball
{
    
    Auto_Movement_We_Have_Ball();// auto movement of the player 

     if(Follow_Ball1)
    {
 Follow_Ball1 =false;

  StartCoroutine(trans1());// enable the player to auto movement 
 
    }
}

else if(BallController.NumberPLayer+1==My_Number_In_My_Equipe)// when the player has the ball
{
   
 Follow_Ball2 =false;// desable the player  auto movement 
 Follow_Ball1=true;// desable the player  auto movement 

  
}




     } 

      
	public void Auto_Movement_We_Have_Ball()// function of auto movement of the player 
    {

   if(Follow_Ball2 )

   {
      

        transform.LookAt(new Vector3(Ball.position.x, transform.position.y, Ball.position.z));// the player face the ball
        

       if(Ball.position.x+My_Position_compared_Ball.x>-50.0f && Ball.position.x+My_Position_compared_Ball.x<45 )// when the player he is in the stade 
       {
          
 float dist = Vector3.Distance(transform.position, new Vector3(Ball.position.x+My_Position_compared_Ball.x,transform.position.y,Start_Position.z));// claculate the distance of the player compared with the ball
if(dist>5)// when the distance >5
{

anim.clip = run_no_ball;



if((Ball.position.x+My_Position_compared_Ball.x) >transform.position.x)// if the player move back
{
 anim["Take 12"].speed = -1f;// animation of move back
}
else if((Ball.position.x+My_Position_compared_Ball.x) <transform.position.x)// if the player move forward
{
 anim["Take 12"].speed = 1f;// animationof move forward
}

anim.Play(); 

  transform.position = Vector3.MoveTowards(transform.position,new Vector3(Ball.position.x+My_Position_compared_Ball.x,transform.position.y,Start_Position.z), 0.1f);// the player auto movement 

}
else if(dist<3)// else 
{
anim.clip = stop;// stop the player
anim.Play(); 
}


       }
       else// when the player out of the stade
       {
           
      transform.position = Vector3.MoveTowards(transform.position,new Vector3(transform.position.x,transform.position.y,Start_Position.z), 0.1f);//reset his position
       

anim.clip = stop;// staop the player



anim.Play(); 

       }

   }
  





    }

    
   public void Get_Position()// function of getting the position of the player compared with the ball
{
    
My_Position_compared_Ball=transform.position-Ball.position;
}

   

     public IEnumerator trans1()
    {
     
   if (anim.IsPlaying("Take 20"))// when the player he is running 
      {
       anim.clip = stop;// stop the player
anim.Play(); 
      }

        yield return new WaitForSeconds(2f);
        Get_Position();
        Follow_Ball2=true;

        
        
    }
	


    
    		private void OnTriggerEnter(Collider  other)// the player hitt obejcts
	
	
	 {
	     if(other.gameObject.tag=="Bar1"||other.gameObject.tag=="Bar2"||other.gameObject.tag=="Bar3"||other.gameObject.tag=="Bar4" )// when the player hitt wall/ barr
   {

 

  PlayerContrl.Enable_Move=false;// desable his moving


     
   }
	}




    		private void OnTriggerExit(Collider  other)// when the player out of objects
	
	
	 {
	     if(other.gameObject.tag=="Bar1"||other.gameObject.tag=="Bar2"||other.gameObject.tag=="Bar3"||other.gameObject.tag=="Bar4" )// when the player out from wall/ barr
   {

 

  PlayerContrl.Enable_Move=true;// enable his moving


     
   }
	}

}
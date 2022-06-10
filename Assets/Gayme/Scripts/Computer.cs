using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
     public Rigidbody Controller;// Rigidbody of the computer  
   
  
    private float Drag = 0.5f;// the drag speed of the computer 
    private float TerminalRotationSpeed = 25.0f;// the speed of rotation

    public int My_Number_In_My_Equipe;// the specific number of the Computer in his team B

    public Ball BallController;// class of the ball controller

    public Transform Ball;// the ball transform

 

    private Vector3 My_Position_compared_Ball;// the position of the computer compared will the position of the ball


    private bool Follow_Ball1=true;// condition of follow the ball when the computer no ball
    private bool Follow_Ball2=false;// condition of follow the ball when the computer no ball
     private Vector3 Start_Position;// the start position of the computer

     public AnimationClip run_no_ball,stop;// animation of run and stop


    public  Animation anim;// the source of the animation



    


	void Start () {
        Controller.maxAngularVelocity = TerminalRotationSpeed;
        Controller.drag = Drag;
        
        Start_Position=transform.position;

             anim.clip = stop;
            anim.Play(); 

  
    }
	public void Rest_Computer()// function of rest the computer after a new goal or start new mittan
    {
     transform.position=Start_Position;

       Follow_Ball1=true;
     Follow_Ball2=false; 

     
     transform.eulerAngles = new Vector3(0, -90, 0);// the computer face the target of the computer 

          anim.clip = stop;
            anim.Play(); 
    }


     void Update ()
     
      {
        

if(BallController.NumberComputer+1!=My_Number_In_My_Equipe)// when the computer has no ball
{
    
    Auto_Movement_We_Have_Ball();// auto movement of the computer 

     if(Follow_Ball1)
    {
 Follow_Ball1 =false;

  StartCoroutine(trans1());// enable the player to auto computer 
 
    }
}

else if(BallController.NumberComputer+1==My_Number_In_My_Equipe)// when the computer has the ball
{
   
 Follow_Ball2 =false;// desable the computer  auto movement 
 Follow_Ball1=true;// desable the computer  auto movement 

  
}




     } 

      
	public void Auto_Movement_We_Have_Ball()// function of auto movement of the computer 
    {
///  seam comment of the class (HUMAIN) /////
   if(Follow_Ball2 )

   {
      

        transform.LookAt(new Vector3(Ball.position.x, transform.position.y, Ball.position.z));
        

       if(Ball.position.x+My_Position_compared_Ball.x>-50.0f && Ball.position.x+My_Position_compared_Ball.x<40 )
       {
          
 float dist = Vector3.Distance(transform.position, new Vector3(Ball.position.x+My_Position_compared_Ball.x,transform.position.y,Start_Position.z));
if(dist>5)
{
    anim.clip = run_no_ball;



if((Ball.position.x+My_Position_compared_Ball.x) <transform.position.x)
{
anim["Take 12"].speed = -1f;
}
else if((Ball.position.x+My_Position_compared_Ball.x) >transform.position.x)
{
 anim["Take 12"].speed = 1f;
}

anim.Play(); 
  transform.position = Vector3.MoveTowards(transform.position,new Vector3(Ball.position.x+My_Position_compared_Ball.x,transform.position.y,Start_Position.z), 0.1f);

}
else  if(dist<3)
{
anim.clip = stop;
anim.Play(); 
}




       }
       else
       {
           
      transform.position = Vector3.MoveTowards(transform.position,new Vector3(transform.position.x,transform.position.y,Start_Position.z), 0.1f);
       

       
anim.clip = stop;



anim.Play(); 

       }

   }
  





    }
  
   public void Get_Position()
{
    
My_Position_compared_Ball=transform.position-Ball.position;
}

   

     public IEnumerator trans1()
    {

          if (anim.IsPlaying("Take 20"))
      {
       anim.clip = stop;
anim.Play(); 
      }
      
        yield return new WaitForSeconds(2f);
        Get_Position();
        Follow_Ball2=true;

        
        
    }
	


}
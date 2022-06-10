using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerController : MonoBehaviour
{
  
public GameObject[] Players =new GameObject[5] ;// the 5 Computers of team B 
private float MoveSpeed =0.15f;// move speed 

private int Random_Player;// random number of Computer

public Ball BallController;// class of the ball controller
public GameObject Cone;// the cone curson of the top of the Computer
public Transform Ball1;// ball transform
public Transform Cage;// the target of the team A / Players

public bool is_shoot=false;// condition of can shoot  the ball 

public int Number_Pass=0;// the number of the pass  ==> for the statistic of the match
public int Number_Shoot=0;// the number of the shoot ==> for the statistic of the match


 public AnimationClip shoot_ball,pass_ball,run_no_ball,stop;// animation clip







	void FixedUpdate () {



if(!BallController.Have_Ball_Computer&&BallController.Have_Ball_Player)// when the computer has no ball and the player too
{
 Get_Near_Player_To_Ball(Ball1);// check how is the player the most near to the ball

  Players[BallController.NumberComputer].transform.LookAt(new Vector3(Ball1.position.x, Players[BallController.NumberComputer].transform.position.y, Ball1.position.z));
Players[BallController.NumberComputer].transform.position = Vector3.MoveTowards(Players[BallController.NumberComputer].transform.position,new Vector3(Ball1.position.x,Players[BallController.NumberComputer].transform.position.y,Ball1.position.z), MoveSpeed);


     Players[BallController.NumberComputer].GetComponent<Computer>().anim.clip = run_no_ball;
  
             Players[BallController.NumberComputer].GetComponent<Computer>().anim.Play();    
            
} 
else if(!BallController.Have_Ball_Computer&&!BallController.Have_Ball_Player && !is_shoot)// when the computer not shooting the ball and no ball to Team A and B
{
 Players[BallController.NumberComputer].GetComponent<Computer>().anim.clip = stop;
  
             Players[BallController.NumberComputer].GetComponent<Computer>().anim.Play();    
}
else if(BallController.Have_Ball_Computer&&!BallController.Have_Ball_Player)// When the Commputer have the ball
{

if( Players[BallController.NumberComputer].transform.position.x>-35.0f)// when the computer in this positon 
{
    Give_Pass();// give pass to the computer how near to the ball compared to his position
}
else  if( Players[BallController.NumberComputer].transform.position.x<-35.0f)// when the computer in this poistion he will shoot the ball
{
Shoot();
}

else
{
 Move_To_Target();
             
}


}           




  


   
           
        
     
         
        }
public void Give_Pass()// pass the ball to other player 
{
      
if(BallController.Have_Ball_Computer)
{




  Random_Player= Check_NearPlayer(BallController.NumberComputer);

if(Random_Player==-1)
{

Move_To_Target();

}
else
{

  
     Players[BallController.NumberComputer].GetComponent<Computer>().anim.clip = pass_ball;
  
             Players[BallController.NumberComputer].GetComponent<Computer>().anim.Play();  

BallController.Pass_Ball(Players[BallController.NumberComputer].transform,Players[Random_Player].transform);

UpdateCursor(Random_Player);

Number_Pass+=1;

}

 


     
}
   
}

 public void Move_To_Target()// function to move the computer to the target of the team A
 {

if(!is_shoot)
{
 Players[BallController.NumberComputer].GetComponent<Computer>().anim.clip = run_no_ball;
  
             Players[BallController.NumberComputer].GetComponent<Computer>().anim.Play(); 
}
       

Players[BallController.NumberComputer].transform.LookAt(new Vector3(Cage.position.x, Players[BallController.NumberComputer].transform.position.y, Cage.position.z));
Players[BallController.NumberComputer].transform.position = Vector3.MoveTowards(Players[BallController.NumberComputer].transform.position,new Vector3(Cage.position.x,Players[BallController.NumberComputer].transform.position.y,Cage.position.z), MoveSpeed);
 }


public int Check_NearPlayer(int Current_Player)// function to check HOW the near Computer to the target of the team A // else this fuction return -1
{

  
  int Near_Player=0;    
  float[] Tab =new float[5];

      for (int i = 0; i < 5; i++)
 {


 float dist = Vector3.Distance(Cage.position, Players[i].transform.position);

 Tab[i]=dist;
            


}

float Distance=Vector3.Distance(Cage.position, Players[Current_Player].transform.position);
      for (int a = 0; a < 5; a++)
 {



if(Tab[a]<Distance && Current_Player != a)
{
Near_Player=a;
return a;
}



}

return -1;    

}
public void Shoot()// function of shoot the ball 
{
      if(BallController.Have_Ball_Computer)
{
 StartCoroutine(trans1());

  Players[BallController.NumberComputer].transform.LookAt(new Vector3(Cage.position.x, Players[BallController.NumberComputer].transform.position.y, Cage.position.z));

   Players[BallController.NumberComputer].GetComponent<Computer>().anim.clip = shoot_ball;
  
             Players[BallController.NumberComputer].GetComponent<Computer>().anim.Play(); 
   BallController.Shoot(Players[BallController.NumberComputer]);
   Number_Shoot+=1;
}
}

  public IEnumerator trans1()
    {
      
is_shoot=true;
        yield return new WaitForSeconds(2f);
        
is_shoot=false;
        
        
    }

public void UpdateCursor(int PlayerID)// function To update the cone of the cursor on the top of the computer
{
      
      Cone.transform.parent=null;
      Cone.transform.position=new Vector3(Players[PlayerID].transform.position.x,Cone.transform.position.y,Players[PlayerID].transform.position.z );
      Cone.transform.parent =Players[PlayerID].transform;
      BallController.NumberComputer=PlayerID;
}

public void  Get_Near_Player_To_Ball(Transform Ball_Move)// function get the near computer to the ball
{
  float Distance=1000;
  int Near_Player=0;    
  float[] Tab =new float[5];

      for (int i = 0; i < 5; i++)
 {



 float dist = Vector3.Distance(Ball_Move.position, Players[i].transform.position);

 Tab[i]=dist;
            


}

      for (int a = 0; a < 5; a++)
 {



if(Tab[a]<Distance)
{
Near_Player=a;
Distance=Tab[a];
}


            
 

}
UpdateCursor(Near_Player);

}

}

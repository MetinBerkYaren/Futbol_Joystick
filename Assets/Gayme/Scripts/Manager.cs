using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{



    public Text ButTeam1; // text of goal TEAM A
    public Text ButTeam2; // text of goal TEAM B

    public Text Time_Match; // text of Timer Match

    private int Goal_Team1=0;// number of goal TEAM A
     private int Goal_Team2=0;// number of goal TEAM B

     private float timer = 0.0f;// Timer

     private int first2DecimalPlaces;// the float number of the timer we convert them to int

     public GameObject[] Players1 =new GameObject[5] ;// the player of the TEAM A
     public GameObject[] Players2 =new GameObject[5] ;// the player of the TEAM B

    public Ball BallController;// class of the ball controller

    public GameObject PlayerController;// the PlayerController object
    public GameObject ComputerController;// the ComputerController object

    private bool Stop_Match=false;// check the match end
    private bool Stop_Mittan=false;// check the mittan end


    public CameraConroller Camera;// class of  camera controller 


    public GoalPlayer Goal_Player_Team1;// the Goal Player of TEAM A

     public GoalPlayer Goal_Player_Team2;// the Goal Player of TEAM B

public Texture[] Stades = new Texture[4];// the texture of the 4 Stades
     
       public Renderer Stade_Material;// material of the stade

       public GameObject Match_Caracteristique;// panel of the match statistic

       public GameObject Match_Button;// the buttons of the game play

       public GameObject Butte_Animation;// the goal animation

       public GameObject Button_Inpause;// button of play pause

       public GameObject Button_Sound_On,Button_Sound_Off;// button on /off sound



//////// static object//////////////
public Text But1,But2;// goal text team A/B
    public Text  Pass1,Pass2;// pass text TEAM A/B

      public Text  Shoot1,Shoot2;// shoot text Team A/B

       public Text  Goal1,Goal2;// parad goal player Team A/B

       public Text  Time1,Time2;// Timer text minute /seconds
       public Text  Number_Mittan;// number of the mittan

//// end ///////////////////////////////


        public  AudioSource Start_Match_S;// audio start match

        public  AudioSource End_Match_S;// audio end the match


        
        public  AudioSource Pause_S;// audio clip when the panel pause show
        public  AudioSource Match_S;// audio clip of the match play
    
    // Start is called before the first frame update
    void Start()
    {

Start_Match_S.Play();

Match_S.Play();
Update_Stade();// update the texture material of the stade
        Update_Score();// initial the score 

Set_Positon_Player_Center_Pass(1);// give the ball the TEam A
        


    }

    private void Update_Stade()// function  of update stade texture material
    {
int i =PlayerPrefs.GetInt ("Stade");
Stade_Material.material.mainTexture = Stades[i];
    }




private  void Set_Positon_Player_Center_Pass(int Number_Team)// function to set all player position to start new game 
{
if (Number_Team==2)
{
    
    Players2[0].transform.position=new Vector3(-3.87f,Players2[0].transform.position.y,8.62f);
    Players2[1].transform.position=new Vector3(-4.99f,Players2[1].transform.position.y,16.5f);


    Players1[0].transform.position=new Vector3(-11.21f,Players1[0].transform.position.y,4.42f);
    Players1[1].transform.position=new Vector3(-11.94f,Players1[1].transform.position.y,13.64f);

}
else
{
    Players2[0].transform.position=new Vector3(-0.2f,Players2[0].transform.position.y,4.4f);
    Players2[1].transform.position=new Vector3(-0.16f,Players2[1].transform.position.y,14.35f);


    Players1[0].transform.position=new Vector3(-7.84f,Players1[0].transform.position.y,8.83f);
    Players1[1].transform.position=new Vector3(-6.46f,Players1[1].transform.position.y,16.2f);

    

   // BallController.Pass_Ball(Players1[0].transform,Players1[1].transform);

  
}
}


private void Start_Game_After_Goal_Mittan()// function to set the Tow  player position  after mittan or goal to play center pass 
{
    
    
 BallController.Rest_Ball();

     for (int i = 0; i < 5; i++)
    {

        Players1[i].GetComponent<Humain>().Rest_Player();
        Players2[i].GetComponent<Computer>().Rest_Computer();
      

    }

}
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime/2;
         first2DecimalPlaces = (int)(((decimal)timer % 1) * 100);
        Time_Match.text=((int)timer).ToString()+":"+first2DecimalPlaces.ToString();

      if(timer>45 && timer <90 && !Stop_Mittan)
      {
       Stop_Mittan=true;
       Start_Game_After_Goal_Mittan();
       Set_Positon_Player_Center_Pass(2);
       PlayerController.GetComponent<PlayerController>().Direction_Game=-1;

  

        Camera.Change_Position_Rotation();

End_Mittan();

End_Match_S.Play();


      }
      else if(timer >90  && !Stop_Match)
      {
          Stop_Match=true;


          PlayerController.SetActive(false);

          ComputerController.SetActive(false);
          End_Match();
          End_Match_S.Play();

      }



    }

private void End_Mittan()// function after the end the mittan
{
Pause();
}    
private void End_Match()// function after the end the match
{

    
    Update_Caracteristique();
Match_Button.SetActive(false);
    Match_Caracteristique.SetActive(true);

   
     Time.timeScale = 0;
}

 public void   AddGoal_Team1()// function to Add Goal score to Team A
    {
Goal_Team1+=1;
Update_Score();

Goal_Player_Team2.After_Goal();

StartCoroutine(trans3(2));
    
    }

     public void   AddGoal_Team2()// function to Add Goal score to Team B
    {
Goal_Team2+=1;
Update_Score();
Goal_Player_Team1.After_Goal();

StartCoroutine(trans3(1));
   
    }


    private void Update_Score() // function to Update the Score
    {
          ButTeam1.text =Goal_Team1.ToString();
           ButTeam2.text =Goal_Team2.ToString();
    }



    
       public IEnumerator trans3(int team)
    {
PlayerController.GetComponent<PlayerController>().is_shoot=false;
ComputerController.GetComponent<ComputerController>().is_shoot=false;
  PlayerController.SetActive(false);

ComputerController.SetActive(false);

Butte_Animation.SetActive(true);


        yield return new WaitForSeconds(3f);

  PlayerController.SetActive(true);

  Butte_Animation.SetActive(false);

       ComputerController.SetActive(true);
         Start_Game_After_Goal_Mittan();
    Set_Positon_Player_Center_Pass(team);

    }



    public void Home()// function when click on the button home
{
      Time.timeScale = 1;
SceneManager.LoadScene(0);
}

public void Pause()// function when click on the button pause
{
    
    Update_Caracteristique();
    Match_Button.SetActive(false);
    Match_Caracteristique.SetActive(true);

    Button_Inpause.SetActive(true);
     Time.timeScale = 0;
}

public void IN_Pause()// function when click on the button play
{

     Time.timeScale = 1;
    Match_Button.SetActive(true);
    Match_Caracteristique.SetActive(false);

    Button_Inpause.SetActive(false);
Pause_S.Stop();
 Match_S.Play();
    
}

public void Sound()// function when click on the button sound
{
if(AudioListener.volume==1f)
{
Button_Sound_On.SetActive(false);
Button_Sound_Off.SetActive(true);
    AudioListener.volume = 0f;
}
else
{
Button_Sound_On.SetActive(true);

Button_Sound_Off.SetActive(false);
    AudioListener.volume = 1f;
}
    
}


private void Update_Caracteristique()// function to update the statistic data
{


But1.text =Goal_Team1.ToString();
But2.text =Goal_Team2.ToString();


Time1.text=((int)timer).ToString();
Time2.text=first2DecimalPlaces.ToString();
Goal1.text  =Goal_Player_Team1.Number_Parad.ToString();

Goal2.text  =Goal_Player_Team2.Number_Parad.ToString();



Pass1.text=   PlayerController.GetComponent<PlayerController>().Number_Pass.ToString();
Pass2.text=ComputerController.GetComponent<ComputerController>().Number_Pass.ToString();


Shoot1.text=PlayerController.GetComponent<PlayerController>().Number_Shoot.ToString();
Shoot2.text=ComputerController.GetComponent<ComputerController>().Number_Shoot.ToString();
if(timer<45f)
{
Number_Mittan.text="1";
}
else
{
  Number_Mittan.text="2";  
}


 Pause_S.Play();
 Match_S.Stop();
}
}

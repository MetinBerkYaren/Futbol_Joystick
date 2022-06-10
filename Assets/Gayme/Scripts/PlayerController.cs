using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CappTools;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    //public VirtualJoystick VirtualJoystick;//class of VirtualJoystick
    public VariableJoystick VariableJoystick;
    public GameObject[] Players = new GameObject[5];// the 5 player of team A 
    public float MoveSpeed;// move speed 

    private int Random_Player;// random number of player

    public Ball BallController;// class of the ball controller
    public GameObject Cone;// the cone curson of the top of the player
    public bool Enable_Move = true;// condition of can move the players 
    public int Direction_Game = 1;// the direction of movement after revers the stade after mittan

    public bool is_shoot = false;// condition of can shoot  the ball 

    public int Number_Pass = 0;// the number of the pass  ==> for the statistic of the match
    public int Number_Shoot = 0;// the number of the shoot ==> for the statistic of the match


    public AnimationClip shoot_ball, pass_ball, run_no_ball, stop;// animation clip

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void Start()
    {
        TouchManager.Instance.onTouchEnded += OnTouchEnded;
        TouchManager.Instance.onTouchMoved += OnTouchMoved;
    }

    private void OnTouchMoved(TouchInput touch)
    {
        joystickvalue = VariableJoystick.Direction.x;
    }

    public void OnTouchEnded (TouchInput Touch)
    {
        
        
        DoWhenTouchEnd();
        
    }


    public Vector3 kip_ball(float translationZ, float translationX)// function of control movement players
    {


        translationZ *= Time.deltaTime;
        translationX *= Time.deltaTime;



        Vector3 movement = new Vector3(Direction_Game * translationX, 0.0f, Direction_Game * translationZ);


        return (movement);
    }



    void FixedUpdate()
    {



        if (VariableJoystick.Direction != Vector2.zero)
        {
            Vector3 direction = new Vector3(VariableJoystick.Direction.x, 0, VariableJoystick.Direction.y);

            Players[BallController.NumberPLayer].transform.Translate(kip_ball(VariableJoystick.Direction.y * MoveSpeed, VariableJoystick.Direction.x * MoveSpeed) * Time.deltaTime * MoveSpeed, Space.World);

            //Vector3 lookDirection = new Vector3(direction.x, 0, direction.z);
            //Quaternion lookRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
            //Quaternion.Lerp(transform.rotation, lookRotation, 30 * Time.deltaTime);
            Players[BallController.NumberPLayer].transform.rotation = Quaternion.LookRotation(kip_ball(VariableJoystick.Direction.y * MoveSpeed, VariableJoystick.Direction.x * MoveSpeed));
        }

        if (!is_shoot)
        {
            Players[BallController.NumberPLayer].GetComponent<Humain>().anim.clip = run_no_ball;
        }

        Players[BallController.NumberPLayer].GetComponent<Humain>().anim.Play();

        if (VariableJoystick.Direction == Vector2.zero && !is_shoot)
        {
            Players[BallController.NumberPLayer].GetComponent<Humain>().anim.clip = stop;

            Players[BallController.NumberPLayer].GetComponent<Humain>().anim.Play();
        }





    }

    public bool triged;
    private float joystickvalue;
    public void DoWhenTouchEnd()
    {
        if(joystickvalue >= 0.8f && joystickvalue >= -0.8)
        {
            //Debug.Log(joystickvalue);
            Shoot();
        }
        else if(joystickvalue != 1f && joystickvalue != -1)
        {
            Give_Pass();
         
        }
       

     
    }
    
   
    public void Give_Pass()// pass the ball to other player
    {

        if (BallController.Have_Ball_Player)// when team A of the player has the ball 
        {




            Random_Player = Check_NearPlayer(BallController.NumberPLayer);// get the number of the near player 

            if (Random_Player == -1)
            {
                Random_Player = Random.Range(0, 5);

                while (Random_Player == BallController.NumberPLayer)
                {
                    Random_Player = Random.Range(0, 5);
                }

            }



            Players[BallController.NumberPLayer].GetComponent<Humain>().anim.clip = pass_ball;

            Players[BallController.NumberPLayer].GetComponent<Humain>().anim.Play();

            BallController.Pass_Ball(Players[BallController.NumberPLayer].transform, Players[Random_Player].transform);

            UpdateCursor(Random_Player);

            Number_Pass += 1;


        }

    }




    public int Check_NearPlayer(int Current_Player)// function to calculate the near player to other player
    {



        for (int i = 0; i < 5; i++)
        {
            float angle = 25;


            if (Current_Player != i)
            {
                if (Vector3.Angle(Players[Current_Player].transform.forward, Players[i].transform.position - Players[Current_Player].transform.position) < angle)

                {

                    return i;
                }

            }
        }




        return -1;

    }
    public void Shoot()// function of shoot the ball 
    {
        if (BallController.Have_Ball_Player)
        {

            StartCoroutine(trans1());

            Players[BallController.NumberPLayer].GetComponent<Humain>().anim.clip = shoot_ball;

            Players[BallController.NumberPLayer].GetComponent<Humain>().anim.Play();


            BallController.Shoot(Players[BallController.NumberPLayer]);

            Number_Shoot += 1;
        }
    }
    public IEnumerator trans1()
    {

        is_shoot = true;
        yield return new WaitForSeconds(2f);

        is_shoot = false;


    }
    public void UpdateCursor(int PlayerID)// function of update the cone of the cursor on the top of the player
    {

        Cone.transform.parent = null;
        Cone.transform.position = new Vector3(Players[PlayerID].transform.position.x, Cone.transform.position.y, Players[PlayerID].transform.position.z);
        Cone.transform.parent = Players[PlayerID].transform;
        BallController.NumberPLayer = PlayerID;
    }




    public void Get_Near_Player_To_Ball(Transform Ball_Move)// function get the near player to the ball
    {
        float Distance = 1000;
        int Near_Player = 0;
        float[] Tab = new float[5];

        for (int i = 0; i < 5; i++)
        {



            float dist = Vector3.Distance(Ball_Move.position, Players[i].transform.position);

            Tab[i] = dist;



        }

        for (int a = 0; a < 5; a++)
        {



            if (Tab[a] < Distance)
            {
                Near_Player = a;
                Distance = Tab[a];
            }





        }
        UpdateCursor(Near_Player);

    }






}

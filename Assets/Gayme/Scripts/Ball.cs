using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{


    public PlayerController PlayerContrl;// class player controller

    public ComputerController ComputerContrl;// class computer controller

    public Manager Manager_Game; // class manager

    public bool Have_Ball_Player = false;// detect team Player have the ball
    public bool Have_Ball_Computer = false;// dtetct team computer have the ball

    public int NumberPLayer = 0;// the number of the player have the ball
    public int NumberComputer = 0;// the number of the computer have the ball
    private Transform StartPosition;// the start position of the ball
    private Transform EndPosition;// the end of the position will get the ball after pass or shoot
    private bool Pass = false;// detct if the ball is passed by the players
    public float PowerShoot;// the power of the shoot the ball

    public float speed = 1.0F;// speed of the ball


    private float startTime;// timer start passing the ball


    private float journeyLength;// lenght of the distance of pass


    private Vector3 Start_Position_Ball;// the start position of the ball

    public Animator Anim_Ball;// anim of rotaion the ball



    public AudioSource Pass_Shoot;// audio of pass or shoot the ball

    public AudioSource Hitt_Barr;// audio of hit the ball wall




    void Start()
    {
       // NumberPLayer = 0;

        Start_Position_Ball = transform.position;// get the start position of the ball


    }

    public void Rest_Ball()// rest function will call after we get a new mittan or new goal 
    {
        transform.parent = null;// no parent folder to the ball
        NumberPLayer = 0;// initial player for the player 
        NumberComputer = 0;// initial player for the computer 
        Pass = false;// no pass
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;// freez the ball
        transform.position = Start_Position_Ball;// reset the position in the center of the ball
        ComputerContrl.UpdateCursor(NumberComputer);// update the cone cursor to the computer 
        PlayerContrl.UpdateCursor(NumberPLayer);// update the cone cursor to the player 
    }


    void Update()
    {

        if (Pass)// when the ball has passed 
        {

            float distCovered = (Time.time - startTime) * speed * 7;


            float fractionOfJourney = distCovered / journeyLength;

            transform.position = Vector3.Lerp(transform.position, EndPosition.position, fractionOfJourney);
        }




        if (!Have_Ball_Player && !Have_Ball_Computer)// when the ball no computer of player have it 
        {
            Anim_Ball.enabled = false;// no animation of rotation
        }
        else
        {
            Anim_Ball.enabled = true;//  animation of rotation
        }

      
    }



    private void OnTriggerEnter(Collider other)// when the ball hit objects


    {
        if (other.CompareTag("GoalArea"))
        {
            PlayerController.Instance.triged = true;
        }
        if (other.gameObject.tag == "Player")// when the ball hitt player humain
        {


            Take_Ball(other.gameObject);// the player will take the ball


           // NumberPLayer = (int)char.GetNumericValue(other.gameObject.name[other.gameObject.name.Length - 1]) - 1;// change the number of the player has ball

            Have_Ball_Player = true;// the player have the ball
            Have_Ball_Computer = false;// the computer don't have the ball
            Pass = false;// no in pass the ball

            PlayerContrl.UpdateCursor(NumberPLayer);// change the cursor of the player

            ComputerContrl.Get_Near_Player_To_Ball(gameObject.transform); // the coputer will change the cursor for how is near to the ball


        }
        else if (other.gameObject.tag == "Computer")// when the ball hitt computer 
        {
            Take_Ball(other.gameObject);// the computer will take the ball
            NumberComputer = (int)char.GetNumericValue(other.gameObject.name[other.gameObject.name.Length - 1]) - 1;// change the number of the computer he has the ball

            Have_Ball_Computer = true;// the computer he has the ball
            Have_Ball_Player = false;// the player don't have the ball
            Pass = false;

            ComputerContrl.UpdateCursor(NumberComputer);
            PlayerContrl.Get_Near_Player_To_Ball(gameObject.transform);

        }

        else if (other.gameObject.tag == "Goal")// when the ball hit the goal player 
        {
            Have_Ball_Player = false;
            Have_Ball_Computer = false;
            Take_Ball(other.gameObject);

        }
        else if (other.gameObject.tag == "Buttarget1")//when the ball hit the target of the team A (the player targert)
        {

            Manager_Game.AddGoal_Team2();// add new goal 
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }

        else if (other.gameObject.tag == "Buttarget2")//when the ball hit the target of the team B (the computer  targert)
        {

            Manager_Game.AddGoal_Team1();
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
        else if (other.gameObject.tag == "Bar1")// when  the ball hitt the barr of the stade
        {

            Hitt_Barr.Play();
            if (Have_Ball_Player)// when the player he has the ball
            {


                PlayerContrl.Enable_Move = false;// we stop his moving ==> to not going out of the stade unitil he out from this barr
            }
            else  /// else we send back the ball to the stade
            {

                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                Degage(3);// for the direction of the ball after hit the barr/wall
            }

        }

        else if (other.gameObject.tag == "Bar2")
        {
            Hitt_Barr.Play();
            if (Have_Ball_Player)
            {


                PlayerContrl.Enable_Move = false;
            }
            else
            {

                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                Degage(4);
            }

        }
        else if (other.gameObject.tag == "Bar3")
        {
            Hitt_Barr.Play();
            if (Have_Ball_Player)
            {


                PlayerContrl.Enable_Move = false;
            }
            else
            {

                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                Degage(5);
            }

        }
        else if (other.gameObject.tag == "Bar4")
        {
            Hitt_Barr.Play();
            if (Have_Ball_Player)
            {


                PlayerContrl.Enable_Move = false;
            }
            else
            {

                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                Degage(6);
            }

            

        }
    }



        private void OnTriggerExit(Collider other)// when the ball out from object


        {
        if (other.CompareTag("GoalArea"))
        {
            PlayerController.Instance.triged = false;
        }
           
            if (other.gameObject.tag == "Bar1" || other.gameObject.tag == "Bar2" || other.gameObject.tag == "Bar3" || other.gameObject.tag == "Bar4")// the ball out from the barr /wall
            {
                if (Have_Ball_Player)// if the player have the ball 
                {


                    PlayerContrl.Enable_Move = true;// we enable his moving back
                }


            }
        }


        public void Take_Ball(GameObject Object)// function of take the ball
        {

            transform.parent = null;// no parent folder to the ball

            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;



            transform.position = new Vector3(Object.transform.position.x, transform.position.y, Object.transform.position.z);// set the ball to other player
            transform.parent = Object.transform;// set the ball in the folder of the other player

            transform.localPosition = new Vector3(0, 0, 2);// set the position




        }

        public void Pass_Ball(Transform startMarker, Transform endMarker)// function of passing the ball
        {

        transform.parent = null;
            StartPosition = gameObject.transform;//initial the start position of ball to cross it 
            EndPosition = endMarker;// initial the end position of the ball the get it 
            Have_Ball_Player = false;// no player have the ball
            Have_Ball_Computer = false;// no computer have the ball

            

            startTime = Time.time;// initial the timer


            journeyLength = Vector3.Distance(StartPosition.position, EndPosition.position); // distance beteewn the start and the end of the positon 
            Pass = true;// the ball is in condition of passing

            Pass_Shoot.Play();// audio of pass

        }

        public void Shoot(GameObject Dirction)// function of shooting the ball
        {

       

        transform.parent = null;// no parent folder to the ball
            Have_Ball_Player = false;
            Have_Ball_Computer = false;



            gameObject.GetComponent<Rigidbody>().useGravity = true;
            var force = transform.position - Dirction.transform.position;// calculate the force

            force.Normalize();
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

            gameObject.GetComponent<Rigidbody>().AddForce(force * PowerShoot, ForceMode.Impulse);// shoot the ball



          // ComputerContrl.Get_Near_Player_To_Ball(gameObject.transform); // the computer will get the near computer to the ball
           PlayerContrl.Get_Near_Player_To_Ball(gameObject.transform); // the player will get the near player to the ball

            //Pass_Shoot.Play();//audio of shoot
        }




        public void Degage(int Dir)// function of degage the ball 
        {

            Vector3 direction;
            Quaternion rotation;
            transform.parent = null;
            Have_Ball_Player = false;
            Have_Ball_Computer = false;



            gameObject.GetComponent<Rigidbody>().useGravity = true;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;






            if (Dir == 1)
            {
                rotation = Quaternion.Euler(0, 0, 60);
                direction = rotation * Vector3.right;

            }
            else if (Dir == 2)
            {
                rotation = Quaternion.Euler(0, 0, -60);
                direction = rotation * Vector3.left;
            }

            else if (Dir == 3)
            {
                rotation = Quaternion.Euler(10, 0, 0);
                direction = rotation * Vector3.forward;
            }
            else if (Dir == 4)
            {
                rotation = Quaternion.Euler(-10, 0, 0);
                direction = rotation * Vector3.back;
            }
            else if (Dir == 5)
            {
                rotation = Quaternion.Euler(0, 0, 10);
                direction = rotation * Vector3.left;

            }
            else
            {
                rotation = Quaternion.Euler(0, 0, -10);
                direction = rotation * Vector3.right;
            }



            gameObject.GetComponent<Rigidbody>().AddForce(direction * 100);

            ComputerContrl.Get_Near_Player_To_Ball(gameObject.transform);
            PlayerContrl.Get_Near_Player_To_Ball(gameObject.transform);


            Pass_Shoot.Play();

        }
 }


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPlayer : MonoBehaviour
{

    public Transform Ball1;// the ball transform

    public Ball BallController;// class of the ball controller

    public int Team_Number;// the goal player reference the the Team A Or Team B

    public AnimationClip folow_ball, ball_center, ball_right_down, ball_left_down, ball_degage;// animation clip
    public Animation anim;// animation source

    public GameObject Fake_Ball;// fake ball will show in the had of the goal player when he degage the ball

    public GameObject Ball_anim;// the animation of the ball


    public int Number_Parad = 0;// the number of the parad of the goal ==> for the statistic of the match


    void Start()
    {
        anim.clip = folow_ball;
        anim.Play();
    }
    void Update()
    {

        if (Ball1.position.z > 3.0f && Ball1.position.z < 15.0f)// when the ball face the goal player 
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, Ball1.position.z), 0.02f);// follow the ball

        }


    }

    private void OnTriggerEnter(Collider other)// when the goal player hitt objects


    {
        if (other.gameObject.tag == "Ball")// whe the goal player hitt the ball
        {
            Number_Parad += 1;// add new parad to the statistic
            StartCoroutine(trans1(other.gameObject));// event of the animation and degage the ball




        }
    }


    public IEnumerator trans1(GameObject other)
    {

        Fake_Ball.SetActive(true);// show the fake ball
        anim.clip = ball_center;
        anim.Play();
        other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;


        Ball_anim.GetComponent<Renderer>().enabled = false;// hide the real ball




        yield return new WaitForSeconds(1f);


        anim.clip = ball_degage;
        anim.Play();
        yield return new WaitForSeconds(1f);
        Ball_anim.GetComponent<Renderer>().enabled = true;// show the real ball

        other.transform.position = new Vector3(other.transform.position.x, 4f, other.transform.position.z);







        BallController.Degage(Team_Number);



        Fake_Ball.SetActive(false);// hide the fake ball



        yield return new WaitForSeconds(1f);


        anim.clip = folow_ball;
        anim.Play();


    }



    public void After_Goal()// function after the new goal to this goal player 
    {
       // StartCoroutine(trans2());
    }


    public IEnumerator trans2()
    {

        if (transform.position.z > Ball1.position.z)// check the ball is in the left or right of the goal player 
        {
            anim.clip = ball_right_down;// set animation 
            anim.Play();
        }
        else
        {
            anim.clip = ball_left_down;
            anim.Play();
        }


        gameObject.GetComponent<Collider>().enabled = false;


        yield return new WaitForSeconds(3f);


        anim.clip = folow_ball;
        anim.Play();

        gameObject.GetComponent<Collider>().enabled = true;

    }
}

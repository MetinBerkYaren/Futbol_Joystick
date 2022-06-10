
//using UnityEngine;
//using UnityEngine.AI;

//public class OldJoystickMove : MonoBehaviour
//{

//    public VariableJoystick joystick;


//    private void Update()
//    {
//        Move();
//    }

//    public NavMeshAgent agent;

//    private Vector3 lastPosition;
//    private float blendTreeSpeed = 0;
//    public float AgentSpeed;
//    public Animator  anim /*run_no_ball, stop*/;
//    public Ball BallController;
//    private bool ar;
//    public float x = 1;
//    void Move()
//    {
//        if (joystick.Direction != Vector2.zero)
//        {
//            Vector3 direction = new Vector3(joystick.Direction.x, 0, joystick.Direction.y);

//            agent.Move(direction * AgentSpeed * Time.deltaTime);

//            Vector3 lookDirection = new Vector3(direction.x, 0, direction.z);
//            Quaternion lookRotation = Quaternion.LookRotation(lookDirection, Vector3.up);

//            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 30 * Time.deltaTime);

//            if (ar == true) return;
//            anim.SetTrigger("Run");
//            ar = true;
//        }
//        else
//        {
//            if (ar == false) return;
//            anim.SetTrigger("Stop");
//            ar = false;
//        }
       
           
            
       
      
        
            

        



//        blendTreeSpeed = Mathf.Lerp(blendTreeSpeed, Mathf.Clamp01((lastPosition - transform.position).magnitude * 10f), 10 * Time.deltaTime);

       

//        lastPosition = transform.position;
//    }
//}


using UnityEngine; 



public class LegoMergeController : MonoBehaviour
{
    //public GameObject ground;
    //public Collider TriggerCollider;
    //private void Start()
    //{
    //    Collider[] colls = GetComponents<Collider>();
    //    foreach (var item in colls)
    //    {
    //        if (item.isTrigger)
    //        {
    //            TriggerCollider = item;
    //            break;
    //        }
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        
    }
    //public void OnCollisionEnter(Collision collision)
    //{




    //    if(collision.gameObject.CompareTag("SmallGround"))
    //    {
    //        if (collision.gameObject.tag == "Untagged") return;
    //        collision.gameObject.tag = "Untagged";




    //        //if (collision.gameObject.CompareTag("Untagged")) return;

    //        //collision.gameObject.tag = "Untagged";

    //        Debug.Log(gameObject.tag);



    //            Instantiate(ground, transform.position, Quaternion.identity);




    //        Destroy(gameObject,0.2f);


    //    }


}



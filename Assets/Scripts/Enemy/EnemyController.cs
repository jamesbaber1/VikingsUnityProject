using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    //private NavMeshAgent agent;


    void Start()
    {
        //agent = gameObject.GetComponent<NavMeshAgent>();
    }



    void OnCollisionEnter(Collision collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "Player")
        {
            //agent.SetDestination(new Vector3(0,0,0));
            Debug.Log("Hit collider");
        }

    }
}
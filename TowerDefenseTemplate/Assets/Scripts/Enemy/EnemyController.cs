using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Linq;

public class EnemyController : MonoBehaviour
{
    
    public float attractionRadius = 7f;


    private GameObject chiefHouse;
    private GameObject[] Players;
    private GameObject NearestPlayer;
    private float playerDistance;
    private List<float> playerDistances = new List<float>();
    private NavMeshAgent agent;


    void Start()
    {
        // The enemies intial target is the chief house
        chiefHouse = GameObject.Find("chief_house_tier1");

        NearestPlayer = chiefHouse;
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.SetDestination(NearestPlayer.transform.position);

        //Get list of all players by tag
        Players = GameObject.FindGameObjectsWithTag("Player");

        //The Search frequency for new Players
        InvokeRepeating("findNearstPlayer", 2.0f, 0.5f);
    }




    void Update()
    {

        if (playerDistance < attractionRadius && NearestPlayer.transform.position != null)
        {
            agent.SetDestination(NearestPlayer.transform.position);
        }

        else
        {
            agent.SetDestination(chiefHouse.transform.position);
        }
    }


    private GameObject findNearstPlayer()
    {
        playerDistances.Clear();

        foreach (GameObject tmpNearestPlayer in Players)
        {
            playerDistance = Vector3.Distance(tmpNearestPlayer.transform.position, this.transform.position);
            playerDistances.Add(playerDistance);

            if(playerDistance <= playerDistances.Min())
            {
                NearestPlayer = tmpNearestPlayer;
                playerDistance = playerDistances.Min();
            }

        }
              

        return NearestPlayer;

    }


    //void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("Hit collider of " + collision.gameObject.name);

    //    //Check for a match with the specified name on any GameObject that collides with your GameObject
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        agent.SetDestination(collision.gameObject.transform.position);
    //        Debug.Log("Hit collider of " + collision.gameObject.name);
    //    }

    //}
}
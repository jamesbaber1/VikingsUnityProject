using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public float attractionRadius = 7f;
    public GameObject selectRing;
    public GameObject AnimationMesh;

    private Animator PlayerAnim;
    //public GameObject navPos;



    private float enemyDistance;
    private GameObject enemyPlayer;
    private List<float> enemyDistances = new List<float>();
    private GameObject NearestEnemy;
    private GameObject[] Enemys;



    private Camera cam;
    private NavMeshAgent agent;
    private bool playerSelected = false;
    private bool enemySelected = false;
    private GameObject selectedEnemy;

    private Vector3 speedTest = new Vector3(0.01f, 0.01f, 0.01f);


    void Start()
    {
        PlayerAnim = AnimationMesh.GetComponent<Animator>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>() as Camera;
        agent = gameObject.GetComponent<NavMeshAgent>();

        InvokeRepeating("updateSelectEnemyPos", 0.5f, 0.5f);

    }


    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider.gameObject.tag == "Player")
                {
                    playerSelected = true;
                    selectRing.SetActive(true);
                }

                else
                {
                    playerSelected = false;
                    selectRing.SetActive(false);
                    //navPos.SetActive(false);
                }
                
            }
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if(hit.collider.gameObject.tag == "Enemy")
                {
                    selectedEnemy = hit.collider.gameObject;
                    enemySelected = true;
                    //navPos.SetActive(false);
                    Debug.Log("Enemy is selected");


                }

                if (playerSelected == true && enemySelected == false)
                {
                    agent.SetDestination(hit.point);
                    enemySelected = false;
                    //navPos.transform.position = hit.point;
                    //navPos.transform.parent = null;
                    //navPos.SetActive(true);
                }

            }
        }

        if (Math.Abs(agent.velocity.x) > speedTest.x && Math.Abs(agent.velocity.y) > speedTest.y && Math.Abs(agent.velocity.z) > speedTest.z)
        {
            PlayerAnim.SetBool("Running", true);
            Debug.Log(agent.velocity);
        }
        if (Math.Abs(agent.velocity.x) < speedTest.x && Math.Abs(agent.velocity.y) < speedTest.y && Math.Abs(agent.velocity.z) < speedTest.z)
        {
            PlayerAnim.SetBool("Running", false);
            Debug.Log(agent.velocity);
        }


    }

    private void updateSelectEnemyPos()
    {
        if (enemySelected)
        {
            if (selectedEnemy != null)
            {
                agent.SetDestination(selectedEnemy.transform.position);
                enemySelected = false;
            }
            else
            {
                agent.SetDestination(transform.position);
                enemySelected = false;
            }

        }
    }


}

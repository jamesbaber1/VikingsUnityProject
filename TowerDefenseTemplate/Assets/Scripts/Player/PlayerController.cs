using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public GameObject selectRing;
    public GameObject navPos;

    private Camera cam;
    private NavMeshAgent agent;
    private bool playerSelected = false;
    private bool enemySelected = false;
    private GameObject selectedEnemy;


    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>() as Camera;
        agent = gameObject.GetComponent<NavMeshAgent>();


        InvokeRepeating("findEnemy", 2.0f, 0.5f);
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
                    navPos.SetActive(false);
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
                    navPos.SetActive(false);


                }

                if (playerSelected == true)
                {
                    agent.SetDestination(hit.point);
                    enemySelected = false;
                    navPos.transform.position = hit.point;
                    navPos.transform.parent = null;
                    navPos.SetActive(true);
                    Debug.Log("set  postition");
                }

            }
        }

        

        



    }

    private void findEnemy()
    {
        if (enemySelected)
        {
            if (selectedEnemy != null)
            {
                agent.SetDestination(selectedEnemy.transform.position);
            }
            else
            {
                agent.SetDestination(this.transform.position);
                enemySelected = false;
            }

        }

        //else if(enemySelected == false)
        //{

        //}
    }
}

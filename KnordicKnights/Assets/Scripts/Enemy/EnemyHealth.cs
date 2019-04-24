using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 10f;
    public GameObject HealthBar;
    public GameObject Enemy;
    public Animator EnemyAnim;

    private NavMeshAgent agent;
    private float currentHealth;
    private Core.Health.HealthVisualizer healthBar;
    private Dictionary<string, bool> attackingPlayers = new Dictionary<string, bool>();
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            EnemyAnim.SetBool("Attacking", true);
            agent.isStopped = true;

            if (!attackingPlayers.ContainsKey(col.GetInstanceID().ToString()))
            {
                attackingPlayers.Add(col.GetInstanceID().ToString(), true);
                StartCoroutine(AddDamage(1.0f, col.GetInstanceID().ToString()));
                Debug.Log("New Player " + col.GetInstanceID() + " Detected");
            }
            else
            {
                attackingPlayers[col.GetInstanceID().ToString()] = true;
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        EnemyAnim.SetBool("Attacking", false);
        agent.isStopped = false;

        if (col.gameObject.tag == "Player")
        {
            attackingPlayers[col.GetInstanceID().ToString()] = false;
        }
    }


    IEnumerator AddDamage(float amount, string key)
    {
        Debug.Log(attackingPlayers[key]);

        while (attackingPlayers[key])
        {
            if (currentHealth > 0)
            {
                currentHealth = currentHealth - amount;
                Debug.Log(currentHealth / maxHealth);
                HealthBar.transform.localScale = new Vector3(currentHealth / maxHealth, 1f, 1f);

                if (currentHealth <= 0)
                {
                    Destroy(Enemy);
                    Debug.Log("Player Died");
                }
            }

            yield return new WaitForSeconds(2);
        }


    }


}


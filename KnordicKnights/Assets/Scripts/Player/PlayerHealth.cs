using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 10f;
    public GameObject HealthBar;
    public GameObject Player;
    public Animator PlayerAnim;
    private NavMeshAgent agent;


    private float currentHealth;
    private Core.Health.HealthVisualizer healthBar;
    private Dictionary<string, bool> attackingEnemies = new Dictionary<string, bool>();
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
        if (col.gameObject.tag == "Enemy")
        {
            PlayerAnim.SetBool("Attacking", true);
            agent.isStopped=true;

            if (!attackingEnemies.ContainsKey(col.GetInstanceID().ToString()))
            {
                attackingEnemies.Add(col.GetInstanceID().ToString(), true);
                StartCoroutine(AddDamage(1, col.GetInstanceID().ToString()));
                Debug.Log("New Enemy " + col.GetInstanceID() + " Detected");
            }
            else
            {
                attackingEnemies[col.GetInstanceID().ToString()] = true;
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        PlayerAnim.SetBool("Attacking", false);
        agent.isStopped = false;

        if (col.gameObject.tag == "Enemy")
        {
            attackingEnemies[col.GetInstanceID().ToString()] = false;
        }
    }
         

    IEnumerator AddDamage(float amount, string key)
    {
        Debug.Log(attackingEnemies[key]);

        while(attackingEnemies[key])
        {
            if (currentHealth > 0)
            {
                currentHealth = currentHealth - amount;
                Debug.Log(currentHealth / maxHealth);
                HealthBar.transform.localScale = new Vector3(currentHealth / maxHealth, 1f, 1f);

                if (currentHealth <= 0)
                {
                    Destroy(Player);
                    Debug.Log("Player Died");
                }
            }

            yield return new WaitForSeconds(2);
        }
        

    }

    
}

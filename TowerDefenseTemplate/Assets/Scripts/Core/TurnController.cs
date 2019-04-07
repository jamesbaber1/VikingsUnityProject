using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    public GameObject grid;
    public GameObject waveManager;
    public GameObject addBuildingUI;
    public GameObject boats;


    public float difficulty = 1.5f;

    float waveDuration = 1.0f;

    private GameObject[] TowersMenus;


    public void ActivateAttackMode()
    {
        grid.SetActive(false);
        addBuildingUI.SetActive(false);
        TowersMenus = GameObject.FindGameObjectsWithTag("TowerMenu");

        foreach (GameObject TowerMenu in TowersMenus)
        {
            TowerMenu.GetComponent<BoxCollider>().enabled =false;
        }

        Instantiate(boats);
        Invoke("StartSpawing", 10);
        Invoke("StopSpawing", 20* waveDuration);
        waveDuration = waveDuration * difficulty;
    }

    public void ActivateBuildMode()
    {
        Destroy(GameObject.FindGameObjectWithTag("Boat"));
        grid.SetActive(true);
        waveManager.SetActive(false);
        addBuildingUI.SetActive(true);
        TowersMenus = GameObject.FindGameObjectsWithTag("TowerMenu");

        foreach (GameObject TowerMenu in TowersMenus)
        {
            TowerMenu.GetComponent<BoxCollider>().enabled = true;
        }

    }

    void StartSpawing()
    {
        waveManager.SetActive(true);
    }

    void StopSpawing()
    {
        waveManager.SetActive(false);
    }
}

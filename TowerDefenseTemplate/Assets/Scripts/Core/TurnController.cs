using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Economy;
using TowerDefense.UI.HUD;

public class TurnController : MonoBehaviour
{


    public float difficulty = 1.5f;
    public int GoldMineProduction = 5;


    public GameObject grid;
    public GameObject waveManager;
    public GameObject addBuildingUI;
    public GameObject boats;
    public GameObject CurrencyContainer;

    public AudioSource gameMusic;
    public AudioClip AttackMusic;
    public AudioClip buildMusic;




    private int goldMineCount = 0;
    private float waveDuration = 1.0f;

    private GameObject[] TowersMenus;
    private GameObject[] goldMines;
    private CurrencyUI currency;


    private void Start()
    {
        currency = CurrencyContainer.GetComponent<CurrencyUI>();
    }

    public void ActivateAttackMode()
    {
        gameMusic.clip = AttackMusic;
        gameMusic.Play();

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
        gameMusic.clip = buildMusic;
        gameMusic.Play();

        Destroy(GameObject.FindGameObjectWithTag("Boat"));
        grid.SetActive(true);
        waveManager.SetActive(false);
        addBuildingUI.SetActive(true);
        TowersMenus = GameObject.FindGameObjectsWithTag("TowerMenu");

        foreach (GameObject TowerMenu in TowersMenus)
        {
            TowerMenu.GetComponent<BoxCollider>().enabled = true;
        }

        goldMines = GameObject.FindGameObjectsWithTag("GoldMine");

        goldMineCount = 0;
        foreach (GameObject goldMine in goldMines)
        {
            goldMineCount++;
        }

        currency.addCurrenency(GoldMineProduction*goldMineCount);

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

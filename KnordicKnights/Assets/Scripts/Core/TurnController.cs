﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Economy;
using TowerDefense.UI.HUD;
using TowerDefense.Level;
using UnityEngine.UI;

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

    public Text Turn;


    public GameObject LevelManager;
    private LevelManager levelManager;

    private bool endPhase = false;
    private bool spawning = false;
    private int goldMineCount = 0;
    private int turnCount = 1;
    private float waveDuration = 1.0f;

    private GameObject[] TowersMenus;
    private GameObject[] Militia;
    private GameObject[] goldMines;
    private CurrencyUI currency;


    private void Start()
    {
        currency = CurrencyContainer.GetComponent<CurrencyUI>();
        levelManager = LevelManager.GetComponent<LevelManager>();
    }

    private void Update()
    {
        if(endPhase && !spawning)
        {
            levelManager.setNumberOfEnemies(0);
            ActivateBuildMode();
            CancelInvoke();
            currency.addCurrenency(GoldMineProduction * goldMineCount);
            endPhase = false;
        }

        if(Input.GetKeyDown(KeyCode.K))
        {
            endBug();
        }
    }

    public void endBug()
    {
        Militia = GameObject.FindGameObjectsWithTag("Militia");
        foreach (GameObject soldier in Militia)
        {
            Destroy(soldier);
        }
        levelManager.setNumberOfEnemies(0);
    }



    public void ActivateAttackMode()
    {
        gameMusic.clip = AttackMusic;
        gameMusic.Play();

        grid.SetActive(false);
        addBuildingUI.SetActive(false);
        TowersMenus = GameObject.FindGameObjectsWithTag("TowerMenu");
        Militia = GameObject.FindGameObjectsWithTag("Militia");

        foreach (GameObject TowerMenu in TowersMenus)
        {
            TowerMenu.GetComponent<BoxCollider>().enabled =false;
        }

        foreach (GameObject soldier in Militia)
        {
            soldier.GetComponent<BoxCollider>().enabled = false;
        }

        Instantiate(boats);
        Invoke("StartSpawing", 10);
        Invoke("StopSpawing", 15* waveDuration);
        waveDuration = waveDuration * difficulty;
    }

    public void ActivateBuildMode()
    {
        turnCount++;
        Turn.text = "End Turn " + turnCount;
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

        

    }

    void StartSpawing()
    {
        waveManager.SetActive(true);
        spawning = true;
    }

    void StopSpawing()
    {
        waveManager.SetActive(false);
        InvokeRepeating("findEnemies", 3.0f, 3.0f);
        spawning = false;
    }

    void findEnemies()
    {
        if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            endPhase = true;
        }
        else
        {
            endPhase = false;
        }
    }
}

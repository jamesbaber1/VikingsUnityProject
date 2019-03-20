using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    public GameObject grid;

    public GameObject addBuildingUI;

    private GameObject[] TowersMenus;


    public void HideGrid()
    {
        grid.SetActive(false);
        addBuildingUI.SetActive(false);
        TowersMenus = GameObject.FindGameObjectsWithTag("TowerMenu");

        foreach (GameObject TowerMenu in TowersMenus)
        {
            TowerMenu.GetComponent<BoxCollider>().enabled =false;
        }
    }
}

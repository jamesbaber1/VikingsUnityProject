using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SlingTowerAnimation : MonoBehaviour
{

    public GameObject targetter;
    public Animator sling;
    public Animator slingMan;

    private TowerDefense.Targetting.Targetter targetList;

    // Start is called before the first frame update
    void Start()
    {
        targetList = targetter.GetComponent<TowerDefense.Targetting.Targetter> ();
    }

    // Update is called once per frame
    void Update()
    {
        if(targetList.m_TargetsInRange.Any())
        {
            sling.SetBool("Shooting", true);
            slingMan.SetBool("Shooting", true);
        }
        else
        {
            sling.SetBool("Shooting", false);
            slingMan.SetBool("Shooting", false);
        }
        
    }
}

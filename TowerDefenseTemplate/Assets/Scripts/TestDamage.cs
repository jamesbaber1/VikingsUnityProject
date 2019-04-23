using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ActionGameFramework.Health;
using Core.Health;

public class TestDamage : DamageZone
{
    public GameObject objectToDamage;
    private Damager damager;


    // Start is called before the first frame update
    void Start()
    {
        damager = objectToDamage.GetComponent<Damager>();
        InvokeRepeating("addDamage", 1.0f, 1.0f);
    }

    private void addDamage()
    {
        LazyLoad();

        float scaledDamage = ScaleDamage(damager.damage);
        damageableBehaviour.TakeDamage(scaledDamage, new Vector3(0.0f,0.0f,0.0f), damager.alignmentProvider);

        damager.HasDamaged(new Vector3(0.0f, 0.0f, 0.0f), damageableBehaviour.configuration.alignmentProvider);
    }

    
}

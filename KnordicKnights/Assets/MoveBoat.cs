using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBoat : MonoBehaviour
{
    public float speed = 1.0f;
    public string nodeName;
    float step = 0.0f;

    private GameObject node;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        node = GameObject.Find(nodeName);
        target = node.transform;
    }

    // Update is called once per frame
    void Update()
    {
        step = speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        transform.LookAt(target);

    }
}

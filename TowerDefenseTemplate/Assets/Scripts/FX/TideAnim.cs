using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TideAnim : MonoBehaviour 
{
    public float verticalSpeed;
    public float amplitude;
    private Vector3 tempPosition;

    // Start is called before the first frame update
    void Start()
    {
        tempPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tempPosition.y = Mathf.Sin(Time.realtimeSinceStartup * verticalSpeed) * amplitude;
        transform.position = tempPosition;
    }
}

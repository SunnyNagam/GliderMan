using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spin : MonoBehaviour
{
    public bool yaw;
    public bool roll;
    public bool pitch;
    public float speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (yaw) transform.Rotate(transform.forward, speed * Time.deltaTime, Space.World);
        if (roll) transform.Rotate(transform.right, speed * Time.deltaTime, Space.World);
        if (pitch) transform.Rotate(transform.up, speed * Time.deltaTime, Space.World);
    }
}

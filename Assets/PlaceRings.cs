using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceRings : MonoBehaviour
{
    public Transform startAt;
    public Transform buildToward;
    public double spread = 5;
    public double spacing = 100;
    public double noiseResolution = 0.1;
    public double xyzSeparation = 1.5;
    public int maxRings = 10;

    // Start is called before the first frame update
    void Start()
    {
        var lastVector = buildToward;
        for (int i = 0; i < maxRings; i++)
        {
            var nextX = Mathf.PerlinNoise(i*noiseResolution, 0.0f)*spread;
            var nextY = Mathf.PerlinNoise(i*noiseResolution, xyzSeparation)*spread;
            var nextZ = Mathf.PerlinNoise(i*noiseResolution, xyzSeparation*2)*spread;

            // idk
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

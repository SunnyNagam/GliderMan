using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceRings : MonoBehaviour
{
    public Transform startAt;
    public Transform buildToward;
    public float spread = 5.0f;
    public float spacing = 100f;
    public float noiseResolution = 0.1f;
    public float xyzSeparation = 1.5f;
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

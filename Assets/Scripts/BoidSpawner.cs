using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidSpawner : MonoBehaviour
{
    public BoidController boidPrefab;
    public int boidCount;
    public float spawningRadius;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < boidCount; ++i)
        {
            var x = Random.Range(-spawningRadius, spawningRadius);
            var maxDeviationY = spawningRadius - x * x;
            var y = Random.Range(-maxDeviationY, maxDeviationY);
            var boid = Instantiate(boidPrefab, transform.position + new Vector3(x, y), transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidSpawner : MonoBehaviour
{
    public BoidController boidPrefab;
    public int boidCount;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < boidCount; ++i)
        {
            var x = Random.Range(-9.0f, +9.0f);
            var y = Random.Range(-5.0f, +5.0f);
            var boid = Instantiate(boidPrefab, transform.position + new Vector3(x, y), transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

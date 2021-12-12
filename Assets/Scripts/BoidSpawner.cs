using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidSpawner : MonoBehaviour
{
    public BoidController boidPrefab;
    public int boidCount;
    [Range(0, 10)]
    public float alignmentPower;
    [Range(0, 10)]
    public float cohesionPower;
    [Range(0, 10)]
    public float separationPower;
    [Range(0, 10)]
    public float totalPower;
    public bool useCustomVelocity;
    [Range(0, 5)]
    public float customVelocity;
    [Range(0, 2)]
    public float viewRadius;
    [Range(0, 180)]
    public float viewAngle;
    private BoidController[] boids;
    private Vector2[] accelerations;
    // Start is called before the first frame update
    void Start()
    {
        boids = new BoidController[boidCount];
        accelerations = new Vector2[boidCount];
        for (int i = 0; i < boidCount; ++i)
        {
            var x = Random.Range(-9.0f, +9.0f);
            var y = Random.Range(-5.0f, +5.0f);
            boids[i] = Instantiate(boidPrefab, new Vector3(x, y), transform.rotation);
            boids[i].GetComponent<CircleCollider2D>().radius = viewRadius;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < boidCount; ++i)
        {
            boids[i].UpdateVelocity(viewAngle);
            accelerations[i] = boids[i].alignmentVelocity.normalized * alignmentPower
                            + boids[i].cohesionVelocity.normalized * cohesionPower
                            + boids[i].separationVelocity.normalized * separationPower;
        }
        for (int i = 0; i < boidCount; ++i)
            boids[i].rigidbody.velocity += accelerations[i] * totalPower * Time.deltaTime;
        if (useCustomVelocity)
        {
            for (int i = 0; i < boidCount; ++i)
                boids[i].rigidbody.velocity = boids[i].rigidbody.velocity.normalized * customVelocity;
        }
    }
}

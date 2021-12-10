using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidController : MonoBehaviour
{
    public float minInitialSpeed;
    public float maxInitialSpeed;
    public Rigidbody2D rigidbody;
    public float alignmentPower;
    public float cohesionPower;
    public Collider2D collider;
    ContactFilter2D contactFilter;
    [SerializeField]
    private Vector2 alignmentVelocity;
    [SerializeField]
    private Vector2 cohesionVelocity;
    // Start is called before the first frame update
    void Start()
    {
        

        contactFilter = new ContactFilter2D();
        contactFilter.useTriggers = true;
        //rigidbody.rotation = Random.Range(-180, 180);
        transform.Rotate(0, 0, Random.Range(-180, 180));
        rigidbody.velocity = (Vector2)transform.up * Random.Range(minInitialSpeed, maxInitialSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= -9.2)
            transform.position = new Vector3(+9.1f, transform.position.y);
        if (transform.position.x >= +9.2)
            transform.position = new Vector3(-9.1f, transform.position.y);
        if (transform.position.y <= -5.2)
            transform.position = new Vector3(transform.position.x, +5.1f);
        if (transform.position.y >= +5.2)
            transform.position = new Vector3(transform.position.x, -5.1f);

        var totalNeighborVelocity = Vector2.zero;
        var totalNeighborPosition = Vector2.zero;
        List<Collider2D> collisionList = new List<Collider2D>();
        int boidCollisionCount = 0;
        collider.OverlapCollider(contactFilter, collisionList);
        foreach (var collision in collisionList)
        {
            if (collision.gameObject.CompareTag("Boid"))
            {
                ++boidCollisionCount;
                totalNeighborVelocity += collision.gameObject.GetComponent<Rigidbody2D>().velocity;
                totalNeighborPosition += (Vector2)collision.gameObject.transform.position;
            }
        }
        if (boidCollisionCount > 0)
        {
            alignmentVelocity = totalNeighborVelocity / boidCollisionCount;
            cohesionVelocity = totalNeighborPosition / boidCollisionCount;

            var acceleration = (alignmentVelocity - rigidbody.velocity) * alignmentPower
                + (cohesionVelocity - rigidbody.velocity) * cohesionPower;
            rigidbody.velocity += acceleration * Time.deltaTime;
        }
        else
        {
            alignmentVelocity = Vector2.zero;

        }
        
    }
}

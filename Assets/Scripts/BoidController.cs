using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidController : MonoBehaviour
{
    public float minInitialSpeed;
    public float maxInitialSpeed;
    public Rigidbody2D rigidbody;
    public Collider2D collider;
    ContactFilter2D contactFilter;
    public Vector2 alignmentVelocity;
    public Vector2 cohesionVelocity;
    public Vector2 separationVelocity;
    public int neighborBoidCount;
    // Start is called before the first frame update
    void Start()
    {
        contactFilter = new ContactFilter2D();
        contactFilter.useTriggers = true;
        rigidbody.rotation = Random.Range(-180, 180);
        //transform.Rotate(0, 0, Random.Range(-180, 180));
        //transform.LookAt(new Vector3(Random.Range(-1, 1), Random.Range(-1, 1)));
        var speed = Random.Range(minInitialSpeed, maxInitialSpeed);
        rigidbody.velocity = new Vector2(speed * -Mathf.Sin(rigidbody.rotation * Mathf.PI / 180)
                                       , speed * Mathf.Cos(rigidbody.rotation * Mathf.PI / 180));
    }

    public void UpdateVelocity()
    {
        if (transform.position.x < -9.2)
            transform.position = new Vector3(+9.1f, transform.position.y);
        if (transform.position.x > +9.2)
            transform.position = new Vector3(-9.1f, transform.position.y);
        if (transform.position.y < -5.2)
            transform.position = new Vector3(transform.position.x, +5.1f);
        if (transform.position.y > +5.2)
            transform.position = new Vector3(transform.position.x, -5.1f);

        var totalNeighborVelocity = Vector2.zero;
        var totalNeighborPosition = Vector2.zero;
        separationVelocity = Vector2.zero;

        List<Collider2D> collisionList = new List<Collider2D>();
        neighborBoidCount = 0;
        collider.OverlapCollider(contactFilter, collisionList);
        foreach (var collision in collisionList)
        {
            if (collision.gameObject.CompareTag("Boid"))
            {
                ++neighborBoidCount;
                totalNeighborVelocity += collision.gameObject.GetComponent<Rigidbody2D>().velocity;
                totalNeighborPosition += (Vector2)collision.gameObject.transform.position;
                Vector2 distanceVector = collision.gameObject.transform.position - transform.position;
                var distance = distanceVector.magnitude;
                separationVelocity -= (1 / distance) * distanceVector.normalized;
            }
        }
        if (neighborBoidCount > 0)
        {
            alignmentVelocity = totalNeighborVelocity / neighborBoidCount;
            cohesionVelocity = totalNeighborPosition / neighborBoidCount;
        }
        else
        {
            alignmentVelocity = Vector2.zero;
            cohesionVelocity = Vector2.zero;
        }
        //rigidbody.rotation = -Mathf.Asin(rigidbody.velocity.normalized.x) * 180 / Mathf.PI;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidController : MonoBehaviour
{
    public float minInitialSpeed;
    public float maxInitialSpeed;
    public Rigidbody2D rigidbody;
    // Start is called before the first frame update
    void Start()
    {
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
    }
}

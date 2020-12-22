using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobController : MonoBehaviour
{

    private GameObject target;
    private Vector3 dir;
    private Rigidbody2D rigidbody;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        dir = transform.position - target.transform.position;
        rigidbody.AddForce(dir * Mathf.Min(speed, rigidbody.velocity.magnitude - speed));
    }
}

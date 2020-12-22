using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //outlet
    Rigidbody2D rigidbody;
    public Transform aimPivot;
    public GameObject projectilePrefab;
    private float speed = 10f;
    private float jump_force = 10f;
    public int jumpsLeft = 2;
    private float max_horizontal_velocity = 10f;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        DontDestroyOnLoad(gameObject);
    }

    void FixedUpdate()
    {
        animator.SetFloat("Speed", rigidbody.velocity.magnitude);
    }


    // Update is called once per frame
    void Update()
    {
        Vector2 cur_velocity = rigidbody.velocity;

        if (Input.GetKey(KeyCode.A))
        {
            if (cur_velocity.x - speed >= -max_horizontal_velocity)
            {
                rigidbody.AddForce(Vector2.left * speed);
            }
            else if (max_horizontal_velocity + cur_velocity.x > 0)
            {
                rigidbody.AddForce(Vector2.left * (max_horizontal_velocity + cur_velocity.x));
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (speed + cur_velocity.x <= max_horizontal_velocity)
            {
                rigidbody.AddForce(Vector2.right * speed);
            }
            else if (max_horizontal_velocity - cur_velocity.x > 0)
            {
                rigidbody.AddForce(Vector2.right * (max_horizontal_velocity - cur_velocity.x));
            }
        }
        if (Input.GetKey(KeyCode.W))
        {
            if (cur_velocity.y - speed >= -max_horizontal_velocity)
            {
                rigidbody.AddForce(Vector2.up * speed);
            }
            else if (max_horizontal_velocity + cur_velocity.y > 0)
            {
                rigidbody.AddForce(Vector2.up * (max_horizontal_velocity + cur_velocity.y));
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (speed + cur_velocity.y <= max_horizontal_velocity)
            {
                rigidbody.AddForce(Vector2.down * speed);
            }
            else if (max_horizontal_velocity - cur_velocity.y > 0)
            {
                rigidbody.AddForce(Vector2.down * (max_horizontal_velocity - cur_velocity.y));
            }
        }

        // Aim Toward Mouse
        Vector3 mousePosition = Input.mousePosition;
        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 directionFromPlayerToMouse = mousePositionInWorld - transform.position;

        float radiansToMouse = Mathf.Atan2(directionFromPlayerToMouse.y, directionFromPlayerToMouse.x);
        float angleToMouse = radiansToMouse * 180f / Mathf.PI;

        aimPivot.rotation = Quaternion.Euler(0, 0, angleToMouse);

        // Shoot Stuff
        if (Input.GetMouseButtonDown(0))
        {
            GameObject newProjectile = Instantiate(projectilePrefab);
            newProjectile.transform.position = transform.position;
            newProjectile.transform.rotation = aimPivot.rotation;
        }

        // Double Jump
        if (Input.GetKeyDown(KeyCode.Space) && jumpsLeft > 0)
        {
            rigidbody.AddForce(new Vector2(0, jump_force), ForceMode2D.Impulse);
            jumpsLeft--;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            // Check beneath
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, -transform.up, 0.7f);

            for (int i = 0; i < hits.Length; ++i)
            {
                if (hits[i].collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    jumpsLeft = 2;
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] ParticleSystem destroyParticles;

    public Transform target;
    private Vector3 v_diff;
    private float atan2;
    public float moveSpeed = 4;
    public float standardMoveSpeed;
    Rigidbody2D rb;
    PolygonCollider2D collider;
    SpriteRenderer renderer;

    private void Start()
    {
        standardMoveSpeed = moveSpeed;
        target = FindObjectOfType<PlayerMovement>().transform;
        rb = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<PolygonCollider2D>();


        v_diff = (target.position - transform.position);
        atan2 = Mathf.Atan2(v_diff.y, v_diff.x);
        transform.rotation = Quaternion.Euler(0f, 0f, atan2 * Mathf.Rad2Deg);
    }

    void Update()
    {
        rb.velocity = (transform.right * moveSpeed);
        if(FindObjectOfType<PlayerMovement>().timeSlowed)
        {
            moveSpeed = standardMoveSpeed / 2;
        }
        else
        {
            moveSpeed = standardMoveSpeed;
        }
    }

    public void DeleteProjectile()
    {
        var particles = Instantiate(destroyParticles, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}


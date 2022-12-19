using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private AttackDetails attackDetails;

    private float
        speed,
        travelDistance,
        xStartPos;

    [SerializeField]
    private float 
        gravity,
        damageRadius;

    [SerializeField]
    private LayerMask
        whatIsGround,
        whatIsPlayer;

    [SerializeField]
    private Transform damagePosition;

    private Rigidbody2D rb;
    private bool isGravityOn;
    private bool hasHitGround;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = 0.0f;
        rb.velocity = transform.right * speed;

        xStartPos = transform.position.x;
    }

    private void Update()
    {
        if(!hasHitGround)
        {
            attackDetails.position = transform.position;

            if(isGravityOn)
            {
                float angle = Mathf.Atan2(rb.velocity.x, rb.velocity.y) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }

    private void FixedUpdate()
    {
        if (!hasHitGround)
        {
            Collider2D damageHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsPlayer);
            Collider2D groundHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsGround);

            if(damageHit && !damageHit.gameObject.GetComponent<PlayerController>().GetDashStatus())
            {
                damageHit.transform.SendMessage("Damage", attackDetails);
                Destroy(gameObject);
            }

            if(groundHit)
            {
                //hasHitGround = true;
                //rb.gravityScale = 0f;
                //rb.velocity = Vector2.zero;
                Destroy(gameObject);
            }
            if (Mathf.Abs(xStartPos - transform.position.x) >= travelDistance && !isGravityOn)
            {
                isGravityOn = true;
                rb.gravityScale = gravity;
            }
        }
    }

    public void FireProjectile(float speed, float travelDistance, float damage)
    {
        this.speed = speed;
        this.travelDistance = travelDistance;
        attackDetails.damageAmount = damage;
    } 

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(damagePosition.position, damageRadius);
    }
}

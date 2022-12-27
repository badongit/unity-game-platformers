using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    public float speed = 5;

    //PlayerCheck
    public Transform playerCheck;
    public bool canAttack;
    public float playerCheckRadius;
    public LayerMask whatIsPlayer;

    //Attack
    [SerializeField]
    private bool isAttacking;
    private bool combatEnabale;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ChechAttack();
        CheckPlayer();
        Attacking();
        AnimationState();
    }

    public void Move()
    {
        GameObject g = GameObject.FindWithTag("Player");

        Vector2 gPos = g.transform.position;

        Vector2 pos = this.transform.position;

        Vector2 dir = gPos - pos;
        dir = dir.normalized;

        pos += dir * speed * Time.deltaTime;

        this.transform.position = pos;
    }

    public void ChechAttack()
    {
        if (!canAttack)
        {
            isAttacking = false;
        }
        else
        {
            isAttacking = true;
        }
    }

    public void CheckPlayer()
    {
        canAttack = Physics2D.OverlapCircle(playerCheck.position, playerCheckRadius, whatIsPlayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(playerCheck.position, playerCheckRadius);
    }

    public void AnimationState()
    {
        
            anim.SetBool("canAttack", canAttack);
        
    }

    public void Attacking()
    {
        if (isAttacking)
        {
            //onTriggerEnter
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    [SerializeField] private bool combatEnabled = true;

    [SerializeField] 
    private float 
        inputTimer,
        attack1Radius,
        attack1Damage;

    [SerializeField] private Transform attack1HitBoxPos;

    [SerializeField] private LayerMask whatIsDamageable;

    private bool 
        gotInput,
        isAttacking,
        isFirstAttack;

    private float lastInputTime = Mathf.NegativeInfinity;

    private float[] attackDetails = new float[2];

    private Animator anim;

    private PlayerController PC;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("canAttack", combatEnabled);
        PC = GetComponent<PlayerController>();
    }

    private void Update()
    {
        CheckCombatInput();
        CheckAttacks();
    }

    private void CheckCombatInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (combatEnabled)
            {
                // attempt combat
                gotInput = true;
                lastInputTime = Time.time;
            }
        }
    }

    private void CheckAttacks()
    {
        if (gotInput)
        {
            // perform attack
            if (!isAttacking)
            {
                gotInput = false;
                isAttacking = true;
                isFirstAttack = !isFirstAttack;
                anim.SetBool("attack1", true);
                anim.SetBool("isAttacking", isAttacking);
                anim.SetBool("firstAttack", isFirstAttack);
            }
        }

        if (Time.time >= lastInputTime + inputTimer)
        {
            // wait for new input
            gotInput = false;
        }
    }

    private void CheckAttackHitBox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attack1HitBoxPos.position, attack1Radius, whatIsDamageable);

        foreach (Collider2D collider in detectedObjects)
        {
            attackDetails[0] = attack1Damage;
            attackDetails[1] = transform.position.x;

            collider.transform.parent.SendMessage("Damage", attackDetails);
        }
    }

    private void FinishAttack1()
    {
        isAttacking = false;
        anim.SetBool("isAttacking", isAttacking);
        anim.SetBool("attack1", false);
    }

    private void Damage(float[] attackDetails)
    {
        int direction;

        if(attackDetails[1] > transform.position.x)
        {
            direction = -1;
        } else
        {
            direction = 1;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attack1HitBoxPos.position, attack1Radius);
    }
}

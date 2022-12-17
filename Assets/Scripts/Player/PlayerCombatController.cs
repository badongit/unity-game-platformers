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
        attack1Damage,
        stunDamageAmount = 1f;

    [SerializeField] private Transform attack1HitBoxPos;

    [SerializeField] private LayerMask whatIsDamageable;

    private bool 
        gotInput,
        isAttacking,
        isFirstAttack;

    private float lastInputTime = Mathf.NegativeInfinity;

    private AttackDetails attackDetails;

    private Animator anim;

    private PlayerController PC;
    private PlayerStats PS;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("canAttack", combatEnabled);
        PC = GetComponent<PlayerController>();
        PS = GetComponent<PlayerStats>();
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
            attackDetails.damageAmount = attack1Damage;
            attackDetails.position = transform.position;
            attackDetails.stunDamageAmount = stunDamageAmount;

            collider.transform.parent.SendMessage("Damage", attackDetails);
        }
    }

    private void FinishAttack1()
    {
        isAttacking = false;
        anim.SetBool("isAttacking", isAttacking);
        anim.SetBool("attack1", false);
    }

    private void Damage(AttackDetails attackDetails)
    {
        if(!PC.GetDashStatus())
        {
            int direction;

            PS.DecreaseHealth(attackDetails.damageAmount);

            if (attackDetails.position.x > transform.position.x)
            {
                direction = -1;
            }
            else
            {
                direction = 1;
            }

            PC.Knockback(direction);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attack1HitBoxPos.position, attack1Radius);
    }
}

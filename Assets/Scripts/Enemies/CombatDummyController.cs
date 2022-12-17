using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatDummyController : MonoBehaviour
{
    [SerializeField] private float maxHealth = 50f;
    [SerializeField] private float knockbackSpeedX = 10f;
    [SerializeField] private float knockbackSpeedY = 5f;
    [SerializeField] private float knockbackDuration = 0.1f;
    [SerializeField] private float knockbackDeathSpeedX = 15f;
    [SerializeField] private float knockbackDeathSpeedY = 8f;
    [SerializeField] private float deathTorque = 1f;
    [SerializeField] private bool applyKnockback = true;
    [SerializeField] private GameObject hitParticles;

    private float currentHealth;
    private float knockbackStart;

    private int playerFacingDirection;

    private bool playerOnLeft;
    private bool knockback;

    private PlayerController pc;
    private GameObject aliveGO;
    private GameObject brokenTopGO;
    private GameObject brokenBotGO;
    private Rigidbody2D rbAlive;
    private Rigidbody2D rbBrokenTop;
    private Rigidbody2D rbBrokenBot;
    private Animator aliveAnim;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        pc = GameObject.Find("Player").GetComponent<PlayerController>();

        aliveGO = transform.Find("Alive").gameObject;
        brokenTopGO = transform.Find("Broken Top").gameObject;
        brokenBotGO = transform.Find("Broken Bottom").gameObject;

        aliveAnim = aliveGO.GetComponent<Animator>();
        rbAlive = aliveGO.GetComponent<Rigidbody2D>();
        rbBrokenTop = brokenTopGO.GetComponent<Rigidbody2D>();
        rbBrokenBot = brokenBotGO.GetComponent<Rigidbody2D>();

        aliveGO.SetActive(true);
        brokenTopGO.SetActive(false);
        brokenBotGO.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CheckKnockback();
    }

    private void Damage(AttackDetails attackDetails)
    {
        currentHealth -= attackDetails.damageAmount;

        if(attackDetails.position.x > aliveGO.transform.position.x) {
            playerFacingDirection = -1;
        } else
        {
            playerFacingDirection = 1;
        }

        Instantiate(hitParticles, aliveGO.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));

        if(playerFacingDirection == 1)
        {
            playerOnLeft = true;
        } else
        {
            playerOnLeft = false;
        }

        aliveAnim.SetBool("playerOnLeft", playerOnLeft);
        aliveAnim.SetTrigger("damage");

        if(applyKnockback && currentHealth > 0.0f)
        {
            Knockback();
        }

        if(currentHealth <= 0.0f)
        {
            Die();
        }
    }

    private void Knockback()
    {
        knockback = true;
        knockbackStart = Time.time;
        rbAlive.velocity = new Vector2(knockbackSpeedX * playerFacingDirection, knockbackSpeedY);
    }

    private void CheckKnockback()
    {
        if(Time.time >= knockbackStart + knockbackDuration && knockback)
        {
            knockback = false;
            rbAlive.velocity = new Vector2(0.0f, rbAlive.velocity.y);
        }
    }

    private void Die()
    {
        aliveGO.SetActive(false);
        brokenTopGO.SetActive(true);
        brokenBotGO.SetActive(true);

        rbBrokenTop.transform.position = aliveGO.transform.position;
        rbBrokenBot.transform.position = aliveGO.transform.position;

        rbBrokenBot.velocity = new Vector2(knockbackSpeedX * playerFacingDirection, knockbackSpeedY);
        rbBrokenTop.velocity = new Vector2(knockbackDeathSpeedX * playerFacingDirection, knockbackDeathSpeedY);
        rbBrokenTop.AddTorque(deathTorque * -playerFacingDirection, ForceMode2D.Impulse);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;
    public HeathBar healthBar;
    public void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Item"))
        {
            Destroy(collision.gameObject);
            takeHP(5);
        }
        if (collision.gameObject.CompareTag("ItemHP"))
        {
            Destroy(collision.gameObject);
            takeHP(20);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            takeDamageHP(20);
        }
    }

    public void takeHP(int demage)
    {
        currentHealth += demage;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthBar.SetHealth(currentHealth);
    }

    public void takeDamageHP(int demage)
    {
        currentHealth -= demage;
        if (currentHealth <=0)
        {
            currentHealth = maxHealth;

        }
        healthBar.SetHealth(currentHealth);
    }
}

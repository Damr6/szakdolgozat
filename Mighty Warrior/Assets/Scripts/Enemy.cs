using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    int currentHealth;
    public static int attackDamage;
    public static float attackRate;
    public static float nextAttackTime = 0f;

    //Slime
    public int slimeMaxHealth = 100;
    public int slimeAttackDamage = 1;
    public float slimeAttackRate = 1f;


    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag == "Slime")
        {
            currentHealth = slimeMaxHealth;
            attackDamage = slimeAttackDamage;
            attackRate = slimeAttackRate;
            
        }
    }

    public void TakeDamage(int damage)
    {
        if(currentHealth > 0)
        {
            currentHealth -= damage;

            // Hurt animation

        }

        else
        {
            Die();
        }

        void Die()
        {
            // Die animation
            Debug.Log("Enemy died!");

            GetComponent<Collider2D>().enabled = false;
            this.enabled = false;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

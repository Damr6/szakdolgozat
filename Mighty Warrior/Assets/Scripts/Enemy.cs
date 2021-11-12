using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    float agroRange;
    float moveSpeed;
    public static Animator animator;
    Rigidbody2D rb2d;

    

    int currentHealth;
    public static int attackDamage;
    public static float attackRate;
    public static float nextAttackTime = 0f;

    //Slime
    public Animator slimeAnimator;
    public int slimeMaxHealth = 100;
    public int slimeAttackDamage = 1;
    public float slimeAttackRate = 1f;
    public float slimeAgroRange = 5;
    public float slimeMoveSpeed = 4;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        if (gameObject.tag == "Slime")
        {
            // Sprite looks the opposite way by default
            GetComponent<SpriteRenderer>().flipX = true;

            animator = slimeAnimator;

            currentHealth = slimeMaxHealth;
            attackDamage = slimeAttackDamage;
            attackRate = slimeAttackRate;
            agroRange = slimeAgroRange;
            moveSpeed = slimeMoveSpeed;
            
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
            GetComponent<SpriteRenderer>().sprite = null;
            GetComponent<Collider2D>().enabled = false;
            this.enabled = false;

            

        }
    }

    // Update is called once per frame
    void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, player.position);

        if (distToPlayer < agroRange)
        {
            Chase();
        }
        else
        {
            StopChase();
        }
    }

    void Chase()
    {
        animator.SetBool("Walk", true);

        if(transform.position.x < player.position.x)
        {
            // enemy to the left, move right
            rb2d.velocity = new Vector2(moveSpeed, 0);
            transform.localScale = new Vector2(1, 1);

        }
        else
        {
            //enemy to the right, move left
            rb2d.velocity = new Vector2(-moveSpeed, 0);
            transform.localScale = new Vector2(-1, 1);
        }
    }

    void StopChase()
    {
        animator.SetBool("Walk", false);
        rb2d.velocity = new Vector2(0,0);
    }
}

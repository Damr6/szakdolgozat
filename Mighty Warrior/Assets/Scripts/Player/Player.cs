using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{

    public static int levelAmount = 5;
    public static int levelToGo;

    public CharacterController2D controller;
    public Animator playerAnimator;

    public float runSpeed = 40f;
    public float tempRunSpeed;
    float horizontalMove = 0f;

    bool playerAlive;

    bool jumpAllow = true;
    bool jump = false;

    bool crouch = false;
    bool byWall = false;

    bool canMove = true;

    public LayerMask enemyLayers;
    public Transform attackPoint;

    public float attackRate = 2f;
    public float nextAttackTime = 0f;
    public float attackRange = 0.5f;
    public int attackDamage = 20;
    public int maxHealth = 1000;
    int currentHealth;

    public HealthBar healthBar;
    public TMP_Text dieMessage;
    public TMP_Text winMessage;

    public Sprite openedChest;
    public Sprite openedDoor;

    private float nextDamageTime = Enemy.attackRate;
    private bool slimeCanAttack;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        playerAlive = true;
    }

    // Update is called once per frame (Inputs)
    void Update()
    {
        if (playerAlive && !PauseMenu.GameIsPaused && canMove)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

            // Move animation stops when player by wall
            if (byWall)
            {
                playerAnimator.SetFloat("Speed", 0);
            }
            else
            {
                playerAnimator.SetFloat("Speed", Mathf.Abs(horizontalMove));
            }


            if (Input.GetButtonDown("Jump") && jumpAllow)
            {
                jump = true;
                playerAnimator.SetBool("IsJumping", true);
            }

            if (Input.GetButtonDown("Crouch"))
            {
                crouch = true;
            }
            else if (Input.GetButtonUp("Crouch"))
            {
                crouch = false;
            }

            // Limit the time between attacks
            if (Time.time >= nextAttackTime)
            {
                // Time speed has nothing to do with the Slash animation
                if (Input.GetButtonDown("Slash") && !playerAnimator.GetBool("IsCrouching") && !playerAnimator.GetBool("IsJumping"))
                {
                    // Not allowed to jump and move during the slash
                    tempRunSpeed = runSpeed;
                    runSpeed = 0f;
                    jumpAllow = false;

                    nextAttackTime = Time.time + 1f / attackRate;
                    playerAnimator.SetTrigger("Slash");
                    Invoke("Attack", 0.3f);
                }
            }
        }
    }

    // Update the character
    void FixedUpdate()
    {

        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;

    }


    /*
     * 
     *  FUNCTIONS
     * 
     */


    void Attack()
    {
        // Attack animation

        // Detect enemies in range of the attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Damage them
        foreach (Collider2D enemy in hitEnemies)
        {

            if (enemy.tag == "Breakable")
            {
                BreakItem(enemy);
            }
            else
            {
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
                Debug.Log("We hit " + enemy.name);
            }

        }

        runSpeed = tempRunSpeed;
        jumpAllow = true;
    }

    public void TakeDamage(int damage)
    {
        
        if (currentHealth > 0)
        {
            Enemy.animator.SetTrigger("Attack");
            //Debug.Log(Enemy.animator.GetCurrentAnimatorStateInfo().length);
            Debug.Log("nextDamageTime: " + nextDamageTime);
            Debug.Log("Time.time: " + Time.time);
            if (Time.time >= nextDamageTime)
            {
                nextDamageTime = Time.time + Enemy.attackRate;

                if (!playerAnimator.GetBool("IsCrouching"))
                {
                    currentHealth -= damage;
                    healthBar.SetHealth(currentHealth);
                    Debug.Log("hit");

                    // Hurt animation
                }
            }
        }

        else if (playerAlive)
        {
            Die();
        }
        
    }

    void Die()
    {
        dieMessage.gameObject.SetActive(true);
        Debug.Log("You Died!");
        Time.timeScale = 0.3f;
        horizontalMove = 0;
        playerAnimator.SetFloat("Speed", 0);
        playerAlive = false;

        // Die animation

        Invoke("ReloadScene", 1f);

    }

    public void ReloadScene()
    {
        Time.timeScale = 1f;
        PauseMenu.GameIsPaused = false;

        LevelLoader.levelToGo = SceneManager.GetActiveScene().buildIndex;
        LevelLoader.startLoad = true;
    }

    public void LoadLobby()
    {
        Time.timeScale = 1f;
        PauseMenu.GameIsPaused = false;

        LevelLoader.levelToGo = 2; //Lobby is 2nd in build
        LevelLoader.startLoad = true;
    }

    // Attack range circle
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void OnLanding()
    {
        playerAnimator.SetBool("IsJumping", false);
    }

    public void OnCrouching(bool isCrouching)
    {
        playerAnimator.SetBool("IsCrouching", isCrouching);

    }

    void BreakItem(Collider2D collider)
    {
        collider.GetComponent<SpriteRenderer>().sprite = null;
        collider.GetComponent<Collider2D>().enabled = false;
        collider.enabled = false;

        // Break animation
        // XP, coin and health can be looted
    }

    void UseItem(Collider2D collider)
    {

        if (!playerAnimator.GetBool("IsJumping") && playerAlive)
        {
            if (collider.gameObject.tag == "Chest")
            {

                collider.GetComponent<SpriteRenderer>().sprite = openedChest;
                collider.GetComponent<Collider2D>().enabled = false;
                collider.enabled = false;

                // XP, coin and health can be looted

                winMessage.gameObject.SetActive(true);
                Debug.Log("You Won!");
                Time.timeScale = 0.3f;
                horizontalMove = 0;
                playerAnimator.SetFloat("Speed", 0);

                // Die animation

                Invoke("LoadLobby", 1f);

            }

            else if (collider.gameObject.tag == "Door")
            {
                canMove = false;
                runSpeed = 0f;
                collider.GetComponent<SpriteRenderer>().sprite = openedDoor;
                collider.GetComponent<Collider2D>().enabled = false;
                collider.enabled = false;

                // Switch levels with doors

                for (int level = 0; level < levelAmount; level++)
                {
                    if (collider.gameObject.name == "Door" + level)
                    {
                        LevelLoader.levelToGo = SceneManager.GetActiveScene().buildIndex + level;
                        LevelLoader.startLoad = true;
                    }
                }
            }

            Debug.Log("We used " + collider.gameObject.tag);
        }
    }


    /*
     * 
     *  TRIGGERS, COLLIDERS
     * 
     */


    public void OnTriggerEnter2D(Collider2D collider)
    {

    }


    public void OnTriggerStay2D(Collider2D collider)
    {
        if (Input.GetButtonDown("Use") && collider.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            UseItem(collider);
        }

        if (collider.gameObject.layer == LayerMask.NameToLayer("Enemy") && collider.gameObject.tag != ("Breakable"))
        {
            TakeDamage(Enemy.attackDamage);
        }
    }


    public void OnTriggerExit2D(Collider2D collider)
    {

    }


    public void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Wall") && byWall == false)
        {
            byWall = true;
        }
        if (collider.gameObject.layer == LayerMask.NameToLayer("Enemy") && collider.gameObject.tag != ("Breakable"))
        {
            nextDamageTime = Time.time + 0.5f;
        }
    }


    public void OnCollisionStay2D(Collision2D collider)
    {

        if (collider.gameObject.layer == LayerMask.NameToLayer("Enemy") && collider.gameObject.tag != ("Breakable"))
        {
            TakeDamage(Enemy.attackDamage);
            //Debug.Log("Player HP:" + currentHealth);
        }
    }


    public void OnCollisionExit2D(Collision2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Wall") && byWall == true)
        {
            byWall = false;
        }
        if (collider.gameObject.layer == LayerMask.NameToLayer("Enemy") && collider.gameObject.tag != ("Breakable"))
        {
            nextDamageTime = Time.time;
        }
    }
}


/*
 * Debug.Log can log twice at collisions because the player consists of 2 colliders.
 * 
 */

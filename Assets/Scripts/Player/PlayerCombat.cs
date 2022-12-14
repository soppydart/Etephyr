using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] Animator myAnimator;
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] LayerMask skeletonLayer;
    [SerializeField] LayerMask miniBossLayer;
    [SerializeField] LayerMask bossLayer;
    [SerializeField] LayerMask monkLayer;
    [SerializeField] int attackDamage = 50;
    [SerializeField] float attackDuration = 0.8f;
    Rigidbody2D myRigidbody;
    PlayerMovement playerMovement;
    [SerializeField] float maxHealth = 100;
    [SerializeField] float currentHealth;
    public bool isAttacking = false;
    [SerializeField] GameObject GameOverBackground;
    [SerializeField] GameObject GameOverButtons;
    [SerializeField] Slider slider;
    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        myRigidbody = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        slider.value = 100;
    }
    void Update()
    {
        slider.value = currentHealth;
    }
    public bool attackAllowed = true;
    void OnAttack()
    {
        if (!attackAllowed)
            return;
        playerMovement.isPlayerMovementAllowed = false;
        attackAllowed = false;
        myRigidbody.velocity = new Vector2(0f, 0f);
        myAnimator.SetTrigger("Attack");
        StartCoroutine(OverridingAttackAnimation());

    }
    public void Attack()
    {
        Collider2D[] hitSkeletons = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, skeletonLayer);
        Collider2D[] hitMiniBoss = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, miniBossLayer);
        foreach (Collider2D enemy in hitSkeletons)
            enemy.GetComponent<Skeleton>().TakeDamage(attackDamage);
        foreach (Collider2D enemy in hitMiniBoss)
            enemy.GetComponent<MiniBoss>().TakeDamage(attackDamage);
        Collider2D[] hitBoss = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, bossLayer);
        foreach (Collider2D enemy in hitBoss)
            enemy.GetComponent<Boss>().TakeDamage(attackDamage);
        Collider2D[] hitMonk = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, monkLayer);
        foreach (Collider2D monk in hitMonk)
            monk.GetComponent<Monk>().TakeDamage();
    }
    IEnumerator OverridingAttackAnimation()
    {
        // yield return new WaitForSeconds(0.5f);
        // Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        // foreach (Collider2D enemy in hitEnemies)
        // {
        //     enemy.GetComponent<Skeleton>().TakeDamage(attackDamage);
        // }
        yield return new WaitForSeconds(attackDuration);
        attackAllowed = true;
        playerMovement.isPlayerMovementAllowed = true;
    }
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    public void TakeDamage(int damage)
    {
        if (GetComponent<PlayerMovement>().isDodging)
            return;
        currentHealth -= damage;
        myAnimator.SetTrigger("isHurt");
        if (currentHealth <= 0)
        {
            myAnimator.SetTrigger("isDead");
            Debug.Log("You Died");
            FindObjectOfType<AudioManager>().GetComponent<AudioManager>().LowerPitch();
            StartCoroutine(Die());
            StartCoroutine(ShowGameOverButtons());
        }
    }
    IEnumerator Die()
    {
        yield return new WaitForSeconds(0f);
        if (showGameOver == true)
        {
            showGameOver = false;
            GameOverBackground.gameObject.SetActive(true);
            StartCoroutine(ShowGameOverButtons());
        }
    }
    public bool showGameOver = true;
    public void TouchedTraps()
    {
        myAnimator.SetTrigger("Trap");
        if (!showGameOver)
            return;
        showGameOver = false;
        GameOverBackground.gameObject.SetActive(true);
        FindObjectOfType<AudioManager>().GetComponent<AudioManager>().LowerPitch();
        StartCoroutine(ShowGameOverButtons());
    }
    IEnumerator ShowGameOverButtons()
    {
        yield return new WaitForSeconds(1.5f);
        currentHealth = 100;
        GameOverButtons.gameObject.SetActive(true);
        myAnimator.SetTrigger("Revive");
    }
    public void SetHealth(float h)
    {
        currentHealth = h;
    }
    public float GetHealth()
    {
        return currentHealth;
    }
}
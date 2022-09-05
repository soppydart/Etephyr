using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] int currentHealth;
    [SerializeField] Animator myAnimator;
    [SerializeField] Transform player;
    [SerializeField] Transform RangeCenter;
    [SerializeField] Transform RangeLimit;
    [SerializeField] float attackRange = 3f;
    [SerializeField] int attackDamage = 20;
    [SerializeField] Transform skeletonAttackPoint;
    [SerializeField] float skeletonAttackRange = 1f;
    public float skeletonMoveSpeed = 1f;
    public int skeletonMoveDirection = -1;
    [SerializeField] LayerMask playerLayer;
    Rigidbody2D myRigidbody;
    public SpriteRenderer mySpriteRenderer;
    public bool playerIsInRange = false;
    float distanceBetweenSkeletonAndAttackPoint;
    [SerializeField] Transform boundary1;
    [SerializeField] Transform boundary2;
    public bool isAttacking = false;
    void Start()
    {
        currentHealth = maxHealth;
        myRigidbody = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        distanceBetweenSkeletonAndAttackPoint = transform.position.x - skeletonAttackPoint.position.x;
        // boundary1.position = new Vector2(RangeCenter.position.x + Mathf.Abs
        // (distanceBetweenSkeletonAndAttackPoint), boundary1.position.y);
        // boundary2.position = new Vector2(RangeCenter.position.x - Mathf.Abs
        // (distanceBetweenSkeletonAndAttackPoint), boundary2.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        CheckRange();
        LookAtPlayer();
        CheckAttackRange();
        FlipSkeletonAttackPoint();
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        myAnimator.SetTrigger("isHit");
        if (currentHealth <= 0)
            Die();
    }
    void Die()
    {
        myAnimator.SetTrigger("isDead");
        StartCoroutine(DestroySkeleton());
    }
    IEnumerator DestroySkeleton()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
    void CheckRange()
    {
        // if (Mathf.Abs(RangeCenter.position.x - player.position.x) <=
        // Mathf.Abs(RangeCenter.position.x - RangeLimit.position.x))
        if (Vector2.Distance(RangeCenter.position, player.position) <=
        Vector2.Distance(RangeCenter.position, RangeLimit.position))
        {
            playerIsInRange = true;

            myAnimator.SetBool("isPlayerInRange", true);
        }
        else
        {
            playerIsInRange = false;
            myAnimator.SetBool("isPlayerInRange", false);
        }
    }
    void CheckAttackRange()
    {
        if (Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            myAnimator.SetBool("isPlayerInAttackRange", true);
        }
        else
        {
            myAnimator.SetBool("isPlayerInAttackRange", false);
        }
    }
    public void FlipSkeleton()
    {
        skeletonMoveDirection *= -1;
    }
    public void LookAtPlayer()
    {
        if (isAttacking)
            return;
        if (!playerIsInRange)
            return;
        if (transform.position.x > player.position.x)
            mySpriteRenderer.flipX = true;
        else if (transform.position.x < player.position.x)
            mySpriteRenderer.flipX = false;
    }
    void FlipSkeletonAttackPoint()
    {
        if (mySpriteRenderer.flipX)
        {
            skeletonAttackPoint.position = new Vector2
        (transform.position.x - Mathf.Abs(distanceBetweenSkeletonAndAttackPoint),
        skeletonAttackPoint.position.y);
        }
        else
        {
            skeletonAttackPoint.transform.position = new Vector2
        (transform.position.x + Mathf.Abs(distanceBetweenSkeletonAndAttackPoint),
        skeletonAttackPoint.position.y);
        }
    }
    public void SkeletonAttack()
    {
        Collider2D[] playerCollider = Physics2D.OverlapCircleAll
        (skeletonAttackPoint.position, skeletonAttackRange, playerLayer);
        foreach (Collider2D player in playerCollider)
        {
            player.GetComponent<PlayerCombat>().TakeDamage(attackDamage);
        }
    }
    void OnDrawGizmosSelected()
    {
        if (skeletonAttackPoint == null)
            return;
        Gizmos.DrawWireSphere(skeletonAttackPoint.position, skeletonAttackRange);
    }
}

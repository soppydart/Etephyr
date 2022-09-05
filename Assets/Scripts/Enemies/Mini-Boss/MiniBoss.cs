using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniBoss : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] int currentHealth;
    [SerializeField] Transform player;
    SpriteRenderer mySpriteRenderer;
    public bool isMovementAllowed = false;
    public bool isAttacking = false;
    public bool isResting = false;
    [SerializeField] Transform hitPoint;
    [SerializeField] float hitRange = 2f;
    [SerializeField] int attackDamage = 50;
    [SerializeField] LayerMask playerLayer;
    Animator myAnimator;
    float distanceBetweenMiniBossAndHitPoint;
    public int comboHits = 7;
    //This is for casting spells
    [SerializeField] GameObject spell;
    [SerializeField] Transform spellGenerationPoint;
    public bool spellCastingAllowed = true;
    [SerializeField] GameObject invisibleWall;
    [SerializeField] GameObject cutsceneTrigger;
    [SerializeField] Slider healthSlider;
    [SerializeField] GameObject MiniBossHealthBar;
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        distanceBetweenMiniBossAndHitPoint = transform.position.x - hitPoint.position.x;
        myAnimator = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthSlider.value = maxHealth;
    }
    void Update()
    {
        LookAtPlayer();
        FlipHitPoint();
        healthSlider.value = currentHealth;
    }
    public void LookAtPlayer()
    {
        if (isAttacking || !isMovementAllowed || isResting)
            return;
        if (transform.position.x > player.position.x)
            mySpriteRenderer.flipX = false;
        else if (transform.position.x < player.position.x)
            mySpriteRenderer.flipX = true;
    }
    public void MiniBossAttack()
    {
        Collider2D[] playerCollider = Physics2D.OverlapCircleAll
        (hitPoint.position, hitRange, playerLayer);
        foreach (Collider2D player in playerCollider)
            player.GetComponent<PlayerCombat>().TakeDamage(attackDamage);
    }
    void OnDrawGizmosSelected()
    {
        if (hitPoint == null)
            return;
        Gizmos.DrawWireSphere(hitPoint.position, hitRange);
    }
    void FlipHitPoint()
    {
        if (mySpriteRenderer.flipX)
        {
            hitPoint.position = new Vector2
        (transform.position.x + Mathf.Abs(distanceBetweenMiniBossAndHitPoint),
        hitPoint.position.y);
        }
        else
        {
            hitPoint.position = new Vector2
        (transform.position.x - Mathf.Abs(distanceBetweenMiniBossAndHitPoint),
        hitPoint.position.y);
        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("AHHHHHHHHHH");
        myAnimator.SetTrigger("isHit");
        if (currentHealth <= 0)
            Die();
    }
    void Die()
    {
        myAnimator.SetTrigger("isDead");
        MiniBossHealthBar.SetActive(false);
        StartCoroutine(DestroyMiniBoss());
    }
    IEnumerator DestroyMiniBoss()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        Destroy(invisibleWall);
        cutsceneTrigger.gameObject.SetActive(true);
    }
    // public void Rest()
    // {
    //     StartCoroutine("WakeUp");
    // }
    // IEnumerator WakeUp()
    // {
    //     yield return new WaitForSeconds(2f);
    //     myAnimator.SetBool("isResting", false);
    // }
    public void CastSpell()
    {
        Instantiate(spell, spellGenerationPoint.position, transform.rotation);
    }
    public void SpellCastingCounter()
    {
        StartCoroutine("SpellTimer");
    }
    IEnumerator SpellTimer()
    {
        yield return new WaitForSeconds(5f);
        spellCastingAllowed = true;
    }
    public void WakeUp()
    {
        StartCoroutine("WakeUpTimer");
    }
    IEnumerator WakeUpTimer()
    {
        yield return new WaitForSeconds(2f);
        myAnimator.SetBool("isResting", false);
    }
}

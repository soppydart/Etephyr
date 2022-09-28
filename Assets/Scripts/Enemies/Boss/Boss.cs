using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    public int currentHealth;
    [SerializeField] Transform player;
    [SerializeField] Transform[] hitPoints;
    [SerializeField] float[] hitRanges;
    // [SerializeField] int attackDamage = 50;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] GameObject spell1;
    [SerializeField] GameObject spell2;
    [SerializeField] Transform spellGenerationPoint;
    [SerializeField] float spellCastTime = 0.5f;
    Animator myAnimator;
    float[] distanceBetweenBossAndHitPoints = new float[4];
    float distanceBetweenBossAndSpellCastingPoint;
    [SerializeField] int[] attackDamages;
    SpriteRenderer mySpriteRenderer;
    public bool isAttacking = false;
    public bool isMovementAllowed = true;
    public float spell1Counter = 5f;
    public float spell2Counter = 5f;
    public float healCounter = 0f;
    [SerializeField] Animator cameraAnimator;
    [SerializeField] GameObject BossMonologueCanvas;
    [SerializeField] GameObject BossMonologueDialogue1;
    [SerializeField] GameObject BossMonologueDialogue2;
    [SerializeField] GameObject BossMonologueDialogue3;
    [SerializeField] Animator MasterCanvasAnimator;
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myAnimator = GetComponent<Animator>();
        currentHealth = maxHealth;
        for (int i = 0; i < 4; i++)
            distanceBetweenBossAndHitPoints[i] = transform.position.x - hitPoints[i].position.x;
        distanceBetweenBossAndSpellCastingPoint = transform.position.x - spellGenerationPoint.position.x;
        healthSlider.value = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();
        DecreaseSpell1Counter();
        DecreaseSpell2Counter();
        DecreaseHealCounter();
        Flip();
        if (stopPlayerMovement)
            FindObjectOfType<PlayerMovement>().GetComponent<PlayerMovement>().StopMoving();
        healthSlider.value = currentHealth;
    }
    void LookAtPlayer()
    {
        if (isAttacking || !isMovementAllowed)
            return;
        if (transform.position.x > player.position.x)
            mySpriteRenderer.flipX = false;
        else if (transform.position.x < player.position.x)
            mySpriteRenderer.flipX = true;
    }
    void OnDrawGizmosSelected()
    {
        for (int i = 0; i < 4; i++)
        {
            if (hitPoints[i] == null)
                return;
            Gizmos.DrawWireSphere(hitPoints[i].position, hitRanges[i]);
        }
    }
    public void CastSpell1()
    {
        StartCoroutine(Spell1());
    }
    IEnumerator Spell1()
    {
        yield return new WaitForSeconds(spellCastTime);
        Instantiate(spell1, spellGenerationPoint.position, transform.rotation);
    }
    public void CastSpell2()
    {
        StartCoroutine(Spell2());
    }
    IEnumerator Spell2()
    {
        yield return new WaitForSeconds(spellCastTime);
        Instantiate(spell2, spellGenerationPoint.position, transform.rotation);
    }
    public void Attack1()
    {
        Collider2D[] playerCollider = Physics2D.OverlapCircleAll
        (hitPoints[0].position, hitRanges[0], playerLayer);
        foreach (Collider2D player in playerCollider)
            if (player.GetType() == typeof(CapsuleCollider2D))
                player.GetComponent<PlayerCombat>().TakeDamage(attackDamages[0]);
    }
    public void Attack2()
    {
        Collider2D[] playerCollider = Physics2D.OverlapCircleAll
        (hitPoints[1].position, hitRanges[1], playerLayer);
        foreach (Collider2D player in playerCollider)
            if (player.GetType() == typeof(CapsuleCollider2D))
                player.GetComponent<PlayerCombat>().TakeDamage(attackDamages[1]);
    }
    public void Attack3()
    {
        Collider2D[] playerCollider = Physics2D.OverlapCircleAll
        (hitPoints[2].position, hitRanges[2], playerLayer);
        foreach (Collider2D player in playerCollider)
            if (player.GetType() == typeof(CapsuleCollider2D))
                player.GetComponent<PlayerCombat>().TakeDamage(attackDamages[2]);
    }
    public void SpecialAttack()
    {
        Collider2D[] playerCollider = Physics2D.OverlapCircleAll
        (hitPoints[3].position, hitRanges[3], playerLayer);
        foreach (Collider2D player in playerCollider)
            if (player.GetType() == typeof(CapsuleCollider2D))
                player.GetComponent<PlayerCombat>().TakeDamage(attackDamages[3]);
    }
    void DecreaseSpell1Counter()
    {
        spell1Counter -= Time.deltaTime;
    }
    void DecreaseSpell2Counter()
    {
        spell2Counter -= Time.deltaTime;
    }
    void DecreaseHealCounter()
    {
        healCounter -= Time.deltaTime;
    }
    public void Heal()
    {
        Debug.Log("Healed");
        currentHealth += 30;
        healCounter = 100f;
    }
    void Flip()
    {
        int direction = (!mySpriteRenderer.flipX) ? -1 : 1;
        for (int i = 0; i < 4; i++)
            hitPoints[i].position = new Vector2
            (transform.position.x + direction * Mathf.Abs(distanceBetweenBossAndHitPoints[i]), hitPoints[i].position.y);
        spellGenerationPoint.position = new Vector3
        (transform.position.x + direction * Mathf.Abs(distanceBetweenBossAndSpellCastingPoint)
        , spellGenerationPoint.position.y, 60f);
    }
    bool flag = false;
    [SerializeField] GameObject BossHealthBar;
    public void TakeDamage(int damage)
    {
        if (isDefending)
            return;
        currentHealth -= damage;
        Debug.Log("AHHHHHHHHHH");
        myAnimator.SetTrigger("isHit");
        if (currentHealth <= 0)
        {
            if (!flag)
            {
                stopPlayerMovement = true;
                BossHealthBar.SetActive(false);
                currentHealth = maxHealth;
                myAnimator.SetBool("isTransitioning", true);
                cameraAnimator.SetTrigger("Phase1Ended");
                StartCoroutine(DisplayBossDialogue2());
                flag = true;
                FindObjectOfType<AudioManager>().StartBossPhaseTransition();
            }
            else
                Die();
        }
    }
    IEnumerator DisplayBossDialogue2()
    {
        yield return new WaitForSeconds(2f);
        BossMonologueCanvas.SetActive(true);
        yield return new WaitForSeconds(5f);
        BossMonologueDialogue1.SetActive(true);
        yield return new WaitForSeconds(5f);
        BossMonologueDialogue2.SetActive(true);
        yield return new WaitForSeconds(5f);
        BossMonologueDialogue3.SetActive(true);
        yield return new WaitForSeconds(5f);
        MasterCanvasAnimator.SetTrigger("FadeOut");
        myAnimator.SetBool("isTransitioning", false);
        stopPlayerMovement = false;
        FindObjectOfType<PlayerMovement>().GetComponent<PlayerMovement>().StartMoving();
        BossHealthBar.SetActive(true);
        yield return new WaitForSeconds(3f);
        StartPhase2();
    }
    public void SkipDialogue2()
    {
        MasterCanvasAnimator.SetTrigger("FadeOut");
        myAnimator.SetBool("isTransitioning", false);
        stopPlayerMovement = false;
        FindObjectOfType<PlayerMovement>().GetComponent<PlayerMovement>().StartMoving();
        BossHealthBar.SetActive(true);
        StartCoroutine(BossResumeAttacks());
    }
    IEnumerator BossResumeAttacks()
    {
        yield return new WaitForSeconds(3f);
        StartPhase2();
    }
    bool stopPlayerMovement = false;
    void Die()
    {
        myAnimator.SetTrigger("Die");
        isMovementAllowed = false;
        FindObjectOfType<BossCutsceneController>().GetComponent<BossCutsceneController>().Letter();
        stopPlayerMovement = true;
        FindObjectOfType<AudioManager>().StopSound("Boss Music Loop");
        Destroy(gameObject, 5f);
    }
    public void StandUp()
    {
        myAnimator.SetBool("isTransitioning", false);
    }
    public void StartPhase2()
    {
        FindObjectOfType<AudioManager>().EndBossPhaseTransition();
        myAnimator.SetTrigger("StartPhase2");
        myAnimator.SetBool("isPhase2", true);
        cameraAnimator.SetTrigger("ResumeFight");
    }
    [SerializeField] Slider healthSlider;
    public bool isDefending = false;
}

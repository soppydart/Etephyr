using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monk : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    public int currentHealth;
    public bool isAttacking = false;
    SpriteRenderer mySpriteRenderer;
    [SerializeField] Transform player;
    [SerializeField] Transform[] hitPoints;
    [SerializeField] float jumpPower = 10f;
    Rigidbody2D myRigidbody2D;
    public bool isMovementAllowed = true;
    Animator myAnimator;
    // Start is called before the first frame update
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();
        FlipMonk();
        if (!isMovementAllowed)
            myRigidbody2D.velocity = new Vector2(0f, myRigidbody2D.velocity.y);
    }
    void LookAtPlayer()
    {
        if (isAttacking)
            return;
        int factor = 1;
        if (transform.position.x > player.position.x)
            factor = -1;
        else if (transform.position.x < player.position.x)
            factor = 1;
        transform.localScale = new Vector2(factor * 4.5f, 4.5f);
    }
    void FlipMonk()
    {
        // if (myRigidbody2D.velocity.x > 0)
        //     transform.localScale = new Vector2(4.5f, 4.5f);
        // else if (myRigidbody2D.velocity.x < 0)
        //     transform.localScale = new Vector2(-4.5f, 4.5f);
    }
    void OnDrawGizmosSelected()
    {
        for (int i = 0; i < 2; i++)
        {
            if (hitPoints[i] == null)
                return;
            Gizmos.DrawWireSphere(hitPoints[i].position, 1f);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "MonkJumper")
        {
            myRigidbody2D.velocity = new Vector2(0f, jumpPower);
            Debug.Log("Whooshhh");
        }
    }
    [SerializeField] LayerMask playerLayer;
    public void Attack1()
    {
        Collider2D[] playerColliders = Physics2D.OverlapCircleAll(hitPoints[0].position, 0.25f, playerLayer);
        foreach (Collider2D player in playerColliders)
            player.GetComponent<PlayerCombat>().TakeDamage(5);
    }
    public void Attack2()
    {
        Collider2D[] playerColliders = Physics2D.OverlapCircleAll(hitPoints[0].position, 0.25f, playerLayer);
        foreach (Collider2D player in playerColliders)
            player.GetComponent<PlayerCombat>().TakeDamage(10);
    }
    public void Attack3()
    {
        Collider2D[] playerColliders = Physics2D.OverlapCircleAll(hitPoints[1].position, 1f, playerLayer);
        foreach (Collider2D player in playerColliders)
            player.GetComponent<PlayerCombat>().TakeDamage(15);
    }
    public void TakeDamage()
    {
        myAnimator.SetTrigger("Hit");
        Debug.Log("oge rrat");
    }
}

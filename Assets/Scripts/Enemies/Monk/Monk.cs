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
    public bool isJumping = false;
    // Start is called before the first frame update
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();
        FlipMonk();
        Jump();
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
    public void Jump()
    {
        myRigidbody2D.AddForce(Vector2.up * jumpPower);
    }
}

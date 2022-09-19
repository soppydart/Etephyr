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
    Rigidbody2D myRigidbody2D;
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
    }
    void LookAtPlayer()
    {
        if (isAttacking)
            return;
        if (transform.position.x > player.position.x)
            transform.localScale = new Vector2(-4.5f, 4.5f);
        else if (transform.position.x < player.position.x)
            transform.localScale = new Vector2(4.5f, 4.5f);
    }
    void FlipMonk()
    {
        if (myRigidbody2D.velocity.x > 0)
            transform.localScale = new Vector2(4.5f, 4.5f);
        else if (myRigidbody2D.velocity.x < 0)
            transform.localScale = new Vector2(-4.5f, 4.5f);
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
}

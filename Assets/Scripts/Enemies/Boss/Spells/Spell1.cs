using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell1 : MonoBehaviour
{
    [SerializeField] float spellVelocity = 5f;
    SpriteRenderer bossSpriteRenderer;
    SpriteRenderer spellSpriteRenderer;
    Rigidbody2D myRigidbody;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        bossSpriteRenderer = FindObjectOfType<Boss>().GetComponent<SpriteRenderer>();
        spellSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        CastSpell();
    }
    bool flag = false;
    void CastSpell()
    {
        if (flag)
            return;
        if (!bossSpriteRenderer.flipX)
        {
            myRigidbody.velocity = new Vector2(spellVelocity, 0f);
            spellSpriteRenderer.flipX = false;
        }
        else
        {
            myRigidbody.velocity = new Vector2(spellVelocity * -1, 0f);
            spellSpriteRenderer.flipX = true;
        }
        flag = true;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        myRigidbody.velocity = new Vector2(0f, 0f);
        GetComponent<Animator>().SetTrigger("DestroySpell");
    }
    public void DestroySpell()
    {
        Destroy(gameObject);
    }
}

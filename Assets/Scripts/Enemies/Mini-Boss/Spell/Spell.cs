using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    bool spellDamageAllowed = false;
    [SerializeField] int spellDamage = 10;
    [SerializeField] Transform hitPoint;
    [SerializeField] float hitRange = 2f;
    [SerializeField] LayerMask playerLayer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void DestroySpell()
    {
        Destroy(gameObject);
    }
    public void DamagePlayer()
    {
        spellDamageAllowed = true;
        Debug.Log("Damaged");
        Collider2D[] playerCollider = Physics2D.OverlapCircleAll
        (hitPoint.position, hitRange, playerLayer);
        foreach (Collider2D player in playerCollider)
            player.GetComponent<PlayerCombat>().TakeDamage(spellDamage);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && spellDamageAllowed)
            other.GetComponent<PlayerCombat>().TakeDamage(spellDamage);
    }
    void OnDrawGizmosSelected()
    {
        if (hitPoint == null)
            return;
        Gizmos.DrawWireSphere(hitPoint.position, hitRange);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleJumpPowerup : MonoBehaviour
{
    Color originalColor;
    // [SerializeField] GameObject pickUpEffect;
    bool pickUpAllowed = true;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player" || !pickUpAllowed)
            return;
        pickUpAllowed = false;
        Debug.Log("Pain");
        originalColor = other.GetComponent<SpriteRenderer>().color;
        // Instantiate(pickUpEffect, transform.position, transform.rotation);
        StartCoroutine(Pickup(other));
    }
    IEnumerator Pickup(Collider2D player)
    {
        player.GetComponent<PlayerMovement>().isDashReallyAllowed = false;
        player.GetComponent<SpriteRenderer>().color = new Color(255f, 0f, 0f);
        yield return new WaitForSeconds(5f);
        Debug.Log("No Pain");
        pickUpAllowed = true;
        player.GetComponent<SpriteRenderer>().color = originalColor;
        FindObjectOfType<PlayerMovement>().GetComponent<PlayerMovement>().isDashReallyAllowed = true;
    }
}

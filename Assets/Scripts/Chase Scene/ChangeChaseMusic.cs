using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeChaseMusic : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player")
            return;
        FindObjectOfType<AudioManager>().StopSound("Chase Music");
        FindObjectOfType<AudioManager>().PlaySound("Tutorial Music");
    }
}

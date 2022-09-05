using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatCameraController : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform miniBoss;
    Animator myAnimator;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.x > miniBoss.position.x)
            myAnimator.SetBool("isFacingRight", false);
        else if (player.position.x < miniBoss.position.x)
            myAnimator.SetBool("isFacingRight", true);
    }
}

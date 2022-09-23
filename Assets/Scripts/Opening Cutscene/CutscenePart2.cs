using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutscenePart2 : MonoBehaviour
{
    [SerializeField] Rigidbody2D player;
    [SerializeField] Animator playerAnimator;
    void Start()
    {
        playerAnimator.SetBool("isRunning", true);
    }

    public bool playCutscene2 = true;
    void Update()
    {
        if (playCutscene2)
        {
            player.velocity = new Vector2(5f, 0f);
            playerAnimator.SetBool("isRunning", true);
        }
    }
}

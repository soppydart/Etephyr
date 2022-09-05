using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSurf : StateMachineBehaviour
{
    Transform player;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float attackRange = 2f;
    Rigidbody2D myRigidbody2D;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        myRigidbody2D = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetComponent<Boss>().healCounter <= 0 && animator.GetComponent<Boss>().currentHealth <= 50)
            animator.SetTrigger("Heal");
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>().isAttacking)
            animator.SetTrigger("Defend");
        if (Vector2.Distance(player.position, myRigidbody2D.position) <= attackRange)
        {
            int randomNumber = Random.Range(1, 3);
            Debug.Log(randomNumber);
            if (randomNumber == 1)
                animator.SetTrigger("Attack3");
            else
                animator.SetTrigger("SpecialAttack");
        }
        else if (animator.GetComponent<Boss>().spell2Counter <= 0)
            animator.SetTrigger("Cast");
        else
        {
            Vector2 target = new Vector2(player.position.x, myRigidbody2D.position.y);
            Vector2 newPosition = Vector2.MoveTowards(myRigidbody2D.position, target, moveSpeed * Time.fixedDeltaTime);
            myRigidbody2D.MovePosition(newPosition);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack3");
        animator.ResetTrigger("SpecialAttack");
        animator.ResetTrigger("Defend");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWalking : StateMachineBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float attackRange = 3f;
    Transform player;
    Rigidbody2D myRigidbody;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        myRigidbody = animator.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector2.Distance(player.position, myRigidbody.position) <= attackRange)
        {
            animator.SetTrigger("Attack");
        }
        else
        {
            Vector2 target = new Vector2(player.position.x, myRigidbody.position.y);
            Vector2 newPosition = Vector2.MoveTowards(myRigidbody.position, target, moveSpeed * Time.fixedDeltaTime);
            myRigidbody.MovePosition(newPosition);
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }
}

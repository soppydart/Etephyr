using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkRun : StateMachineBehaviour
{
    Transform player;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float attackRange = 2f;
    Rigidbody2D myRigidbody2D;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        myRigidbody2D = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetComponent<Monk>().isAttacking)
            return;
        if (Vector2.Distance(player.position, myRigidbody2D.position) <= attackRange)
            animator.SetTrigger("Attack");
        else
        {
            // Vector2 target = new Vector2(player.position.x, myRigidbody2D.position.y);
            // Vector2 newPosition = Vector2.MoveTowards(myRigidbody2D.position, target, moveSpeed * Time.fixedDeltaTime);
            // myRigidbody2D.MovePosition(newPosition);
            myRigidbody2D.velocity = new Vector2(Mathf.Sign(player.position.x - myRigidbody2D.position.x) * moveSpeed, myRigidbody2D.velocity.y);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}

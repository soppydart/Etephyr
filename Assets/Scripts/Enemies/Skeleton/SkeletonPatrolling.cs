using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonPatrolling : StateMachineBehaviour
{
    Rigidbody2D myRigidbody;
    [SerializeField] float patrolSpeed = 1f;
    SpriteRenderer mySpriteRenderer;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        myRigidbody = animator.GetComponent<Rigidbody2D>();
        mySpriteRenderer = animator.GetComponent<SpriteRenderer>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        myRigidbody.velocity = new Vector2
        (patrolSpeed * animator.GetComponent<Skeleton>().skeletonMoveDirection, 0f);
        mySpriteRenderer.flipX = (animator.GetComponent<Skeleton>().skeletonMoveDirection == 1) ? false : true;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossRun : StateMachineBehaviour
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
        if (!animator.GetComponent<MiniBoss>().isMovementAllowed)
            return;
        if (animator.GetComponent<MiniBoss>().comboHits > 0)
        {
            if (Vector2.Distance(player.position, myRigidbody2D.position) <= attackRange)
                animator.SetTrigger("Attack");
            else if (animator.GetComponent<MiniBoss>().spellCastingAllowed)
            {
                animator.GetComponent<MiniBoss>().spellCastingAllowed = false;
                animator.SetTrigger("Cast");
                animator.GetComponent<MiniBoss>().SpellCastingCounter();
            }
            else
            {
                Vector2 target = new Vector2(player.position.x, myRigidbody2D.position.y);
                Vector2 newPosition = Vector2.MoveTowards(myRigidbody2D.position, target, moveSpeed * Time.fixedDeltaTime);
                myRigidbody2D.MovePosition(newPosition);
            }
        }
        else
        {
            // animator.GetComponent<MiniBoss>().comboHits = Random.Range(6, 10);
            animator.SetBool("isResting", true);
            if (animator.GetComponent<MiniBoss>().comboHits < 0)
                animator.GetComponent<MiniBoss>().isResting = true;
            animator.GetComponent<MiniBoss>().WakeUp();
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }
}

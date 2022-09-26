using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneTrigger : MonoBehaviour
{
    [SerializeField] Animator myAnimator;
    [SerializeField] SpriteRenderer miniBoss;
    [SerializeField] GameObject skipCanvas;
    bool flag = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !flag)
        {
            myAnimator.SetTrigger("startCutscene");
            skipCanvas.SetActive(true);
            StartCoroutine("FlipMiniBoss");
            FindObjectOfType<CutsceneControllerMiniBoss>().isInCutscene = true;
            other.GetComponent<PlayerMovement>().StopMoving();
            flag = true;
        }
    }
    IEnumerator FlipMiniBoss()
    {
        yield return new WaitForSeconds(2f);
        miniBoss.flipX = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftCamera : MonoBehaviour
{
    [SerializeField] Animator cameraAnimator;
    bool flag = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" & !flag)
        {
            flag = true;
            cameraAnimator.SetTrigger("NewCamera");
        }
    }
}

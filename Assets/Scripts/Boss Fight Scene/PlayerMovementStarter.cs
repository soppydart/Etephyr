using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementStarter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<PlayerMovement>().GetComponent<PlayerMovement>().StartMoving();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

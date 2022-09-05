using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementCOntroller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<PlayerMovement>().StopMoving();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

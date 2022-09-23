using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] float ghostDelay = 1f;
    [SerializeField] GameObject ghost;
    float ghostDelaySeconds;
    public bool makeGhost = false;
    void Start()
    {
        ghostDelaySeconds = ghostDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (!makeGhost)
            return;
        if (ghostDelaySeconds > 0f)
        {
            ghostDelaySeconds -= Time.deltaTime;
        }
        else
        {
            GameObject currentGhost = Instantiate(ghost, transform.position, transform.rotation);
            currentGhost.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
            currentGhost.GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color;
            if (GetComponent<SpriteRenderer>().flipX)
                currentGhost.GetComponent<SpriteRenderer>().flipX = true;
            ghostDelaySeconds = ghostDelay;
            Destroy(currentGhost, 2f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingPlatforms : MonoBehaviour
{
    // [SerializeField] float breakTime = 2f;
    // CompositeCollider2D myCompositeCollider;
    // Rigidbody2D myRigidbody;
    // float breakTimeCounter;
    // [SerializeField] float repositionTime = 5f;
    // // [SerializeField] float shakeSpeed = 1f;
    // [SerializeField] float shakeAmount = 1f;
    // Vector2 initialPosition;
    // bool platformMovingBack = false;
    // bool shake = false;
    // // bool isBroken = false;
    // bool decreaseCounter = false;
    // void Start()
    // {
    //     myCompositeCollider = GetComponent<CompositeCollider2D>();
    //     myRigidbody = GetComponent<Rigidbody2D>();
    //     initialPosition = transform.position;
    // }
    // void Update()
    // {
    //     if (myCompositeCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
    //     {
    //         // breakTimeCounter -= Time.deltaTime;
    //         decreaseCounter = true;
    //     }
    //     else
    //     {
    //         // if (!isBroken)
    //         //     transform.position = initialPosition;
    //     }
    //     if (breakTimeCounter < 0)
    //         BreakPlatform();
    //     ReturnPlatform();
    //     ShakePlatform();
    //     DecreaseCounter();
    // }
    // void DecreaseCounter()
    // {
    //     if (!decreaseCounter)
    //         return;
    //     shake = true;
    //     breakTimeCounter -= Time.deltaTime;
    // }
    // void BreakPlatform()
    // {
    //     shake = false;
    //     myRigidbody.isKinematic = false;
    //     StartCoroutine(RepositionPlatform());
    //     // isBroken = true;
    // }
    // IEnumerator RepositionPlatform()
    // {
    //     yield return new WaitForSeconds(repositionTime);
    //     myRigidbody.isKinematic = true;
    //     myRigidbody.velocity = Vector2.zero;
    //     platformMovingBack = true;
    // }
    // void ReturnPlatform()
    // {
    //     if (platformMovingBack)
    //         transform.position = Vector2.MoveTowards(transform.position, initialPosition, 20f * Time.deltaTime);
    //     if (platformMovingBack && transform.position.y == initialPosition.y)
    //     {
    //         platformMovingBack = false;
    //         // isBroken = false;
    //         decreaseCounter = false;
    //         breakTimeCounter = breakTime;
    //     }
    // }
    // void ShakePlatform()
    // {
    //     if (!shake)
    //         return;
    //     Vector3 newPosition = Random.insideUnitSphere * (Time.deltaTime * shakeAmount);
    //     transform.position = new Vector3(transform.position.x, newPosition.y, newPosition.z);
    // }

    [SerializeField] float breakTime = 2f;
    CompositeCollider2D myCompositeCollider;
    Rigidbody2D myRigidbody;
    float breakTimeCounter;
    [SerializeField] float repositionTime = 5f;
    [SerializeField] float shakeAmount = 1f;
    Vector2 initialPosition;
    bool platformMovingBack = false;
    bool hasTouchedPlatform = false;
    bool shake = false;
    void Start()
    {
        myCompositeCollider = GetComponent<CompositeCollider2D>();
        myRigidbody = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
        breakTimeCounter = breakTime;
    }
    void Update()
    {
        if (myCompositeCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            hasTouchedPlatform = true;
            shake = true;
        }
        DecreaseCounter();
        BreakPlatform();
        ShakePlatform();
        BreakPlatform();
        RepositionPlatform();
        if (shake && breakTimeCounter == breakTime)
            shake = false;
    }
    void ShakePlatform()
    {
        if (!shake)
            return;
        Vector3 newPosition = Random.insideUnitSphere * (Time.deltaTime * shakeAmount);
        transform.position = new Vector3(newPosition.x, transform.position.y, transform.position.z);
    }
    void DecreaseCounter()
    {
        if (!hasTouchedPlatform)
            return;
        breakTimeCounter -= Time.deltaTime;
    }
    void BreakPlatform()
    {
        if (breakTimeCounter < 0)
        {
            shake = false;
            myRigidbody.gravityScale = 2f;
            StartCoroutine(RaisePlatform());
        }
    }
    IEnumerator RaisePlatform()
    {
        yield return new WaitForSeconds(repositionTime);
        myRigidbody.gravityScale = 0;
        myRigidbody.velocity = Vector2.zero;
        platformMovingBack = true;
    }
    void RepositionPlatform()
    {
        if (platformMovingBack)
            transform.position = Vector2.MoveTowards(transform.position, initialPosition, 5f * Time.deltaTime);
        if (platformMovingBack && transform.position.y == initialPosition.y)
        {
            platformMovingBack = false;
            hasTouchedPlatform = false;
            breakTimeCounter = breakTime;
        }
    }
}

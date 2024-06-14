using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpHigh = 10f;
    Vector2 moveInput;
    Rigidbody2D myRi;
    Animator myA;
    CapsuleCollider2D myCap;
    void Start()
    {
        myRi = GetComponent<Rigidbody2D>();
        myA = GetComponent<Animator>();
        myCap = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
    }
        
    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRi.velocity.y);
        myRi.velocity = playerVelocity;

    }
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    void FlipSprite()
    {
        bool hasHori = Mathf.Abs(myRi.velocity.x) > Mathf.Epsilon;
        if (hasHori)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRi.velocity.x), 1f);
        }
    }
    void OnJump(InputValue value)
    {
       if(!myCap.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        if(value.isPressed)
        {
          myRi.velocity += new Vector2(0f,jumpHigh);
        }
    }
}

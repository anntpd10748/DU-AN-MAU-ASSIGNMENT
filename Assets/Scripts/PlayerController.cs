using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpHigh = 10f;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;

    [SerializeField] int playerLives = 3;
    [SerializeField] int score = 0;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoresText;

    Vector2 moveInput;
    Rigidbody2D myRi;
    Animator myA;
    CapsuleCollider2D myCap;
    bool isAlive = true;

    void Start()
    {
        livesText.text = playerLives.ToString();
        scoresText.text = score.ToString();

        myRi = GetComponent<Rigidbody2D>();
        myA = GetComponent<Animator>();
        myCap = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
        Die();
        Died();
    }
        
    void Run()
    {

        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRi.velocity.y);
        myRi.velocity = playerVelocity;

    }
    void OnMove(InputValue value)
    {
        if(!isAlive) { return; }
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
        if (!isAlive) { return; }
        if(!myCap.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        if(value.isPressed)
        {
          myRi.velocity += new Vector2(0f,jumpHigh);
        }
    }
    void Die()
    {
        
        if (myCap.IsTouchingLayers(LayerMask.GetMask("Enemy")) && playerLives == 0)
        {
            isAlive = false;
            myA.SetTrigger("Dying");
        }
        if (myCap.IsTouchingLayers(LayerMask.GetMask("Trap")) && playerLives <= 0)
        {
            isAlive = false;
            myA.SetTrigger("Dying");
        }

    }
    void OnFire(InputValue value)
    {
        if (!isAlive) { return; }
        Instantiate(bullet, gun.position, transform.rotation);
    }
    public void AddScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoresText.text = score.ToString();
    }
    void takeLife()
    {
        playerLives--;
        livesText.text = playerLives.ToString();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            takeLife();
        }
        if (collision.gameObject.tag.Equals("Trap"))
        {
            takeLife();
        }
    }
    public void LoadSceneByName(int sceneIndex)
    {
        SceneManager.LoadScene( sceneIndex);
    }
    public void Died()
    {
        if(isAlive == false || playerLives <= 0)
        {
            LoadSceneByName(4);
            Debug.Log("died");
        }
    }
}

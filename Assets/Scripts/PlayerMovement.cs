using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Physics")]
    public float speed;
    public float JumpForce = 1;
    public float DodgeDistance;

    [Header("Animator")]
    public Animator animator;

    private Rigidbody2D _rigidbody;
    
    [Header("What is ground")]
    public float checkradius;
    public Transform FeetPos;
    public LayerMask WhatisGround;


    [Header("Health")]
    public GameObject[] HealthImage;
    public Sprite[] HealthSprite;

    
    
    
    
    private int damage = 0;
    private float Timer = 0.0f;
    private bool Isdodging = false;

    private bool IsGrounded;
    private float jumptimecounter;
    public float jumptime;
    private bool isJumping;




    //private int MaxHealth = 3; //Will implement if i add a item to increase max health
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();   

    }

    // Update is called once per frame
    void Update()
    {
        
        Movement();
        Jump();
      
      
    }

    private void Movement()
    {
        float movement = Sidetoside();

        Dodging(movement);
        

    }

    private float Sidetoside()
    {
        //Input on horizontal axis
        var movement = Input.GetAxis("Horizontal");
        //Moves player side to side
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * speed;
        //Used for animation and records the speed of the player while always keeping the value positive
        animator.SetFloat("Speed", Mathf.Abs(movement));


        //Fliping
        if (movement > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (movement < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        return movement;

       


    }

   

    private void Dodging(float movement)
    {
        if (Input.GetButtonDown("Dodge"))
        {
            animator.SetBool(("IsDodging"), true);
            Isdodging = true;
            _rigidbody.velocity = new Vector2((movement * speed) * DodgeDistance, _rigidbody.velocity.y);



        }
        if (Isdodging == true)
        {
            Timer += Time.deltaTime;
            if (Timer > 1.4f)
            {
                animator.SetBool(("IsDodging"), false);
                Isdodging = false;
                Timer = 0.0f;
            }
            else
            {
                Timer += Time.deltaTime;
            }
        }
    }

    private void Jump()
    {


        //Checks if player is touching ground through empty game object thats given a radius 
        IsGrounded = Physics2D.OverlapCircle(FeetPos.position, checkradius, WhatisGround);


        if (Input.GetButtonDown("Jump") && Mathf.Abs(_rigidbody.velocity.y) < 0.001f && IsGrounded == true)
        {
            _rigidbody.velocity = Vector2.up * JumpForce;
            jumptimecounter = jumptime;
            isJumping = true;


        }
        if (Input.GetKey(KeyCode.Space))
        {
            if (jumptimecounter > 0 && isJumping == true)
            {
                _rigidbody.velocity = Vector2.up * JumpForce;
                jumptimecounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }


        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }

        JumpAnimator();

    }

    private void JumpAnimator()
    {
        if (IsGrounded == true)
        {
            animator.SetBool("IsJumping", false);

        }
        else
        {
            animator.SetBool("IsJumping", true);

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Compares the tag of another object and if it matches the player will take damage
        if (other.gameObject.CompareTag("Bullet"))
        {
          
            damage = damage + 1;
            HealthImage[damage].gameObject.GetComponent<Image>().sprite = HealthSprite[1];
           
        }
       
    }
}

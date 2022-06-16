using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy1 : MonoBehaviour
{
    [Header("Health")]
    public int Health = 100;

    [Header("Pathfinding")]
    public Transform target;
    public float ActivateDistance = 50f;
    public float PathUpdateSecounds = 0.5f;

    [Header("Physics")]
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public float JumpNodeHeightRequirement = 0.8f;
    public float JumpModifier = 0.3f;
    public float JumpCheckOffset = 0.1f;
    public float JumpForce = 1f;

    [Header("IsGrounded")]
    public float checkradius;
    public Transform FeetPos;
    public LayerMask WhatisGround;

    [Header("Custom Behavior")]
    public bool followEnabled = true;
    public bool jumpEnabled = true;
    public bool directionLookEnabled = true;
    public Animator animator;
    public GameObject DeathEffect;

    private float Speed;
    private Path path;
    private int CurrentWaypoint = 0;
    bool isGrounded = false;
    Seeker seeker;
    Rigidbody2D rb;
    



    public void TakeDamage(int damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Instantiate(DeathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, PathUpdateSecounds); //Keeps repeating same script every "PathUpdateSecounds" this reduces stress on system

    }


    private void FixedUpdate()
    {
        //when player is in distance the path finding will be enabled
        if (TargetInDistance() && followEnabled)
        {
            PathFollow();
        }
    }

    private void UpdatePath()
    {
        if (followEnabled && TargetInDistance() && seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }
    private void PathFollow()
    {
        if (path == null)
        {
            return;
        }
        //Reached end of path
        if (CurrentWaypoint >= path.vectorPath.Count)
        {
            return;
        }
       
        
       
       

        //Direction Calculation
        Vector2 direction = ((Vector2)path.vectorPath[CurrentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        //jump
        if (jumpEnabled && isGrounded == true)
        {
            if(direction.y > JumpNodeHeightRequirement)
            {
                rb.velocity = Vector2.up * JumpForce;
                
            }
           
           
        }
        // Movement
        rb.AddForce(force);

        //Next Waypoint
        float distance = Vector2.Distance(rb.position, path.vectorPath[CurrentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            CurrentWaypoint++;
        }


        // Direction Graphics Handling
        if (directionLookEnabled)
        {
            if (rb.velocity.x > 0.05f)
            {
                transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (rb.velocity.x < -0.05f)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }

    }

    private bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, target.transform.position) < ActivateDistance;
    }
    private void OnPathComplete(Path P)
    {
        if (!P.error)
        {
            path = P;
            CurrentWaypoint = 0;
        }
    }
    // Update is called once per frame
    void Update()
    {
        //used for the animation of the enemy
        Speed = rb.velocity.magnitude;
        animator.SetFloat("Speeds", Mathf.Abs(Speed));


        //checks if grounded
        isGrounded = Physics2D.OverlapCircle(FeetPos.position, checkradius, WhatisGround);

        //used for jump animation for animation
        if (isGrounded == true)
        {
            animator.SetBool("IsJumping", false);
        }
        else
        {
            animator.SetBool("IsJumping", true);
        }
    }
    
}

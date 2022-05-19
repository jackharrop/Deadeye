using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public float speed;
    public float JumpForce = 1;
    public float DodgeDistance;
    private Rigidbody2D _rigidbody;
    private bool CanDoubleJump;
    public GameObject Bullet;
    public GameObject[] HealthImage;
    public Sprite[] HealthSprite;
    private int damage = 0;
    private float Timer = 0.0f;
    private bool Isdodging = false;

    public bool IsGrounded;
    public LayerMask ground;
    public BoxCollider2D GroundTouchingBoxcollider;


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
        if(IsGrounded)
        {
            animator.SetBool("IsJumping" , false);
            
        }
        else
        {
            animator.SetBool("IsJumping", true);
            
        }
        IsGrounded = GroundTouchingBoxcollider.IsTouchingLayers(ground);
    }

    private void Movement()
    {
        float movement = Sidetoside();

        Dodging(movement);
        Fliping(movement);

    }

    private float Sidetoside()
    {
        var movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * speed;
        animator.SetFloat("Speed", Mathf.Abs(movement));
        return movement;
    }

    private void Fliping(float movement)
    {
        if (movement > 0)
        {
           
            gameObject.transform.localScale = new Vector3(4, 4, 4);
        }
        if (movement < 0)
        {
           
            gameObject.transform.localScale = new Vector3(-4, 4, 4);
        }
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
            if (Timer > 1.5f)
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






        if (Input.GetButtonDown("Jump") && Mathf.Abs(_rigidbody.velocity.y) < 0.001f)
        {
            _rigidbody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            CanDoubleJump = true;
        }
        else if (CanDoubleJump && Input.GetButtonDown("Jump"))
        {
            _rigidbody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            CanDoubleJump = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("Bullet"))
        {
          
            damage = damage + 1;
            HealthImage[damage].gameObject.GetComponent<Image>().sprite = HealthSprite[1];
            Bullet.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Heal")) 
        {
            HealthImage[damage].gameObject.GetComponent<Image>().sprite = HealthSprite[0];
            damage = damage - 1;
        }
        //if (other.gameObject.CompareTag("MoreHealth")) //Will implement if i add a item to increase max health
        //{

       // }
    }
}

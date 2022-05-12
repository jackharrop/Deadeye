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
    private int MaxHealth = 3; //Will implement if i add a item to increase max health
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
        var movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * speed;
        animator.SetFloat("Speed", Mathf.Abs(movement));

        if (Input.GetButtonDown("Dodge"))
        {
            _rigidbody.velocity = new Vector2((movement * speed) * DodgeDistance, _rigidbody.velocity.y);
        }
       
     
        
        
        if (movement > 0)
        {
            gameObject.transform.localScale = new Vector3(4 ,4,4 );
        }
        if (movement < 0)
        {
            gameObject.transform.localScale = new Vector3(-4, 4, 4);
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
        if (other.gameObject.CompareTag("MoreHealth")) //Will implement if i add a item to increase max health
        {

        }
    }
}

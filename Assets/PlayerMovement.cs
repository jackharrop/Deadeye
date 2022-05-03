using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    
    public float speed;
    public float JumpForce = 1;
    public float DodgeDistance;
    private Rigidbody2D _rigidbody;
    private bool CanDoubleJump;
    public GameObject Bullet;
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

        if (Input.GetButtonDown("Dodge"))
        {
            _rigidbody.velocity = new Vector2((movement * speed) * DodgeDistance, _rigidbody.velocity.y);
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
        if(other.gameObject.CompareTag("Bullet"))
        {
            Bullet.gameObject.SetActive(false);
        }
    }
}

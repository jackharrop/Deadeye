using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public float DestroyTimer;
    
    public Rigidbody2D rb;

    public GameObject DestroyEffect;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyProjectile", lifetime);
       

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
    void DestroyProjectile()
    {
        Instantiate(DestroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D hitinfo)
    {
        if(hitinfo.gameObject.CompareTag("Player"))
        {
            //This is done so the bullet does nothing when it comes into contact with player
        }
        else
        {

            Enemy1 enemy1 = hitinfo.GetComponent<Enemy1>();
            if (enemy1 != null)
            {
                enemy1.TakeDamage(40);
            }



            Debug.Log(hitinfo.name);
            Instantiate(DestroyEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
       
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float offset;
    public GameObject Projectile;
    public Transform Shotpoint;
    public GameObject PlayerRotation;
    public GameObject ShotpointFlip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float RotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, RotationZ + offset);

        //Vector3 localscale = Vector3.one;
        if (RotationZ > 90 || RotationZ <-90)
        {

            gameObject.GetComponent<SpriteRenderer>().flipY = true;
            ShotpointFlip.transform.position = new Vector3(0f, 3.571429f, 0f);
           // localscale.y = -0.07f;
            //localscale.x = -0.07f;

        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipY = false;
            ShotpointFlip.transform.localScale = new Vector3(0f, 3.571429f, 0f);
            //localscale.y = +0.07f;
            //localscale.x = +0.07f;

        }
        //transform.localScale = localscale;

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(Projectile, Shotpoint.position, transform.rotation );
        }

     

    }
}

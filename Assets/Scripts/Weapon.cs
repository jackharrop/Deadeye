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
    public bool CanShoot = true;
    public float DelayinSecounds;
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

        Vector3 localscale = Vector3.one;
        if (RotationZ > 90 || RotationZ <-90)
        {
            localscale.x = +0.07f;
            localscale.y = -0.07f;
            localscale.z = +0.07f;
            offset = 6;




        }
        else
        {
            localscale.x = +0.07f;
            localscale.y = +0.07f;
            localscale.z = +0.07f;
            offset = -6;
        }
        transform.localScale = localscale;

        if (Input.GetMouseButtonDown(0))
        {

            if(CanShoot == true)
            {
                Instantiate(Projectile, Shotpoint.position, transform.rotation);
                CanShoot = false;
                StartCoroutine(Shootdelay());
            }
           


            
        }

     

    }
    IEnumerator Shootdelay()
    {
        yield return new WaitForSeconds(DelayinSecounds);
        CanShoot = true;
    }
}

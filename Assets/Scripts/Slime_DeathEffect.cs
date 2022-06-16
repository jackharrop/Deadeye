using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_DeathEffect : MonoBehaviour
{

    public float DestroyTimer;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DeathEffect", DestroyTimer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DeathEffect()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyImpact : MonoBehaviour
{
    public float DestroyTimer;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyImpacts", DestroyTimer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void DestroyImpacts()
    {
        Destroy(gameObject);
    }
}

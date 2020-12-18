using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    private float projectileSpeed = 75;
    private float outOfBounds = 45;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * projectileSpeed);         //ProjectileMovement

        if (transform.position.x > outOfBounds)
        {
            Destroy(gameObject);                                                     //Destroy when out of bounds
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private float enemySpeed;
    private float outOfBounds = -47;

    private PlayerController playerControllerScript;
    private SpawnManager spawnManagerScript;

    public ParticleSystem explosionParticle;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();            //Get a reference to the playercontroller Script
        spawnManagerScript = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();             //Get a refernce to the spawnmanager script
    }

    // Update is called once per frame
    void Update()
    {
        enemySpeed = Random.Range(12, 17);                                                          //Give the enemies a random speed value

        transform.Translate(Vector3.forward * Time.deltaTime * enemySpeed);                             //Enemy movement

        if (transform.position.x < outOfBounds)
        {
            Destroy(gameObject);                                                                       //Destroy when out of bounds
 
            spawnManagerScript.UpdateScore(1);                                                         //Add +1 to the score whenever a Rock goes out of bounds
        }

        if (playerControllerScript.gameOver == false)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * enemySpeed);
        }

        if (playerControllerScript.gameOver == true)
        {
            enemySpeed = 0f;        
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))                                                             //Destroy when hits object with the tag "Projectile"
        {
            Destroy(gameObject);
            spawnManagerScript.UpdateScore(3);                                                          //Add +3 to the score whenever a Rock is destroyed by the Player
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            Destroy(other.gameObject);
        }
    }
}

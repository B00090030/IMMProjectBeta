using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float horizontalInput;
    public float verticalInput;
    private float playerSpeed = 20;

    private float xRange = 45;
    private float zRange = 15;

    public GameObject projectilePrefab;

    public bool gameOver = true;

    public bool hasPowerup = false;

    public ParticleSystem dirtParticle;

    public Animator playerAnim;

    public AudioClip crashSound;
    private AudioSource playerAudio;
    public AudioSource backgroundAudio;

    public GameObject gameOverText;
    public GameObject restartButton;

    public GameObject powerupIndicator;


    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver == false)
        {
            playerAnim.SetBool("Static_b", true);

            horizontalInput = Input.GetAxis("Horizontal");                                                                                              //Player Movement
            verticalInput = Input.GetAxis("Vertical");                                                                                                  //Player Movement

            transform.Translate(Vector3.right * verticalInput * Time.deltaTime * playerSpeed);                                                          //Player Movement
            transform.Translate(Vector3.forward * horizontalInput * Time.deltaTime * playerSpeed);                                                      //Player Movement

            powerupIndicator.transform.position = transform.position + new Vector3(0, 0.5f, 0);

            dirtParticle.Play();
            if (transform.position.x < -xRange)
            {
                transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);                                                  //Keeping Player inbounds
            }

            if (transform.position.x > xRange)
            {
                transform.position = new Vector3(xRange, transform.position.y, transform.position.z);                                                   //Keeping Player inbounds
            }

            if (transform.position.z < -zRange)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -zRange);                                                  //Keeping Player inbounds
            }

            if (transform.position.z > zRange)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, zRange);                                                   //Keeping Player inbounds
            }

            if (Input.GetKeyDown(KeyCode.Space) && hasPowerup == true)
            {
                Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);                                                 //Launch the projectile
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))                                                                                                                 //Destroy the Powerup when hits object with the tag "Powerup"
        {
            hasPowerup = true;
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCountdownRoutine());
        }

        if (other.gameObject.CompareTag("Enemy"))                                                                                                        //"End" the game when you hit an enemy, set the boolean to true and add a Game Over to the debug log
        {
            gameOver = true;
            Debug.Log("Game Over!");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 2.0f);
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            backgroundAudio.Stop();
            gameOverText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
        }
    }

    IEnumerator PowerUpCountdownRoutine()
    {
        if (gameOver == false)
        {
            yield return new WaitForSeconds(5);
            hasPowerup = false;
            powerupIndicator.gameObject.SetActive(false);
        }
    }

    public void RestartGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

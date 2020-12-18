using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;

    private float spawnRangeZ = 17;
    private float spawnInterval = 3f;

    private PlayerController playerControllerScript;

    public GameObject powerupPrefab;

    private int score;
    public TextMeshProUGUI scoreText;

    public GameObject titleScreen;
    public Button startButton;


    // Start is called before the first frame update
    void Start()
    {
        startButton = gameObject.GetComponent<Button>();
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame() 
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();                                                                        //Get a reference to the playercontroller Script

        playerControllerScript.gameOver = false;

        titleScreen.gameObject.SetActive(false);                                                                                                                    //Remove the Title and button when the game starts

        GameObject.Find("Player").SetActive(true);

        StartCoroutine(SpawnRandomEnemy());                                                                                                                         //Continously Spawn Enemies
        StartCoroutine(SpawnPowerup());                                                                                                                             //Continously Spawn Powerups
        score = 0;
        UpdateScore(0);
    }

    IEnumerator SpawnRandomEnemy()
    {
        while (playerControllerScript.gameOver == false)                                                                                                            //Stop spawning when Game is Over
        {
            yield return new WaitForSeconds(spawnInterval);
            int enemyIndex = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[enemyIndex], new Vector3(45, 0, Random.Range(spawnRangeZ, -spawnRangeZ)), enemyPrefabs[enemyIndex].transform.rotation);        //Spawn a random Enemy from the array in a random z position
        }
    }

    IEnumerator SpawnPowerup()                                                                                                                                              //Spawn the powerup in a random area on the map
    {
        float spawnPowerRangeX = 9;
        float spawnPowerRangeZ = 15;
        float powerupSpawnTimer = Random.Range(20, 40);

        while (playerControllerScript.gameOver == false)
        {
            yield return new WaitForSeconds(powerupSpawnTimer);
            Instantiate(powerupPrefab, new Vector3(Random.Range(-spawnPowerRangeX, spawnPowerRangeX), 0, Random.Range(spawnPowerRangeZ, -spawnPowerRangeZ)), powerupPrefab.transform.rotation);
        }
    }

    public void UpdateScore(int scoreToAdd)                                                                                                                          //Method for updating the score in the UI
    {
        score += scoreToAdd;
        scoreText.text = "Rocks Destroyed: " + score;
    }
}

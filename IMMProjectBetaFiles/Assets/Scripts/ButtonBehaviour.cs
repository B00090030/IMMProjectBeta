using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{

    private SpawnManager spawnManagerScript;
    private PlayerController playerControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        spawnManagerScript = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGameButton()
    {
        spawnManagerScript.StartGame();
        Debug.Log("Start Button Pressed");
    }

    public void RestartGameButton()
    {
        playerControllerScript.RestartGame();
        Debug.Log("Restart Button Pressed");
    }
}

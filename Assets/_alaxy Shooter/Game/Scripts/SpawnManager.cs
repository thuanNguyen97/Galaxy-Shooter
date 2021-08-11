using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyShips;

    [SerializeField]
    private GameObject[] _powerups;

    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        //find and get the GameManager components
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        
    }

    public void StartSpawnRoutine()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(PowerupSpawnRoutine());
    }

    //create a coroutine to spawn the enemy every 5 seconds
    public IEnumerator SpawnEnemyRoutine()
    {
        while (_gameManager.gameOver == false) 
        {
            Instantiate(_enemyShips, new Vector3(Random.Range(-8.43f, 8.43f), 6.5f, 0), Quaternion.identity); //spawn enemy with random x
            yield return new WaitForSeconds(Random.Range(1.0f, 1.7f));  //wait for between 1s and 5s
        }
    }

    //corotine to spawn powerup items
    public IEnumerator PowerupSpawnRoutine()
    {
        while (_gameManager.gameOver == false)
        {
            int randomPowerup = Random.Range(0, 3);     // in Random.Range() the max is exclusive, so using 0 -> 10 will return 0 -> 9
            Instantiate(_powerups[randomPowerup], new Vector3(Random.Range(-8.43f, 8.43f), 6.5f, 0), Quaternion.identity); //spawn powerup items with random x
            yield return new WaitForSeconds(Random.Range(5.0f, 8.0f)); //wait for between 1s and 5s
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //when the game start, it start with this Game over state  
    public bool gameOver = true;

    public GameObject player;

    private UIManager _uiManager;

    //if gameOver is true
    //if space key pressed
    //spawn the player
    //gameOver is false
    //hide title screen

    void Start() 
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();    
    }

    // Update is called once per frame
    void Update() 
    {
        if (gameOver == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity); //we can replace to Vector3.zero
                gameOver = false;
                _uiManager.HideTitleScreen();
            }
        }
    }

}

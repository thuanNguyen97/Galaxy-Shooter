using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

    [SerializeField]
    private float _speed = 3.0f;

    [SerializeField]
    private int _powerupID; //0 = triple shot, 1 = speed boost, 2 = shield

    [SerializeField]
    private AudioClip _clips;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -5.52f)
        {
            Destroy(this.gameObject);
        }  
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Debug.Log("Collided with " + other.name);

        if (other.tag == "Player") // to check if it was the right game object, the player will have the tag named player
        {
            //access the player in the Player class
            Player player = other.GetComponent<Player>(); 
            if (player != null)
            {
                
                if (_powerupID == 0)
                {
                    // enable triple shot as a method
                    player.TripleShotPowerupOn();
                }
                else if (_powerupID == 1)
                {
                    // enable speed boost as a method
                    player.SpeedBoostPowerupOn();
                } 
                else if (_powerupID == 2)
                {
                    // enable shield as a method
                    player.enebleShields();
                }
            }
            
            AudioSource.PlayClipAtPoint(_clips, Camera.main.transform.position, 1f);    //play the powerup sound 
            //destroy the item in the game scene
            Destroy(this.gameObject);    
        }
             
    }

}

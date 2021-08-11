using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyExplosionPrefab;

    //variable for the speed
    [SerializeField]
    private float _speed = 3.0f;

    [SerializeField]
    private AudioClip _clips;

    private UIManager _uiManager;
 
    
    // Start is called before the first frame update
    void Start()
    {
        // Find the UI component (Score_Text) in the canvas
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //move down
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        //when off the screen on the bottom
        //destroy enemy game object
        if (transform.position.y < -5.52)
        {
            Destroy(this.gameObject);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Laser")
        {
            if (other.transform.parent != null)
            {
                Destroy(other.transform.parent.gameObject);
            }

            Destroy(other.gameObject); //destroy laser game object
            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity); //instantiate enemy explosion prefab
            AudioSource.PlayClipAtPoint(_clips, Camera.main.transform.position, 1f);    //play that sound in that position
            Destroy(this.gameObject); //destroy enemy game object

            if (_uiManager != null) // we did find it
            {
                _uiManager.UpdateScore();   //update the score
            }
        }
        else if (other.tag == "Player")
        {
            //damage player
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }
            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity); //instantiate enemy explosion prefab
            AudioSource.PlayClipAtPoint(_clips, Camera.main.transform.position, 1f);    //play that sound in that position
            Destroy(this.gameObject); //destroy enemy game object
        }
    }

}

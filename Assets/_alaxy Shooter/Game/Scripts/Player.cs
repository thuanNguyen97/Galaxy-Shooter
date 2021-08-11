using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool canTripleShots = false;
    public bool speedBoost = false;
    public bool shields = false;
    public int lives = 3;

    [SerializeField]
    private GameObject _playerExplosionPrefab;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private GameObject _shieldGameObject;

    [SerializeField]
    private GameObject[] _engines;

    

    //fireRate is 0.25f second
    //canFire -- has the amount of time between fireRate passed?
    [SerializeField]
    private float _fireRate = 0.1f;
    private float _canFire = 0.0f;
    [SerializeField]
    private float _speed = 5.0f;

    private UIManager _uiManager;
    private GameManager _gameManager;
    private SpawnManager _spawnManager;
    private AudioSource _audioSource;

    private int hitCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        //current pos = new position
        transform.position = new Vector3(0, 0, 0);
        
        //find Canvas and parse data to UIManager
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        //find GameManager and parse data to GameManager
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        //find SpawnManager class and get components from it
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        //get access to audio source
        _audioSource = GetComponent<AudioSource>();

        if (_uiManager != null) //we did find it
        {
            _uiManager.UpdateLives(lives);   //parsing lives of player of Player class to UpdateLives(int currentLives) method in UIManager class
        }

        //if we did find it, when spawn player, start spawn routine
        if (_spawnManager != null)
        {
            _spawnManager.StartSpawnRoutine();
        }

        //count the player hit times to instantiate the engine failure game object
        hitCount = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        //if space key pressed
        //spawn laser at player's position
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        //if triple shot = true
        //instantiate triple shot prefab
        //else
        //shot 1 laser

        if (Time.time > _canFire)
        {
            //play the sound everytime this method was called
            _audioSource.Play();

            if (canTripleShots == true)
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                //spawn my laser
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
            }
            //fire after 0.25s
            _canFire = Time.time + _fireRate;         
        }
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //if speedBoost = true
        //move 1.5x speed
        //else
        //move normal speed
        if (speedBoost == true)
        {
            transform.Translate(Vector3.right * _speed * 1.5f * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * 1.5f * verticalInput * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);
        }
        
        //if player on positon y > 4.2
        //set player position on Y to 4.2
        if (transform.position.y > 4.2f)
        {
            transform.position = new Vector3(transform.position.x, 4.2f);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f);
        }
        
        //if player on position x >= 8.3
        //set player position on the x to 8.3
        if (transform.position.x > 8.3f)
        {
            transform.position = new Vector3(8.3f, transform.position.y);
        }
        else if (transform.position.x < -8.3f)
        {
            transform.position = new Vector3(-8.3f, transform.position.y);
        }
    }

    public void Damage()
    {
        //if player has shield 
        //do nothing
        
        if (shields == true)
        {
            shields = false;    //turn off shields
            _shieldGameObject.SetActive(false); //turn off shield visual
            return; //turn off this program
        }

        hitCount++; //when the damage method was called, hitCount ++
        
        if (hitCount == 1)
        {
            //turn on left engine failure
            _engines[0].SetActive(true);
        }
        else if (hitCount == 2)
        {
            //turn on right engine failure
            _engines[1].SetActive(true);
        }

        //subtract 1 life from player
        lives--;
        _uiManager.UpdateLives(lives);

        //if life < 1 (equal 0)
        if (lives < 1)
        {
            Instantiate(_playerExplosionPrefab, transform.position, Quaternion.identity);
            _gameManager.gameOver = true;   //set game state to game over
            _uiManager.ShowTitleScreen();   //show the title screen;
            Destroy(this.gameObject);   //destroy player
        }

    }

    //method to use speed boost cooldown
    public void SpeedBoostPowerupOn()
    {
        speedBoost = true;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    //method to use triple shot cooldown
    public void TripleShotPowerupOn()
    {
        canTripleShots = true;
        StartCoroutine(TripleShotPowerDownRoutine());
        
    }

    //method to eneble shields
    public void enebleShields()
    {
        shields = true;
        _shieldGameObject.SetActive(true); //activate shield visual
    }

    //coroutine for cooldown triple shot
    public IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShots = false;
    }

    //coroutine for cooldown speed boost
    public IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        speedBoost = false;
    }

}

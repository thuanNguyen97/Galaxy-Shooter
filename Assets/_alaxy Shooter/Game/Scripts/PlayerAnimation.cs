using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {   
        //get component in the animator
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //if "a" is pressed down or left arrow is pressed down, trigger the turn left animation
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _animator.SetBool("Turn_Left", true);
            _animator.SetBool("Turn_Right", false); //this code make the turn right false so the A and D key will not fight each other
        }

        //if we lift the a key or left arrow key up, set turn left animation to false
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            _animator.SetBool("Turn_Left", false);
        }

        //if "d" is pressed down or right arrow is pressed down, trigger the turn left animation
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            _animator.SetBool("Turn_Right", true);
            _animator.SetBool("Turn_Left", false); //this code make the turn right false so the A and D key will not fight each other
        }

        //if we lift the d key or right arrow key up, set turn left animation to false
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            _animator.SetBool("Turn_Right", false);
        }
    }
}

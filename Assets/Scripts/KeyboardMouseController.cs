﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardMouseController : MonoBehaviour
{


	// Start is called before the first frame update
	void Start()
    {
    }

	// FixedUpdate is called every fixed frame-rate frame, use it when using Rigidbody
	private void FixedUpdate()
	{
        
    }

    bool shotingDelay = true;

	// Update is called once per frame
	void Update()
    {
 
        if (Input.GetKey(KeyCode.W))
        {
            GameController.gameController.PlayerCharacter.GetComponent<Character>().Jump();
        }

        float moveInput = Input.GetAxis("Horizontal");
        GameController.gameController.PlayerCharacter.GetComponent<Character>().Move(moveInput);

        if (Input.GetButtonDown("Fire3"))
        {
            GameController.gameController.PlayerCharacter.GetComponent<Weapon>().Shoot();
        }
           


        if (Input.GetKey(KeyCode.Escape)) Application.Quit();
      
    }
    
    
   

}

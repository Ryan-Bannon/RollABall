using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


public class PlayerScript : MonoBehaviour
{
	public TMP_Text scoreText;
	public TMP_Text winText;
	public TMP_Text timeText;
	public float speed;	
	private Rigidbody rBody;
	public GameObject collectible; 
	private int score;
	private bool scored;
	public int collectibleCount;
	public float timeLeft; 
	private bool lost;
	private bool active;
	public GameObject resetButton;
	public GameObject winButton;
	private bool grounded;
	public float jumpHeight;
	private string sceneName;
	private bool levelTwo;
	public bool beginning;
	private float wTextTime;
	public bool pressed;
	void Start()
	{
		if(PlayerPrefs.GetString("HasBeenReset") == "true")
			pressed = true;
		else
			pressed = false;
		sceneName = SceneManager.GetActiveScene().name;
		
		if(sceneName == "Level 2")
			levelTwo = true;		
		else
			levelTwo = false;
			
		wTextTime = 4f;
		score = 0;
		scoreText.text = "Score: " + score + "/" + collectibleCount;  //initialize the scoreText value
		
		if(pressed)
		{
			Debug.Log("Script worked!");
			beginning = false;
		}			
		else
		{
			Debug.Log("Script did not work!");
			beginning = true;
		}
			
		if(levelTwo && !pressed)
			winText.text = "You can use spacebar to jump";
		else
			winText.text = "";	//initialize the winText value
		timeText.text = "Time remaining: " + timeLeft;
		rBody = GetComponent<Rigidbody>();
		active = true;
		grounded = true;
		
		
		
	}
	void FixedUpdate()
	{	
		if(levelTwo && wTextTime > 0 && beginning)
			wTextTime -= Time.deltaTime;
			
		else if(beginning)
		{
			winText.text = "";
			beginning = false;
		}			

		if(timeLeft > 0)
		{
			timeText.text = "Time remaining: " + (int)timeLeft;
			timeLeft -= Time.deltaTime;			
		}			
		else if(active)
		{
			lost = true;
			endGame();
			if(levelTwo)
				winText.text = "            YOU LOSE!!";
			else
				winText.text = "YOU LOSE!!";
		}
		if(active)
		{
			float moveHorizontal = Input.GetAxis("Horizontal");		//Access the right and left arrow keys
			float moveVertical = Input.GetAxis("Vertical");         //Access the up and down arrow keys
			Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);	//Vector3s deal with movement in 3D space.  X, Y, and Z aspects.  In this case the Y is zero.  Vector3s take floats.	
			rBody.AddForce(movement * speed * Time.deltaTime); //This accesses the rigidbody component and adds force ot get it moving
		}
		else
			rBody.velocity = new Vector3(0f, 0f, 0f);
		
		if(scored)
		{
			scoreText.text ="Score: " + score + "/" + collectibleCount; 
			if(score == collectibleCount && !lost)
			{
				endGame();		
				if(levelTwo)
					winText.text = "            YOU WON!";
				else
					winText.text = "YOU WON!";
				winButton.SetActive(true);
			}			
			scored = false; 
		}
		if(Input.GetKey(KeyCode.Space) && grounded)
			Jump();
	}

	void OnTriggerEnter(Collider target)
	{
		if(target.tag == "collectible")
		{
			score++;
			scored = true;
		}
	}

	private void endGame()
	{
		scoreText.gameObject.SetActive(false);
		timeText.gameObject.SetActive(false);
		active = false;		
		resetButton.SetActive(true);
	}

	private void Jump()
	{
		grounded = false; 
		rBody.AddForce(0, jumpHeight, 0);
	}
	void OnCollisionStay(Collision c)
	{
		if(c.gameObject.tag == "ground")
			grounded = true; 
	}	
}
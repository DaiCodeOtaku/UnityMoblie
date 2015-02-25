﻿using UnityEngine;
using System.Collections;

public enum cScheme {moveScale, arrows, accel, SOD, moveScaleInv, arrowsInv, accelInv, SODInv, tiltInv, tilt};
// moveScale = Player moves at varying speed depending on how extreme their button presses are either side of middle
// arrows = Two arrows appear on screen for the player to click to move at a contstant speed in the chosen direction
// accel = Accelerated movement, the longer the player holds, the faster they move. Let go and they slow down
// SOD = Stop on Dime, if the player changes direction, their speed becomes 0 and they immediately start moving in that direction
// tilt = Gyroscope controls, tilt phone to move in a direction
// suffix -Inv = Inverted control scheme, the player moves in the opposide direction to their input.


public class Player
{
	public float speed; // Constant speed
	public float accelSpeed; // Accelerated speed, cumulative
	public cScheme scheme;  // What control schele is being used
	public float maxSpeed; // Highest positive speed the player can go to in accel scheme
	public float minSpeed; // Lowest etc. 
	public float telegraphTime; // Grace period before the control scheme changes
	public bool teleCheck; // Checks if the grace period is active
	public bool startGetScheme; // Used to tell the control scheme function to get a new control scheme
	public bool direction; // 0 = Left, 1 = Right
	
	public UiControl UIControl;
	public cScheme lastScheme;
	public AudioSource music;


	public Player()
	{
		scheme = cScheme.moveScale;
		speed = 0.0375f;
		accelSpeed = 0; 
		maxSpeed = 10;
		minSpeed = -10;
		telegraphTime = 0.0f;
		lastScheme = (cScheme)(-1);
		startGetScheme = false;
	}

	public void GetControlScheme()
	{
		if(lastScheme == (cScheme)(-1))
		{
			if ((scheme == cScheme.arrows) || (scheme == cScheme.arrowsInv))
			{
				UIControl.ArrowScroll(1);

			}
			if (scheme >= (cScheme)4 && scheme <= (cScheme)8)
			{
				music.pitch = -1.5f;
			}
			lastScheme = scheme;
			UIControl.Controls(scheme.ToString());
		}
		Debug.Log(startGetScheme);
		if (startGetScheme == true)
		{
			while (scheme == lastScheme) 
			{
				// if (UseTilt()) // Player stored info from Options, checks if tilt controls are enabled
				//{
					scheme = (cScheme)Random.Range (0, 10);
				/*}
				else
				{
					scheme = Random.Range (0, 8);
					accelSpeed = 0;
				}*/

				if (scheme != lastScheme)
				{
					telegraphTime = 0;
					startGetScheme = false;
					Debug.Log(scheme);

					UIControl.Inverse(false);
					UIControl.Controls(scheme.ToString());
					accelSpeed = 0;


					if ((scheme != cScheme.arrows || scheme != cScheme.arrowsInv) && (lastScheme == cScheme.arrows || lastScheme == cScheme.arrowsInv))
					{

						UIControl.ArrowScroll(0);
					}
					if ((scheme == cScheme.arrows || scheme == cScheme.arrowsInv) && (lastScheme != cScheme.arrows || lastScheme != cScheme.arrowsInv))
					{
						UIControl.ArrowScroll(1);
					}

				// DisplayScheme(); A function from UI that displays what control scheme is being used
					if (scheme >= (cScheme)4 && scheme <= (cScheme)8)
					{
						//ShowInvert();
						music.pitch = -1.5f;
					} // shows the "INVERT" flashing UI bit
					else
					{
						music.pitch = 1.0f;
					}
				}
			}
			lastScheme = scheme;
		}
	}
}

public class PlayerControls : MonoBehaviour 
{
/*
	__      __       _____   _____            ____   _       ______   _____ 
	\ \    / //\    |  __ \ |_   _|    /\    |  _ \ | |     |  ____| / ____|
	 \ \  / //  \   | |__) |  | |     /  \   | |_) || |     | |__   | (___  
	  \ \/ // /\ \  |  _  /   | |    / /\ \  |  _ < | |     |  __|   \___ \ 
	   \  // ____ \ | | \ \  _| |_  / ____ \ | |_) || |____ | |____  ____) |
		\//_/    \_\|_|  \_\|_____|/_/    \_\|____/ |______||______||_____/  */

	// Timing Variables:
	float timer;
	bool activateTimer = true;
	float teleTime = 3;

	// Speed Variables:
	float arrowSpeed = 150.0f;
	float normalisedSpeed = 0.0f;
	float tiltSpeed = 6.0f;
	float acceleration = 0.2f;
	float revAcceleration = 0.4f;
	float scaleSpeedLimiter = 0.9f;

	// Misc Variables:
	bool checkSOD;
	float moveThreshold = 0.2f;
	float arrowTop = (Screen.height / 5);
	float arrowBottom = (Screen.height / 8);
	bool musicPlayed = false;

	// Class Variables:
	Player player = new Player();
	public AudioSource inverseBeeps;
	public AudioSource gameStart;
	public AudioSource Music;
	public AudioSource explosion;

	// Use this for initialization
	void Start () 
	{
		timer = 0;
		checkSOD = false;
		player.UIControl = GameObject.FindObjectOfType<UiControl>();
		player.music = Music;
	}

	/*
	 ______ _    _ _   _  _____ _______ _____ ____  _   _  _____ 
	|  ____| |  | | \ | |/ ____|__   __|_   _/ __ \| \ | |/ ____|
	| |__  | |  | |  \| | |       | |    | || |  | |  \| | (___  
	|  __| | |  | | . ` | |       | |    | || |  | | . ` |\___ \ 
	| |    | |__| | |\  | |____   | |   _| || |__| | |\  |____) |
	|_|     \____/|_| \_|\_____|  |_|  |_____\____/|_| \_|_____/  
	 */

	// UNITY FUNCTIONS
	// Update is called once per frame
	void Update () 
	{
		normalisedSpeed = player.speed * Time.deltaTime;
		timer += Time.deltaTime;

		if (player.scheme != (cScheme)(-1))
		{
			ArrowCheck ();
			PlayTimer ();
			TelegraphChecker ();
			MovePlayer ();
			AccelLimit (ref player);
			if (!gameStart.isPlaying && musicPlayed == false)
			{
				Music.Play ();
				musicPlayed = true;
			}
		}
	}


	//SCOTT'S FUNCTIONS
	// Update Function
	void MovePlayer()
	{
		if ((this.transform.position.x + 0.25 < 3.1) && (this.transform.position.x - 0.25 > -3.1))
		{
			switch ((cScheme)player.scheme)
			{
			case cScheme.moveScale:
				MoveScale(1);
				break;
			case cScheme.moveScaleInv:
				MoveScale(-1);
				break;
			case cScheme.tilt:
				Tilt(1);
				break;
			case cScheme.tiltInv:
				Tilt (-1);
				break;
			case cScheme.accel:
				Accelerate(1);
				break;
			case cScheme.accelInv:
				Accelerate(-1);
				break;
			case cScheme.SOD:
				StopOnDime(1);
				break;
			case cScheme.SODInv:
				StopOnDime(-1);
				break;
			case cScheme.arrows:
				Arrows(1);
				break;
			case cScheme.arrowsInv:
				Arrows (-1);
				break;
				//Move movement schemes go here
			default:
				break;
			}
		}
		else
		{
			EdgeDetect(ref player.accelSpeed);
		}
	}

	// Movement Functions:
	void MoveScale(int sign)
	{
		if (Input.GetMouseButton (0) && Input.mousePosition.x > (Screen.width / 2)) 
		{
			this.transform.Translate ((Input.mousePosition.x - (Screen.width / 2)) * normalisedSpeed * scaleSpeedLimiter * sign, 0, 0);
		} 
		else if (Input.GetMouseButton (0) && Input.mousePosition.x < (Screen.width / 2)) 
		{	
			this.transform.Translate ((((Screen.width / 2) - Input.mousePosition.x) * -normalisedSpeed * scaleSpeedLimiter * sign), 0, 0);
		}
	}

	void Tilt(int sign)
	
	{
		if (Input.acceleration.y > moveThreshold)
		{
			transform.Translate(Input.acceleration.x * player.speed * tiltSpeed * sign,0,0);
		}
		else if (Input.acceleration.y < -moveThreshold)
		{
			transform.Translate(Input.acceleration.x * player.speed * tiltSpeed * sign,0,0);
		} 
	}

	void Accelerate(int sign)
	{
		if (Input.GetMouseButton (0) && Input.mousePosition.x < (Screen.width / 2)) 
		{	
			if(player.direction == false)
			{
				player.accelSpeed -= acceleration;
			} else
			{
				player.accelSpeed -= revAcceleration;
			}
		} 
		else if (Input.GetMouseButton (0) && Input.mousePosition.x > (Screen.width / 2)) 
		{	
			if(player.direction == true)
			{
				player.accelSpeed += acceleration;
			} else
			{
				player.accelSpeed += revAcceleration;
			}
		}
		else 
		{
			player.accelSpeed *= 0.98f;
		}
		this.transform.Translate (player.accelSpeed * Time.deltaTime * sign, 0, 0);
	}

	void StopOnDime(int sign)
	{
		if (Input.GetMouseButton (0) && Input.mousePosition.x < (Screen.width / 2)) 
		{	
			if(transform.position.x > 0)
			{
				if(checkSOD == false)
				{
					player.accelSpeed = 0.0f;
					checkSOD = true;
				}
				else
				{
					player.accelSpeed -= acceleration;
				}
			} else
			{
				player.accelSpeed -= revAcceleration;
				checkSOD = false;
			}
		} 
		else if (Input.GetMouseButton (0) && Input.mousePosition.x > (Screen.width / 2)) 
		{	
			if(transform.position.x < 0)
			{
				if(checkSOD == false)
				{
					player.accelSpeed = 0.0f;
					checkSOD = true;
				}
				else
				{
					player.accelSpeed += acceleration;
				}
			} else
			{
				player.accelSpeed += revAcceleration;
				checkSOD = false;
			}
		}
		this.transform.Translate (player.accelSpeed * Time.deltaTime * sign, 0, 0);
	}

	void Arrows(int sign)
	{
		
		if (Input.GetMouseButton (0) && (Input.mousePosition.x > ((Screen.width / 4) + (Screen.width / 2.5))) && ((Input.mousePosition.y < arrowTop) && (Input.mousePosition.y > arrowBottom))) 
		{	
			this.transform.Translate (normalisedSpeed * arrowSpeed * sign, 0, 0);
		} 
		else if (Input.GetMouseButton (0) && (Input.mousePosition.x < (Screen.width / 2.5)) && ((Input.mousePosition.y < arrowTop) && (Input.mousePosition.y > arrowBottom))) 
		{	
			this.transform.Translate (-normalisedSpeed * arrowSpeed * sign, 0, 0);
		}
	}
	
	
	// Misc Functions:
	public void EdgeDetect(ref float accelSpeed)
	{
		accelSpeed = 0.0f;
		if (this.transform.position.x < (Screen.width/2))
		{
			this.transform.Translate ((this.transform.position.x * -0.005f),0,0);
		}
		else
		{
			this.transform.Translate ((this.transform.position.x * -0.005f),0,0);
		}
	}
	
	public void AccelLimit (ref Player player)
	{
		if (player.accelSpeed >= player.maxSpeed) 
		{
			player.accelSpeed = player.maxSpeed - 1;
		}
		if (player.accelSpeed <= player.minSpeed) 
		{
			player.accelSpeed = player.minSpeed + 1;
		}
	}

	void TelegraphChecker ()
	{
		if (player.teleCheck) 
		{
			player.telegraphTime += Time.deltaTime;
			if(player.telegraphTime >= teleTime)
			{
				player.startGetScheme = true;
				player.teleCheck = false;
				activateTimer = true;
				player.telegraphTime = 0.0f;
				player.UIControl.Inverse(true);
				inverseBeeps.Play();
			}
		}
	}

	void PlayTimer ()
	{
		if (timer >= 5 && activateTimer) 
		{
			player.GetControlScheme();
			activateTimer = false;
			timer = 0.0f;
			player.teleCheck = true;
		}
	}

	void ArrowCheck()
	{
		if (player.lastScheme == (cScheme)(-1))
		{
			player.GetControlScheme();
		}
	}

	void DirectionCheck()
	{
		if(player.speed < 0)
		{
			player.direction = false;
		}
		else
		{
			player.direction = true;
		}
	}

	void OnCollisionEnter ()
	{
		if (ObstacleController.GO == false) {
			player.UIControl.GameOver();
			ObstacleController.GO = true;
			player.scheme = (cScheme)(-1);
			Music.pitch = -0.5f;
			explosion.Play ();
			inverseBeeps.Stop();
			Handheld.Vibrate();
			Destroy(this.gameObject);
		}
	}
}


